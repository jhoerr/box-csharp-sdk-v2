using System;
using System.IO;
using System.Net;
using System.Threading;

namespace BoxApi.V2
{
    /// <summary>
    ///     Provides methods for using Box v2 API.  This class is not designed to be thread-safe.
    /// </summary>
    public partial class BoxManager
    {
        private readonly BoxRestClient _restClient;
        private readonly RequestHelper _requestHelper;

        /// <summary>
        ///     Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The API key which is assigned to your application</param>
        /// <param name="authorizationToken"> The user's authorization token.  This can be null if you are exclusively consuming shared items.</param>
        /// <param name="proxy">Proxy information</param>
        public BoxManager(string applicationApiKey, string authorizationToken = null, IWebProxy proxy = null)
        {
            _requestHelper = new RequestHelper();
            _restClient = new BoxRestClient(new RequestAuthenticator(applicationApiKey, authorizationToken), proxy);
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