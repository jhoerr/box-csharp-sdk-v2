using System.Text;
using RestSharp;

namespace BoxApi.V2.Authentication.Common
{
    public abstract class RequestAuthenticatorBase
    {
        protected readonly string AccessToken;
        protected string SharedLink;
        protected string OnBehalfOf;

        protected RequestAuthenticatorBase(string accessToken)
        {
            AccessToken = accessToken;
        }

        public void SetSharedLink(string sharedLink)
        {
            SharedLink = sharedLink;
        }

        public void ClearSharedLink()
        {
            SharedLink = null;
        }

        public void SetOnBehalfOf(string userLogin)
        {
            OnBehalfOf = userLogin;
        }

        protected static void TryAddParameter(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sb.AppendFormat("&{0}={1}", label, value);
            }
        }

        protected bool Has(string header)
        {
            return !string.IsNullOrWhiteSpace(header);
        }
    }
}