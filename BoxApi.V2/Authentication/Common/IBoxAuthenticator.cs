using RestSharp;

namespace BoxApi.V2.Authentication.Common
{
    internal interface IBoxAuthenticator : IAuthenticator
    {
        void SetSharedLink(string sharedLink);
        void ClearSharedLink();
    }
}