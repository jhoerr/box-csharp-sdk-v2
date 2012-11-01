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
        public BoxToken CreateToken(string emailAddress)
        {
            IRestRequest request = _requestHelper.CreateToken(emailAddress);
            return _restClient.ExecuteAndDeserialize<BoxToken>(request);
        }

        public void CreateToken(Action<BoxToken> onSuccess, Action<Error> onFailure, string emailAddress)
        {
            IRestRequest request = _requestHelper.CreateToken(emailAddress);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}
