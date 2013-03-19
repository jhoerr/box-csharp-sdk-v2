using RestSharp;

namespace BoxApi.V2.Authentication.Common
{
    internal interface IRequestAuthenticator : IAuthenticator
    {
        void SetSharedLink(string sharedLink);
        void ClearSharedLink();
        void SetOnBehalfOf(string userLogin);
    }
}