using System;
using BoxApi.V2.Authentication.Common;
using RestSharp;

namespace BoxApi.V2.Authentication.OAuth2
{
    internal class OAuth2RequestAuthenticator : RequestAuthenticatorBase, IBoxAuthenticator
    {
        public OAuth2RequestAuthenticator(string accessToken) : base(accessToken)
        {
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", string.Format("Bearer {0}", AccessToken));
            if (HasSharedLink())
            {
                request.AddHeader("BoxApi", string.Format("shared_link={0}", SharedLink));
            }
        }
    }
}