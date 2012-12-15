using System;
using System.Text;
using RestSharp;

namespace BoxApi.V2
{
    internal class RequestAuthenticator : IBoxAuthenticator
    {
        private readonly string _applicationApiKey;
        private readonly string _authorizationToken;
        private string _sharedLink;

        public RequestAuthenticator(string applicationApiKey, string authorizationToken)
        {
            _applicationApiKey = applicationApiKey;
            _authorizationToken = authorizationToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            string header = HasSharedLink()
                              ? AuthenticateForSharedLink()
                              : AuthenticateForLegacy();
            request.AddHeader("Authorization", header);

        }

        public void SetSharedLink(string sharedLink)
        {
            _sharedLink = sharedLink;
        }

        public void ClearSharedLink()
        {
            _sharedLink = null;
        }

        private string AuthenticateForBearer()
        {
            return string.Format("Bearer {0}", _authorizationToken);
        }

        private string AuthenticateForLegacy()
        {
            var sb = new StringBuilder(string.Format("BoxAuth api_key={0}", _applicationApiKey));
            TryAddParameter(sb, "auth_token", _authorizationToken);
            return sb.ToString();
        }

        private string AuthenticateForSharedLink()
        {
            var sb = new StringBuilder(string.Format("BoxAuth api_key={0}", _applicationApiKey));
            TryAddParameter(sb, "auth_token", _authorizationToken);
            TryAddParameter(sb, "shared_link", _sharedLink);
            return sb.ToString();
        }

        private static void TryAddParameter(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sb.AppendFormat("&{0}={1}", label, value);
            }
        }

        private bool HasSharedLink()
        {
            return !string.IsNullOrWhiteSpace(_sharedLink);
        }
    }

    internal interface IBoxAuthenticator : IAuthenticator
    {
        void SetSharedLink(string sharedLink);
        void ClearSharedLink();
    }
}