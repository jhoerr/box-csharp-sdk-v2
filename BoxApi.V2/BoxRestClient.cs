using System;
using System.Linq;
using System.Net;
using BoxApi.V2.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{
    public class BoxRestClient : RestClient
    {
        private const string ServiceUrl = "https://www.box.com/api/";
        public const string JsonMimeType = "application/json";
        public const string JsonAltMimeType = "application/json";
        public const string XmlMimeType = "application/xml";
        public const string XmlAltMimeType = "text/xml";

        public BoxRestClient(IAuthenticator authenticator = null, IWebProxy proxy = null):
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
            if (!WasSuccessful(restResponse, out error))
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
            if (onSuccess == null)
            {
                throw new ArgumentException("onSuccess can not be null");
            }

            base.ExecuteAsync<T>(request, (response, handle) => 
                {
                    Error error;
                    if (WasSuccessful(response, out error))
                    {
                        onSuccess(response.Data);
                    }
                    else if (onFailure != null)
                    {
                        handle.Abort();
                        onFailure(error);
                    }
                });
        }

        public void ExecuteAsync(IRestRequest request, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("callback can not be null");
            }

            base.ExecuteAsync(request, (response, handle) => 
                {
                    Error error;
                    if (WasSuccessful(response, out error))
                    {
                        onSuccess(response);
                    }
                    else if (onFailure != null)
                    {
                        handle.Abort();
                        onFailure(error);
                    }
                });
        }

        public bool WasSuccessful(IRestResponse restResponse, out Error error)
        {
            error = null;
            bool success = true;
            if (restResponse == null)
            {
                success = false;
            }
            else if(restResponse.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                error = new Error() { Code = "Internal Server Error", Status = "500" };
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
    }
}