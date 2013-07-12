using System;
using System.IO;
using System.Net;
using System.Threading;
using BoxApi.V2.Authentication.Common;
using BoxApi.V2.Authentication.Legacy;
using BoxApi.V2.Authentication.OAuth2;
using BoxApi.V2.Model.Enum;
using RestSharp;

namespace BoxApi.V2
{
    /// <summary>
    ///     Provides methods for using the Box v2 API.  This class is not designed to be thread-safe.
    /// </summary>
    public partial class BoxManager : IEnterpriseManager
    {
        private readonly RequestHelper _requestHelper;
        private readonly BoxRestClient _restClient;
        private readonly BoxUploadClient _uploadClient;
        private readonly RestClient _downloadClient;

        /// <summary>
        ///     Creates a BoxManager client using the v2 authentication scheme
        /// </summary>
        /// <param name="oauth2AccessToken">The OAuth 2.0 access token for this user</param>
        /// <param name="proxy">HTTP proxy configuration information</param>
        /// <param name="options">Options to customize the behavior of the BoxManager</param>
        /// <param name="onBehalfOf">All operations will be performed on behalf of the user with this login.</param>
        public BoxManager(string oauth2AccessToken, IWebProxy proxy = null, BoxManagerOptions options = BoxManagerOptions.None, string onBehalfOf = null)
            : this(new OAuth2RequestAuthenticator(oauth2AccessToken), proxy, options, onBehalfOf)
        {
        }

        /// <summary>
        ///     Creates a BoxManager client using the v1 authentication scheme
        /// </summary>
        /// <param name="v1ApiKey">The v1 API key for your Box app</param>
        /// <param name="v1AuthToken">The Box user's v1 auth token</param>
        /// <param name="proxy">HTTP proxy configuration information</param>
        /// <param name="options">Options to customize the behavior of the BoxManager</param>
        [Obsolete("Please transition to the v2 authentication scheme and use BoxManager(oauth2AccessToken)")]
        public BoxManager(string v1ApiKey, string v1AuthToken, IWebProxy proxy = null, BoxManagerOptions options = BoxManagerOptions.None)
            : this(new LegacyRequestAuthenticator(v1ApiKey, v1AuthToken), proxy, options)
        {
        }

        private BoxManager(IRequestAuthenticator requestAuthenticator, IWebProxy proxy, BoxManagerOptions options, string onBehalfOf = null)
            : this()
        {
            requestAuthenticator.SetOnBehalfOf(onBehalfOf);
            _restClient = new BoxRestClient(requestAuthenticator, proxy, options);
            _uploadClient = new BoxUploadClient(requestAuthenticator, proxy, options);
            _downloadClient = new RestClient() { Authenticator = requestAuthenticator };
        }

        private BoxManager()
        {
            _requestHelper = new RequestHelper();
        }

        private static void GuardFromNull(object arg, string argName)
        {
            if (arg == null || (arg is string && string.IsNullOrEmpty((string) arg)))
            {
                throw new ArgumentException("Argument cannot be null or empty", argName);
            }
        }

        private static void GuardFromNullCallbacks(object onSuccess, object onFailure)
        {
            GuardFromNull(onSuccess, "onSuccess");
            GuardFromNull(onFailure, "onFailure");
        }

        /// <summary>
        /// Creates a new BoxManager from the existing one.
        /// </summary>
        public BoxManager Clone()
        {
            return new BoxManager((IRequestAuthenticator)_restClient.Authenticator, _restClient.Proxy, _restClient.Options);
        }
    }
}