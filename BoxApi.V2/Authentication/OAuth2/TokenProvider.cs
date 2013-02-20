using System.Net;
using BoxApi.V2.Model.Enum;
using RestSharp;

namespace BoxApi.V2.Authentication.OAuth2
{
    public class TokenProvider
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly RequestHelper _requestHelper;
        private readonly BoxRestClient _restClient;

        public TokenProvider(string clientId, string clientSecret, IWebProxy webProxy = null, BoxManagerOptions options = BoxManagerOptions.None)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _requestHelper = new RequestHelper();
            _restClient = new BoxRestClient(null, webProxy, options);
        }

        public string GetAuthorizationUrl(string redirectUri = null)
        {
            IRestRequest request = _requestHelper.AuthorizationUrl(_clientId, redirectUri);
            return _restClient.BuildUri(request).AbsoluteUri;
        }

        public OAuthToken GetAccessToken(string authorizationCode, string redirectUri = null)
        {
            IRestRequest request = _requestHelper.GetAccessToken(_clientId, _clientSecret, authorizationCode, redirectUri);
            return _restClient.ExecuteAndDeserialize<OAuthToken>(request);
        }

        public OAuthToken RefreshAccessToken(string refreshToken)
        {
            IRestRequest request = _requestHelper.RefreshAccessToken(_clientId, _clientSecret, refreshToken);
            return _restClient.ExecuteAndDeserialize<OAuthToken>(request);
        }

        public void DestroyTokens(string token)
        {
            IRestRequest request = _requestHelper.DestroyTokens(_clientId, _clientSecret, token);
            _restClient.Execute(request);
        }
    }
}