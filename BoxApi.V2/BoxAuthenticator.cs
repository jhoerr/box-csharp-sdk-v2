using System;
using System.Net;
using BoxApi.V2.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{
    /// <summary>
    /// Provides methods for generating tickets and user authorization tokens
    /// </summary>
    public class BoxAuthenticator
    {
        private readonly RequestHelper _requestHelper;
        private readonly BoxRestClient _restClient;

        /// <summary>
        /// A web proxy that should be used in conjunction with any calls to Box.
        /// </summary>
        public WebProxy Proxy { get; private set; }

        /// <summary>
        /// The API key for your application
        /// </summary>
        public string ApiKey { get; private set; }
        
        /// <summary>
        /// The ticket associated with the current authorization token request
        /// </summary>
        public string Ticket { get; private set; }

        /// <summary>
        /// The authorization token 
        /// </summary>
        public string AuthToken { get; private set; }

        /// <summary>
        /// Instantiates a BoxAuthenticator
        /// </summary>
        /// <param name="apiKey">The API key for your application</param>
        /// <param name="ticket">The ticket associated with the current authorization token request, if you already have one.</param>
        /// <param name="proxy">An optional web proxy that should be used in conjunction with any calls to Box</param>
        public BoxAuthenticator(string apiKey, string ticket = null, WebProxy proxy = null)
        {
            ApiKey = apiKey;
            Ticket = ticket;
            Proxy = proxy;
            _requestHelper = new RequestHelper();
            _restClient = new BoxRestClient(new RequestAuthenticator(ApiKey, null), proxy);
        }

        private void SetTicket()
        {
            var request = _requestHelper.GetTicket(ApiKey);
            var boxTicket = _restClient.ExecuteAndDeserialize<BoxTicket>(request);
            Ticket = boxTicket.Ticket;
        }

        /// <summary>
        /// Generates a Box-pointing URL to which a user can be redirected for authentication
        /// </summary>
        /// <returns>The URL</returns>
        /// <remarks>A Ticket is requested from Box and locally set as a result of this call.</remarks>
        public string GetAuthorizationUrl()
        {
            SetTicket();
            var request = _requestHelper.AuthorizationUrl(Ticket);
            return _restClient.BuildUri(request).AbsoluteUri;
        }

        /// <summary>
        /// Swaps the existing ticket for an authorization token that can be used by your application to access the user's Box.  This can only be performed after the user has authenticated at Box and approved your application to access their data.
        /// </summary>
        /// <returns>The user's new authorization token</returns>
        public string GetAuthorizationToken()
        {
            if (AuthToken == null)
            {
                if(Ticket == null)
                {
                    throw new Exception("You must retrieve and approve a ticket before exchanging it for an authorization token");
                }
                var request = _requestHelper.SwapTicketForToken(ApiKey, Ticket);
                var token = _restClient.ExecuteAndDeserialize<BoxAuthToken>(request);
                return token.AuthToken;
            }
            return AuthToken;
        }

        /// <summary>
        /// Create a sandboxed authentication token and corresponding folder for a user.  The user does not need to previously have a registered Box account.
        /// </summary>
        /// <param name="email">The sanboxed user's email address</param>
        /// <returns>A sandboxed authentication token</returns>
        public string GetAppAuthTokenForUser(string email)
        {
            if (AuthToken == null)
            {
                var request = _requestHelper.CreateToken(email);
                var token = _restClient.ExecuteAndDeserialize<BoxToken>(request);
                AuthToken = token.Token;
            }
            return AuthToken;
        }
    }
}