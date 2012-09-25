using System;
using System.Net;
using BoxApi.V2.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{
    public class BoxAuthenticator
    {
        private readonly RequestHelper _requestHelper;
        private readonly BoxRestClient _restClient;

        public WebProxy Proxy { get; private set; }
        public string ApiKey { get; private set; }
        public string Ticket { get; private set; }
        public string AuthToken { get; private set; }


        public BoxAuthenticator(string apiKey, string ticket = null, WebProxy proxy = null)
        {
            ApiKey = apiKey;
            Ticket = ticket;
            Proxy = proxy;
            _requestHelper = new RequestHelper();
            _restClient = new BoxRestClient(null, proxy);
        }

        private void SetTicket()
        {
            var request = _requestHelper.GetTicket(ApiKey);
            var boxTicket = _restClient.ExecuteAndDeserialize<BoxTicket>(request);
            Ticket = boxTicket.Ticket;
        }

        public string GetAuthorizationUrl()
        {
            SetTicket();
            var request = _requestHelper.AuthorizationUrl(Ticket);
            return _restClient.BuildUri(request).AbsoluteUri;
        }

        public string GetAuthorizationToken()
        {
            if (AuthToken == null)
            {
                if(Ticket == null)
                {
                    throw new Exception("You must retrive and approve a ticket before exchanging it for an authorization token");
                }
                var request = _requestHelper.SwapTicketForToken(ApiKey, Ticket);
                var token = _restClient.ExecuteAndDeserialize<BoxAuthToken>(request);
                AuthToken = token.AuthToken;
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
                var token = _restClient.ExecuteAndDeserialize<BoxToken>(restRequest);
                AuthToken = token.Token;
            }
            return AuthToken;
        }
    }
}