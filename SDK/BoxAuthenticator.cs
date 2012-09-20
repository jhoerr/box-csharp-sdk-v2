using System;
using System.Net;
using BoxApi.V2.SDK.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2.SDK
{
    public class BoxAuthenticator
    {
        private string _serviceUrl = "https://www.box.com/api/";
        private RequestHelper _requestHelper;

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