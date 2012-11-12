using System;
using System.Linq;
using System.Net;
using BoxApi.V2.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{


    internal class BoxRestClient : RestClient
    {
        private const string ServiceUrl = "https://www.box.com/api/";
        public const string JsonMimeType = "application/json";
        public const string JsonAltMimeType = "application/json";
        public const string XmlMimeType = "application/xml";
        public const string XmlAltMimeType = "text/xml";

        public BoxRestClient(IBoxAuthenticator authenticator = null, IWebProxy proxy = null) :
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
            AddHandler(JsonAltMimeType, jsonDeserializer);
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            var restResponse = base.Execute(request);
            Error error;
            if (!HandleResponse(restResponse, out error))
            {
                throw new BoxException(error);
            }
            return restResponse;
        }

        public T ExecuteAndDeserialize<T>(IRestRequest request) where T : class, new()
        {
            var restResponse = base.Execute<T>(request);
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
            var success = true;

            TryClearSharedLink();

            if (restResponse == null)
            {
                success = false;
            }
            else if (restResponse.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                error = new Error {Code = "Internal Server Error", Status = "500"};
                success = false;
            }
            else if (restResponse.ContentType.Equals(JsonMimeType) && restResponse.Content.Contains(@"""type"":""error"""))
            {
                success = false;
                var jsonDeserializer = new JsonDeserializer();
                error = jsonDeserializer.Deserialize<Error>(restResponse);
                if (error.Type == null)
                {
                    var errorCollection = jsonDeserializer.Deserialize<ErrorCollection>(restResponse);
                    if (!string.IsNullOrEmpty(errorCollection.TotalCount))
                    {
                        error = errorCollection.Entries.First();
                    }
                }
            }
            return success;
        }

        private void TryClearSharedLink()
        {
            if (Authenticator != null)
            {
                ((IBoxAuthenticator) Authenticator).ClearSharedLink();
            }
        }

        public BoxRestClient WithSharedLink(string sharedLink)
        {
            ((IBoxAuthenticator)Authenticator).SetSharedLink(sharedLink);
            return this;
        }
    }
}