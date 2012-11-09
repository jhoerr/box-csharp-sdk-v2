using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Model;
using RestSharp;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        /// Create a sandboxed authentication token and corresponding folder for a user.  The user does not need to previously have a registered Box account.
        /// </summary>
        /// <param name="emailAddress">The sanboxed user's email address</param>
        /// <returns>A sandboxed authentication token</returns>
        public BoxToken CreateToken(string emailAddress)
        {
            IRestRequest request = _requestHelper.CreateToken(emailAddress);
            return _restClient.ExecuteAndDeserialize<BoxToken>(request);
        }

        /// <summary>
        /// Create a sandboxed authentication token and corresponding folder for a user.  The user does not need to previously have a registered Box account.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the sandboxed authentication token</param>
        /// <param name="onFailure">Action to perform following a failed Token operation</param>
        /// <param name="emailAddress">The user's email address</param>
        public void CreateToken(Action<BoxToken> onSuccess, Action<Error> onFailure, string emailAddress)
        {
            IRestRequest request = _requestHelper.CreateToken(emailAddress);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}
