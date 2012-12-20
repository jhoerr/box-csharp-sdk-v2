using System;
using BoxApi.V2.Authentication.Legacy;
using BoxApi.V2.Authentication.OAuth2;
using BoxApi.V2.Model;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     Create a sandboxed authentication token and corresponding folder for a user.  The user does not need to previously have a registered Box account.
        /// </summary>
        /// <param name="emailAddress">The sanboxed user's email address</param>
        /// <returns>A sandboxed authentication token</returns>
        public BoxToken CreateToken(string emailAddress)
        {
            var request = _requestHelper.CreateToken(emailAddress);
            return _restClient.ExecuteAndDeserialize<BoxToken>(request);
        }

        /// <summary>
        ///     Create a sandboxed authentication token and corresponding folder for a user.  The user does not need to previously have a registered Box account.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the sandboxed authentication token</param>
        /// <param name="onFailure">Action to perform following a failed Token operation</param>
        /// <param name="emailAddress">The user's email address</param>
        public void CreateToken(Action<BoxToken> onSuccess, Action<Error> onFailure, string emailAddress)
        {
            var request = _requestHelper.CreateToken(emailAddress);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
        
        public OAuthToken RefreshAccessToken()
        {
            var authenticator = new TokenProvider(_clientId, _clientSecret, _proxy);
            var refreshAccessToken = authenticator.RefreshAccessToken(_refreshToken);
            ConfigureRestClient(refreshAccessToken.AccessToken, refreshAccessToken.RefreshToken);
            return refreshAccessToken;
        }
    }
}