using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
<<<<<<< HEAD
using System.Xml;
using BoxApi.V2.SDK;
=======
using System.Text.RegularExpressions;
using System.Threading;
>>>>>>> 2abf755370b6ae5fbf74a03ef34e7fb81e80edfe
using BoxApi.V2.SDK.Model;
using BoxApi.V2.ServiceReference;
using BoxApi.V2.Statuses;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2.SDK
{
    /// <summary>
    ///   Provides methods for using Box.NET SOAP web service
    /// </summary>
    public partial class BoxManager
    {
        private const string JsonMimeType = "application/json";
        private string _serviceUrl = "https://www.box.com/api/";

        private readonly boxnetService _service;
        private readonly string _apiKey;
        private string _ticket;
        private string _authorizationToken;
        private readonly RestClient _restContentClient;
        private readonly RestClient _restAuthorizationClient;
        private readonly RequestHelper _requestHelper;
        private Entity _rootFolder;
        private IWebProxy _proxy;

        /// <summary>
        ///   Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The unique API key which is assigned to application </param>
        /// <param name="proxy"> Proxy information </param>
        public BoxManager(string applicationApiKey, IWebProxy proxy = null) :
            this(applicationApiKey, null, proxy)
        {
        }

        /// <summary>
        ///   Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The unique API key which is assigned to application </param>
        /// <param name="authorizationToken"> Valid authorization token </param>
        /// <param name="proxy"> Proxy information </param>
        public BoxManager(string applicationApiKey, string authorizationToken, IWebProxy proxy = null)
        {
            _apiKey = applicationApiKey;
            _authorizationToken = authorizationToken;
            _proxy = proxy;

            _restAuthorizationClient = new RestClient {Proxy = proxy};
            _restAuthorizationClient.ClearHandlers();
            _restAuthorizationClient.AddHandler("*", new XmlDeserializer());

            _restContentClient = new RestClient(_serviceUrl) {Proxy = proxy, Authenticator = new BoxAuthenticator(applicationApiKey, authorizationToken)};
            _restContentClient.ClearHandlers();
            _restContentClient.AddHandler(JsonMimeType, new JsonDeserializer());

             _requestHelper = new RequestHelper("1.0", "2.0");
        }

        #region V2_Authentication

        public string GetTicket()
        {
            return _ticket ?? (_ticket = BoxXmlAuthRequest("https://www.box.com/api/1.0/rest?action=get_ticket&api_key=" + _apiKey, "ticket"));
        }

        public string GetAuthorizationUrl()
        {
            return "https://www.box.com/api/1.0/auth/" + GetTicket();
        }

        public string GetAuthorizationToken()
        {
            return _authorizationToken ?? (_authorizationToken =
                BoxXmlAuthRequest("https://www.box.com/api/1.0/rest?action=get_auth_token&api_key=" + _apiKey + "&ticket=" + GetTicket(), "auth_token"));
        }

        public string GetAppAuthTokenForUser(string email)
        {
            if (_authorizationToken == null)
            {
                var restRequest = new RestRequest("2.0/tokens", Method.POST) { RequestFormat = DataFormat.Json };
                restRequest.AddHeader("Authorization", "BoxAuth api_key=" + _apiKey);
                restRequest.AddBody(new { email });
                var client = new RestClient(_serviceUrl) { Proxy = _proxy };
                client.ClearHandlers();
                client.AddHandler("*", new JsonDeserializer());
                var restResponse = client.Execute<Token>(restRequest);
                if (!WasSuccessful(restResponse, HttpStatusCode.Created))
                {
                    throw new BoxException(restResponse);
                }
                _authorizationToken = restResponse.Data.BoxToken;
                _rootFolder = restResponse.Data.RootFolder;
            }
<<<<<<< HEAD
            return _authorizationToken;
=======
            else
            {
                status = GetAuthenticationTokenStatus.Failed;
                error = e.Error;
            }

            var response = new GetAuthenticationTokenResponse
                {
                    Status = status,
                    UserState = state[1],
                    Error = error,
                    AuthenticationToken = string.Empty
                };

            if (response.Status == GetAuthenticationTokenStatus.Successful)
            {
                var authenticatedUser = new User(e.user);
                response.AuthenticatedUser = authenticatedUser;
                response.AuthenticationToken = e.auth_token;
                _token = e.auth_token;

                // Set HTTP auth header
            }

            getAuthenticationTokenCompleted(response);
>>>>>>> 2abf755370b6ae5fbf74a03ef34e7fb81e80edfe
        }

        private string BoxXmlAuthRequest(string url, string elementName)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            using (var response = req.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                // Console application output  
                var doc = new XmlDocument();
                string resp = reader.ReadToEnd();
                doc.LoadXml(resp);
                var node = doc.GetElementsByTagName(elementName).Item(0);
                return node == null ? null : node.InnerText;
            }
        }

        #endregion

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
            error = null;
            bool success = true;
            if (restResponse == null)
            {
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

        private static void Backoff(int attempt)
        {
            Thread.Sleep((int)Math.Pow(2, attempt) * 100);
        }
    }
}