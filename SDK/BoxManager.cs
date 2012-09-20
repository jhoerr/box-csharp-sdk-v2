using System;
using System.Net;
using System.Threading;
using BoxApi.V2.SDK.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2.SDK
{
    /// <summary>
    ///   Provides methods for using Box.NET SOAP web service
    /// </summary>
    public partial class BoxManager  
    {
        private string _serviceUrl = "https://www.box.com/api/";
        private readonly RestClient _restContentClient;
        private readonly RequestHelper _requestHelper;

        /// <summary>
        ///   Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The unique API key which is assigned to application </param>
        /// <param name="authorizationToken"> Valid authorization ticket </param>
        /// <param name="proxy"> Proxy information </param>
        public BoxManager(string applicationApiKey, string authorizationToken, IWebProxy proxy = null)
        {
            _requestHelper = new RequestHelper();

            _restContentClient = new RestClient(_serviceUrl) {Proxy = proxy, Authenticator = new RequestAuthenticator(applicationApiKey, authorizationToken)};
            _restContentClient.ClearHandlers();
            _restContentClient.AddHandler(RequestHelper.JsonMimeType, new JsonDeserializer());
        }

        private static void GuardFromNull(object arg, string argName)
        {
            if (arg == null || (arg is string && string.IsNullOrEmpty((string) arg)))
            {
                throw new ArgumentException("Argument cannot be null or empty", argName);
            }
        }

        private IRestResponse Execute(IRestRequest request)
        {
            var restResponse = _restContentClient.Execute(request);
            Error error;
            if (!WasSuccessful(restResponse, out error))
            {
                throw new BoxException(error);
            }
            return restResponse;
        }

        private T Execute<T>(IRestRequest request) where T : class, new()
        {

            var restResponse = _restContentClient.Execute<T>(request);
            Error error;
            if (!WasSuccessful(restResponse, out error))
            {
                throw new BoxException(error);
            }
            return restResponse.Data;
        }


        private void ExecuteAsync<T>(IRestRequest request, Action<T> onSuccess, Action onFailure) where T : class, new()
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("onSuccess can not be null");
            }

            _restContentClient.ExecuteAsync<T>(request, response =>
                {
                    Error error;
                    if (WasSuccessful(response, out error))
                    {
                        onSuccess(response.Data);
                    }
                    else if (onFailure != null)
                    {
                        onFailure();
                    }
                });
        }

        private void ExecuteAsync(IRestRequest request, Action<IRestResponse> onSuccess, Action onFailure)
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("callback can not be null");
            }

            _restContentClient.ExecuteAsync(request, response =>
                {
                    Error error;
                    if (WasSuccessful(response, out error))
                    {
                        onSuccess(response);
                    }
                    else if (onFailure != null)
                    {
                        onFailure();
                    }
                });
        }

        private bool WasSuccessful(IRestResponse restResponse, out Error error)
        {
            return _requestHelper.WasSuccessful(restResponse, out error);
        }

        private static void Backoff(int attempt)
        {
            Thread.Sleep((int)Math.Pow(2, attempt) * 100);
        }
    }
}