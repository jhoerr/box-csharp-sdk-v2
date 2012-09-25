using RestSharp;

namespace BoxApi.V2
{
    public class RequestAuthenticator : IAuthenticator
    {
        private readonly string _applicationApiKey;
        private readonly string _authorizationToken;

        public RequestAuthenticator(string applicationApiKey, string authorizationToken)
        {
            _applicationApiKey = applicationApiKey;
            _authorizationToken = authorizationToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", string.Format("BoxAuth api_key={0}&auth_token={1}", _applicationApiKey, _authorizationToken));
        }
    }
}