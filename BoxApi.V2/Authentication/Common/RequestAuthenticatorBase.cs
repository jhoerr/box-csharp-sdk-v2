using System.Text;

namespace BoxApi.V2.Authentication.Common
{
    internal abstract class RequestAuthenticatorBase
    {
        protected string AccessToken;
        protected string SharedLink;

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

        protected static void TryAddParameter(StringBuilder sb, string label, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                sb.AppendFormat("&{0}={1}", label, value);
            }
        }

        protected bool HasSharedLink()
        {
            return !string.IsNullOrWhiteSpace(SharedLink);
        }
    }
}