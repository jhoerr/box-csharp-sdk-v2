using System;
using System.IO;
using System.Net;
using System.Threading;
using BoxApi.V2.Authentication.Legacy;
using BoxApi.V2.Authentication.OAuth2;

namespace BoxApi.V2
{
    /// <summary>
    ///     Provides methods for using the Box v2 API.  This class is not designed to be thread-safe.
    /// </summary>
    public partial class BoxManager
    {
        private readonly IWebProxy _proxy;
        private readonly RequestHelper _requestHelper;
        private readonly BoxRestClient _restClient;

        /// <summary>
        ///     Instantiates BoxManager
        /// </summary>
        /// <param name="clientId">The client ID for your Box application</param>
        /// <param name="clientSecret">The client secret for your Box application</param>
        /// <param name="accessToken"> The Access Token provided by Box for this User.</param>
        /// <param name="refreshToken"> The Refresh Token provided by Box for this User.</param>
        /// <param name="proxy">Proxy information</param>
        [Obsolete("Please use BoxManager(OAuth2RequestAuthenticator)")]
        public BoxManager(string clientId, string clientSecret, string accessToken = null, string refreshToken = null, IWebProxy proxy = null)
            : this(new OAuth2RequestAuthenticator(accessToken), proxy)
        {
        }

        /// <summary>
        ///     Creates a BoxManager client using the v2 authentication scheme
        /// </summary>
        /// <param name="requestAuthenticator">Generates v2 request authentication headers</param>
        /// <param name="proxy">HTTP proxy configuration information</param>
        public BoxManager(OAuth2RequestAuthenticator requestAuthenticator, IWebProxy proxy = null): this()
        {
            _restClient = new BoxRestClient(requestAuthenticator, proxy);
        }

        /// <summary>
        ///     Creates a BoxManager client using the v1 authentication scheme
        /// </summary>
        /// <param name="requestAuthenticator">Generates v1 request authentication headers</param>
        /// <param name="proxy">HTTP proxy configuration information</param>
        [Obsolete("Please transition to the v2 authentication scheme and use BoxManager(OAuth2RequestAuthenticator)")]
        public BoxManager(LegacyRequestAuthenticator requestAuthenticator, IWebProxy proxy = null) : this()
        {
            _restClient = new BoxRestClient(requestAuthenticator, proxy);
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

        private static void Backoff(int attempt)
        {
            Thread.Sleep((int) Math.Pow(2, attempt)*100);
        }

        private static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}