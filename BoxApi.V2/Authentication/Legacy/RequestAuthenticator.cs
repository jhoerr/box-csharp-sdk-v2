using System.Text;
using BoxApi.V2.Authentication.Common;
using RestSharp;

namespace BoxApi.V2.Authentication.Legacy
{
    internal class RequestAuthenticator : RequestAuthenticatorBase, IBoxAuthenticator
    {
        private readonly string _applicationApiKey;

        public RequestAuthenticator(string applicationApiKey, string accessToken):base(accessToken)
        {
            _applicationApiKey = applicationApiKey;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var sb = new StringBuilder(string.Format("BoxAuth api_key={0}", _applicationApiKey));
            TryAddParameter(sb, "auth_token", AccessToken);
            if (HasSharedLink())
            {
                TryAddParameter(sb, "shared_link", SharedLink);
            }
            request.AddHeader("Authorization", sb.ToString());
        }
    }
}