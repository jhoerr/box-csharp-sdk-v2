using System;
using System.Diagnostics.Contracts;
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
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IWebProxy _proxy;
        private readonly RequestHelper _requestHelper;
        private string _refreshToken;
        private BoxRestClient _restClient;

        /// <summary>
        ///     Instantiates BoxManager
        /// </summary>
        /// <param name="clientId">The client ID for your Box application</param>
        /// <param name="clientSecret">The client secret for your Box application</param>
        /// <param name="accessToken"> The Access Token provided by Box for this User.</param>
        /// <param name="refreshToken"> The Refresh Token provided by Box for this User.</param>
        /// <param name="proxy">Proxy information</param>
        public BoxManager(string clientId, string clientSecret, string accessToken = null, string refreshToken = null, IWebProxy proxy = null) : this(proxy)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            ConfigureRestClient(accessToken, refreshToken);
        }

        /// <summary>
        ///     Instantiates BoxManager
        /// </summary>
        /// <param name="apiKey"> The API key for your Box application</param>
        /// <param name="authToken"> The Authorization Token provided by Box for this User.</param>
        /// <param name="proxy">Proxy information</param>
        [Obsolete("This uses the deprecated v1 authentication scheme.  Please transition to the v2 scheme (OAuth2), which is supported by the other BoxManager constructor")]
        public BoxManager(string apiKey, string authToken, IWebProxy proxy) : this(proxy)
        {
            _restClient = new BoxRestClient(new RequestAuthenticator(apiKey, authToken), proxy);
        }

        private BoxManager(IWebProxy proxy)
        {
            _proxy = proxy;
            _requestHelper = new RequestHelper();
        }

        private void ConfigureRestClient(string accessToken, string refreshToken)
        {
            _refreshToken = refreshToken;
            _restClient = new BoxRestClient(new OAuth2RequestAuthenticator(accessToken), _proxy);
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