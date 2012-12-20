using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace BoxApi.V2.Authentication.Legacy
{
    public class RequestHelper
    {
        public IRestRequest GetTicket(string apiKey)
        {
            var restRequest = new RestRequest("1.0/rest");
            restRequest.AddParameter("action", "get_ticket");
            restRequest.AddParameter("api_key", apiKey);
            return restRequest;
        }

        public IRestRequest AuthorizationUrl(string ticket)
        {
            var restRequest = new RestRequest("1.0/auth/{ticket}");
            restRequest.AddUrlSegment("ticket", ticket);
            return restRequest;
        }

        public IRestRequest SwapTicketForToken(string apiKey, string ticket)
        {
            var restRequest = new RestRequest("1.0/rest");
            restRequest.AddParameter("action", "get_auth_token");
            restRequest.AddParameter("api_key", apiKey);
            restRequest.AddParameter("ticket", ticket);
            return restRequest;
        }
    }
}
