using System.Text;
using BoxApi.V2.Authentication.Common;
using RestSharp;

namespace BoxApi.V2.Authentication.Legacy
{
    /// <summary>
    /// Adds v1 authentication headers to HTTP requests
    /// </summary>
    public class LegacyRequestAuthenticator : RequestAuthenticatorBase, IRequestAuthenticator
    {
        private readonly string _applicationApiKey;

        /// <summary>
        /// Instantiates a request authenticator
        /// </summary>
        /// <param name="applicationApiKey">The Box application's API key</param>
        /// <param name="accessToken">The Box user's v1 access token</param>
        public LegacyRequestAuthenticator(string applicationApiKey, string accessToken):base(accessToken)
        {
            _applicationApiKey = applicationApiKey;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            var sb = new StringBuilder(string.Format("BoxAuth api_key={0}", _applicationApiKey));
            TryAddParameter(sb, "auth_token", AccessToken);
            if (Has(SharedLink))
            {
                TryAddParameter(sb, "shared_link", SharedLink);
            }
            request.AddHeader("Authorization", sb.ToString());
        }
    }
}