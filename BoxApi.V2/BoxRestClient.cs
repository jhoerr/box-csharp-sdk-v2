using System;
using System.Linq;
using System.Net;
using BoxApi.V2.Authentication;
using BoxApi.V2.Authentication.Common;
using BoxApi.V2.Authentication.Legacy;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{
    internal class BoxRestClient : RestClient
    {
        private const string ServiceUrl = "https://api.box.com/";
        public const string JsonMimeType = "application/json";
        public const string XmlMimeType = "application/xml";
        public const string XmlAltMimeType = "text/xml";

        public BoxRestClient(IRequestAuthenticator authenticator = null, IWebProxy proxy = null) :
            base(ServiceUrl)
        {
            Authenticator = authenticator;
            Proxy = proxy;
            ClearHandlers();
            var xmlDeserializer = new XmlDeserializer();
            var jsonDeserializer = new JsonDeserializer();
            AddHandler(XmlMimeType, xmlDeserializer);
            AddHandler(XmlAltMimeType, xmlDeserializer);
            AddHandler(JsonMimeType, jsonDeserializer);
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            IRestResponse restResponse = base.Execute(request);
            Error error;
            if (!HandleResponse(restResponse, out error))
            {
                switch (error.Status)
                {
                    case 202:
                        throw new BoxDownloadNotReadyException(error);
                    case 304: // precondition (If-None-Match) failed
                        throw new BoxItemNotModifiedException(error);
                    case 412: // precondition (If-Match) failed
                        throw new BoxItemModifiedException(error);
                    default:
                        throw new BoxException(error);
                }
            }
            return restResponse;
        }

        public T ExecuteAndDeserialize<T>(IRestRequest request) where T : class, new()
        {
            IRestResponse<T> restResponse = base.Execute<T>(request);
            return restResponse.Data;
        }

        public void ExecuteAsync<T>(IRestRequest request, Action<T> onSuccess, Action<Error> onFailure) where T : class, new()
        {
            AssertUsableActions(onSuccess, onFailure);
            base.ExecuteAsync<T>(request, (response, handle) =>
                {
                    Error error;
                    if (HandleResponse(response, out error))
                    {
                        onSuccess(response.Data);
                        return;
                    }
                    HandleFailure(onFailure, handle, error);
                });
        }

        public void ExecuteAsync(IRestRequest request, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            AssertUsableActions(onSuccess, onFailure);
            base.ExecuteAsync(request, (response, handle) =>
                {
                    Error error;
                    if (HandleResponse(response, out error))
                    {
                        onSuccess(response);
                        return;
                    }
                    HandleFailure(onFailure, handle, error);
                });
        }

        public void ExecuteAsync(IRestRequest request, Action onSuccess, Action<Error> onFailure)
        {
            AssertUsableActions(onSuccess, onFailure);
            base.ExecuteAsync(request, (response, handle) =>
                {
                    Error error;
                    if (HandleResponse(response, out error))
                    {
                        onSuccess();
                        return;
                    }
                    HandleFailure(onFailure, handle, error);
                });
        }

        private static void AssertUsableActions<T>(Action<T> onSuccess, Action<Error> onFailure)
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("success callback can not be null", "onSuccess");
            }

            AssertUsableFailureAction(onFailure);
        }

        private static void HandleFailure(Action<Error> onFailure, RestRequestAsyncHandle handle, Error error)
        {
            handle.Abort();
            onFailure(error);
        }

        private static void AssertUsableActions(Action onSuccess, Action<Error> onFailure)
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("success callback can not be null", "onSuccess");
            }

            AssertUsableFailureAction(onFailure);
        }

        private static void AssertUsableFailureAction(Action<Error> onFailure)
        {
            if (onFailure == null)
            {
                throw new ArgumentException("failure callback can not be null", "onFailure");
            }
        }

        public bool HandleResponse(IRestResponse restResponse, out Error error)
        {
            error = null;
            bool success = true;

            TryClearSharedLink();

            if (restResponse == null)
            {
                success = false;
            }
            else if (restResponse.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                error = new Error {Code = "Internal Server Error", Status = 500};
                success = false;
            }
            else if (restResponse.StatusCode.Equals(HttpStatusCode.NotModified))
            {
                error = new Error { Code = "Not Modified", Status = 304, HelpUrl = "http://developers.box.com/docs/#if-match" };
                success = false;
            }
            else if (restResponse.StatusCode.Equals(HttpStatusCode.Accepted))
            {
                var retryAfter = restResponse.Headers.SingleOrDefault(h => h.Name.Equals("Retry-After", StringComparison.InvariantCultureIgnoreCase));
                if (retryAfter != null)
                {
                    error = new Error { Code = "Download Not Ready", Message = "This file is not yet ready to be downloaded. Please wait and try again.", HelpUrl = "http://developers.box.com/docs/#files-download-a-file", Status = 202, RetryAfter = int.Parse((string)retryAfter.Value) };
                    success = false;
                }
            }

            else if (restResponse.ContentType.Equals(JsonMimeType))
            {
                var jsonDeserializer = new JsonDeserializer();
                if (restResponse.Content.Contains(@"""type"":""error"""))
                {
                    success = false;
                    error = TryGetSingleError(restResponse, jsonDeserializer) ?? TryGetFirstErrorFromCollection(restResponse, jsonDeserializer);
                }
                else if (restResponse.Content.Contains(@"""error"":"))
                {
                    var authError = jsonDeserializer.Deserialize<AuthError>(restResponse);
                    error = new Error(){Code = authError.Error, Status = 400, Message = authError.ErrorDescription, Type = ResourceType.Error};
                }
            }
            return success;
        }

        private static Error TryGetSingleError(IRestResponse restResponse, JsonDeserializer jsonDeserializer)
        {
            var deserialized = jsonDeserializer.Deserialize<Error>(restResponse);
            return deserialized.Type.Equals(ResourceType.Error) ? deserialized : null;
        }

        private Error TryGetFirstErrorFromCollection(IRestResponse restResponse, JsonDeserializer jsonDeserializer)
        {
            var deserialized = jsonDeserializer.Deserialize<ErrorCollection>(restResponse);
            return (deserialized.Entries != null) ? deserialized.Entries.First() : null;
        }

        private void TryClearSharedLink()
        {
            if (Authenticator != null)
            {
                ((IRequestAuthenticator) Authenticator).ClearSharedLink();
            }
        }

        public BoxRestClient WithSharedLink(string sharedLink)
        {
            ((IRequestAuthenticator) Authenticator).SetSharedLink(sharedLink);
            return this;
        }
    }
}