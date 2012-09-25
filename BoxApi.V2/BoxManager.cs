using System;
using System.Net;
using System.Threading;
using BoxApi.V2.Model;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{
    /// <summary>
    ///   Provides methods for using Box.NET SOAP web service
    /// </summary>
    public partial class BoxManager  
    {
        private readonly BoxRestClient _restClient;
        private readonly RequestHelper _requestHelper;

        /// <summary>
        ///   Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The unique API key which is assigned to application </param>
        /// <param name="authorizationToken"> Valid authorization ticket </param>
        /// <param name="proxy"> Proxy information </param>
        public BoxManager(string applicationApiKey, string authorizationToken, IWebProxy proxy = null)
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

        private static void Backoff(int attempt)
        {
            Thread.Sleep((int)Math.Pow(2, attempt) * 100);
        }
    }
}