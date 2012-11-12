using System.Text;
using RestSharp;

namespace BoxApi.V2
{
    public class RequestAuthenticator : IBoxAuthenticator
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
            var sb = new StringBuilder(string.Format("BoxAuth api_key={0}", _applicationApiKey));
            TryAddParameter(sb, "auth_token", _authorizationToken);
            TryAddParameter(sb, "shared_link", _sharedLink);
            request.AddHeader("Authorization", sb.ToString());
        }

        private static void TryAddParameter(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sb.AppendFormat("&{0}={1}", label, value);
            }
        }

        public void SetSharedLink(string sharedLink)
        {
            _sharedLink = sharedLink;
        }
        
        public void ClearSharedLink()
        {
            _sharedLink = null;
        }
    }

    public interface IBoxAuthenticator : IAuthenticator
    {
        void SetSharedLink(string sharedLink);
        void ClearSharedLink();
    }
}