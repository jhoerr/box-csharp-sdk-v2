using System;
using BoxApi.V2.Authentication.Common;
using RestSharp;

namespace BoxApi.V2.Authentication.OAuth2
{
    /// <summary>
    /// Adds v2 (OAuth 2.0) authentication headers to HTTP requests
    /// </summary>
    public class OAuth2RequestAuthenticator : RequestAuthenticatorBase, IRequestAuthenticator
    {
        /// <summary>
        /// Instantiates a request authenticator
        /// </summary>
        /// <param name="accessToken">The Box user's OAuth 2.0 access token.</param>
        public OAuth2RequestAuthenticator(string accessToken) : base(accessToken)
        {
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            if (Has(SharedLink))
            {
                request.AddHeader("BoxApi", string.Format("shared_link={0}", SharedLink));
            }
            if (Has(OnBehalfOf))
            {
                request.AddHeader("On-Behalf-Of", OnBehalfOf);
            }
        }
    }
}