using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        private string _token;
        private readonly RestClient _restContentClient;
        private readonly RestClient _restAuthorizationClient;
        private readonly RequestHelper _requestHelper;

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
            _restAuthorizationClient = new RestClient {Proxy = proxy};
            _restAuthorizationClient.ClearHandlers();
            _restAuthorizationClient.AddHandler("*", new XmlDeserializer());

            _restContentClient = new RestClient(_serviceUrl) {Proxy = proxy, Authenticator = new BoxAuthenticator(applicationApiKey, authorizationToken)};
            _restContentClient.ClearHandlers();
            _restContentClient.AddHandler(JsonMimeType, new JsonDeserializer());

            _requestHelper = new RequestHelper("1.0", "2.0");
        }

        #region GetAuthenticationToken

        /// <summary>
        ///   Gets authentication token required for communication between Box.NET service and user's application.
        ///   Method has to be called after the user has authorized themself on Box.NET site
        /// </summary>
        /// <param name="authenticationTicket"> Athentication ticket </param>
        /// <param name="authenticationToken"> Authentication token </param>
        /// <param name="authenticatedUser"> Authenticated user account information </param>
        /// <returns> Operation result </returns>
        public GetAuthenticationTokenStatus GetAuthenticationToken(
            string authenticationTicket,
            out string authenticationToken,
            out User authenticatedUser)
        {
            SOAPUser user;

            var result = _service.get_auth_token(_apiKey, authenticationTicket, out authenticationToken, out user);

            authenticatedUser = user == null ? null : new User(user);
            _token = authenticationToken;

            return StatusMessageParser.ParseGetAuthenticationTokenStatus(result);
        }

        /// <summary>
        ///   Gets authentication token required for communication between Box.NET service and user's application.
        ///   Method has to be called after the user has authorized themself on Box.NET site
        /// </summary>
        /// <param name="authenticationTicket"> Athentication ticket </param>
        /// <param name="getAuthenticationTokenCompleted"> Call back method which will be invoked when operation completes </param>
        /// <exception cref="ArgumentNullException">Thrown if
        ///   <paramref name="getAuthenticationTokenCompleted" />
        ///   is null</exception>
        public void GetAuthenticationToken(
            string authenticationTicket,
            OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted)
        {
            GetAuthenticationToken(authenticationTicket, getAuthenticationTokenCompleted, null);
        }

        /// <summary>
        ///   Gets authentication token required for communication between Box.NET service and user's application.
        ///   Method has to be called after the user has authorized themself on Box.NET site
        /// </summary>
        /// <param name="authenticationTicket"> Athentication ticket </param>
        /// <param name="getAuthenticationTokenCompleted"> Callback method which will be invoked when operation completes </param>
        /// <param name="userState"> A user-defined object containing state information. This object is passed to the <paramref
        ///    name="getAuthenticationTokenCompleted" /> delegate as a part of response when the operation is completed </param>
        /// <exception cref="ArgumentNullException">Thrown if
        ///   <paramref name="getAuthenticationTokenCompleted" />
        ///   is null</exception>
        public void GetAuthenticationToken(
            string authenticationTicket,
            OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted,
            object userState)
        {
            // ThrowIfParameterIsNull(getAuthenticationTokenCompleted, "getAuthenticationTokenCompleted");

            _service.get_auth_tokenCompleted += GetAuthenticationTokenFinished;

            object[] state = {getAuthenticationTokenCompleted, userState};

            _service.get_auth_tokenAsync(_apiKey, authenticationTicket, state);
        }

        private void GetAuthenticationTokenFinished(object sender, get_auth_tokenCompletedEventArgs e)
        {
            var state = (object[]) e.UserState;
            Exception error = null;

            var getAuthenticationTokenCompleted =
                (OperationFinished<GetAuthenticationTokenResponse>) state[0];

            GetAuthenticationTokenStatus status;

            if (e.Error == null)
            {
                status = StatusMessageParser.ParseGetAuthenticationTokenStatus(e.Result);

                if (status == GetAuthenticationTokenStatus.Unknown)
                {
                    error = new Exception(e.Result);
                }
            }
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
        }

        #endregion

        #region GetTicket

        /// <summary>
        ///   Gets ticket which is used to generate an authentication page 
        ///   for the user to login
        /// </summary>
        /// <param name="authenticationTicket"> Authentication ticket </param>
        /// <returns> Operation status </returns>
        public GetTicketStatus GetTicket(out string authenticationTicket)
        {
            var result = _service.get_ticket(_apiKey, out authenticationTicket);

            return StatusMessageParser.ParseGetTicketStatus(result);
        }

        /// <summary>
        ///   Gets ticket which is used to generate an authentication page 
        ///   for the user to login
        /// </summary>
        /// <param name="getAuthenticationTicketCompleted"> Call back method which will be invoked when operation completes </param>
        /// <exception cref="ArgumentException">Thrown if
        ///   <paramref name="getAuthenticationTicketCompleted" />
        ///   is
        ///   <c>null</c>
        /// </exception>
        public void GetTicket(OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted)
        {
            GetTicket(getAuthenticationTicketCompleted, null);
        }

        /// <summary>
        ///   Gets ticket which is used to generate an authentication page 
        ///   for the user to login
        /// </summary>
        /// <param name="getAuthenticationTicketCompleted"> Call back method which will be invoked when operation completes </param>
        /// <param name="userState"> A user-defined object containing state information. This object is passed to the <paramref
        ///    name="getAuthenticationTicketCompleted" /> delegate as a part of response when the operation is completed </param>
        /// <exception cref="ArgumentException">Thrown if
        ///   <paramref name="getAuthenticationTicketCompleted" />
        ///   is
        ///   <c>null</c>
        /// </exception>
        public void GetTicket(
            OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted,
            object userState)
        {
            // ThrowIfParameterIsNull(getAuthenticationTicketCompleted, "getAuthenticationTicketCompleted");

            object[] data = {getAuthenticationTicketCompleted, userState};

            _service.get_ticketCompleted += GetTicketFinished;

            _service.get_ticketAsync(_apiKey, data);
        }


        private void GetTicketFinished(object sender, get_ticketCompletedEventArgs e)
        {
            var data = (object[]) e.UserState;
            var getAuthenticationTicketCompleted = (OperationFinished<GetTicketResponse>) data[0];
            GetTicketResponse response;

            if (e.Error != null)
            {
                response = new GetTicketResponse
                    {
                        Status = GetTicketStatus.Failed,
                        UserState = data[1],
                        Error = e.Error
                    };
            }
            else
            {
                var status = StatusMessageParser.ParseGetTicketStatus(e.Result);

                var error = status == GetTicketStatus.Unknown
                                ? new Exception(e.Result)
                                : null;

                response = new GetTicketResponse
                    {
                        Status = status,
                        Ticket = e.ticket,
                        UserState = data[1],
                        Error = error
                    };
            }

            getAuthenticationTicketCompleted(response);
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