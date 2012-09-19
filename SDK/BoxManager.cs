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

            _restContentClient = new RestClient(_serviceUrl) {Proxy = proxy, Authenticator = new BoxAuthenticator(applicationApiKey, authorizationToken)};
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

    public class BoxAuth
    {
        private string _serviceUrl = "https://www.box.com/api/";
        private RequestHelper _requestHelper;

        public WebProxy Proxy { get; private set; }
        public string ApiKey { get; private set; }
        public string Ticket { get; private set; }
        public string AuthToken { get; private set; }


        public BoxAuth(string apiKey, string ticket = null, WebProxy proxy = null)
        {
            ApiKey = apiKey;
            Ticket = ticket;
            Proxy = proxy;
            _requestHelper = new RequestHelper();
        } 

        public string GetTicket()
        {
            if(Ticket == null)
            {
                var restRequest = new RestRequest("1.0/rest");
                restRequest.AddParameter("action", "get_ticket");
                restRequest.AddParameter("api_key", ApiKey);
                var client = new RestClient(_serviceUrl) { Proxy = Proxy };
                client.ClearHandlers();
                client.AddHandler("*", new XmlDeserializer());
                var restResponse = client.Execute<BoxTicket>(restRequest);
                if (!restResponse.Data.Status.Equals("get_ticket_ok"))
                {
                    throw new Exception(restResponse.Data.Status); //TODO - BoxException?
                }
                Ticket = restResponse.Data.Ticket;         
            }
            return Ticket ;
        }

        public string GetAuthorizationUrl()
        {
            return "https://www.box.com/api/1.0/auth/" + GetTicket();
        }

        public string GetAuthorizationToken()
        {
            if (AuthToken == null)
            {
                if(Ticket == null)
                {
                    throw new Exception("You must retrive and approve a ticket before exchanging it for an authorization token");
                }
                var restRequest = new RestRequest("1.0/rest");
                restRequest.AddParameter("action", "get_auth_token");
                restRequest.AddParameter("api_key", ApiKey);
                restRequest.AddParameter("ticket", Ticket);
                var client = new RestClient(_serviceUrl) { Proxy = Proxy };
                client.ClearHandlers();
                client.AddHandler("*", new XmlDeserializer());
                var restResponse = client.Execute<BoxAuthToken>(restRequest);
                if (!restResponse.Data.Status.Equals("get_auth_token_ok"))
                {
                    throw new Exception(restResponse.Data.Status); //TODO - BoxException?
                }
                AuthToken = restResponse.Data.AuthToken;
            }
            return AuthToken;
        }

        public string GetAppAuthTokenForUser(string email)
        {
            if (AuthToken == null)
            {
                var restRequest = new RestRequest("2.0/tokens", Method.POST) { RequestFormat = DataFormat.Json };
                restRequest.AddHeader("Authorization", "BoxAuth api_key=" + ApiKey);
                restRequest.AddBody(new { email });
                var client = new RestClient(_serviceUrl) { Proxy = Proxy};
                client.ClearHandlers();
                client.AddHandler("*", new JsonDeserializer());
                var restResponse = client.Execute<BoxToken>(restRequest);
                Error error;
                if (!_requestHelper.WasSuccessful(restResponse, out error))
                {
                    throw new BoxException(error);
                }
                AuthToken = restResponse.Data.Token;
            }
            return AuthToken;
        }
    }

}