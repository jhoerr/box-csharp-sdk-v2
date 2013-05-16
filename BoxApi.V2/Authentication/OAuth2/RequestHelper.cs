using RestSharp;

namespace BoxApi.V2.Authentication.OAuth2
{
    internal class RequestHelper
    {
        public IRestRequest AuthorizationUrl(string clientId, string redirectUri = null)
        {
            var restRequest = new RestRequest("oauth2/authorize", Method.GET);
            restRequest.AddParameter("response_type", "code");
            restRequest.AddParameter("client_id", clientId);
            restRequest.AddParameter("state", "authenticated");
            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                restRequest.AddParameter("redirect_uri", redirectUri);
            }
            return restRequest;
        }

        public IRestRequest GetAccessToken(string clientId, string clientSecret, string authorizationCode, string redirectUri=null)
        {
            var restRequest = new RestRequest("oauth2/token", Method.POST);
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("code", authorizationCode);
            restRequest.AddParameter("client_id", clientId);
            restRequest.AddParameter("client_secret", clientSecret);
            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                restRequest.AddParameter("redirect_uri", redirectUri);
            }
            return restRequest;
        }

        public IRestRequest RefreshAccessToken(string clientId, string clientSecret, string refreshToken)
        {
            var restRequest = new RestRequest("oauth2/token", Method.POST);
            restRequest.AddParameter("grant_type", "refresh_token");
            restRequest.AddParameter("refresh_token", refreshToken);
            restRequest.AddParameter("client_id", clientId);
            restRequest.AddParameter("client_secret", clientSecret);
            return restRequest;
        }

        public IRestRequest DestroyTokens(string clientId, string clientSecret, string token)
        {
            var restRequest = new RestRequest("oauth2/revoke", Method.POST);
            restRequest.AddParameter("client_id", clientId);
            restRequest.AddParameter("client_secret", clientSecret);
            restRequest.AddParameter("token", token);
            return restRequest;
        }
    }
}