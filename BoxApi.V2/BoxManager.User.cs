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
        /// Returns the list of all users for the Enterprise with their user_id, public_name, and login if the user is an enterprise admin. If the user is not an admin, this request returns the current user’s user_id, public_name, and login.
        /// </summary>
        /// <param name="filterTerm"> </param>
        /// <param name="limit"> </param>
        /// <param name="offset"> </param>
        /// <returns></returns>
        public UserCollection GetUsers(string filterTerm = null, int? limit = null, int? offset = null, Field[] fields = null)
        {
            IRestRequest request = _requestHelper.GetUsers(filterTerm, limit, offset, fields);
            return _restClient.ExecuteAndDeserialize<UserCollection>(request);
        }

        public void GetUsers(Action<UserCollection> onSuccess, Action<Error> onFailure, string filterTerm = null, int? limit = null, int? offset = null, Field[] fields = null)
        {
            IRestRequest request = _requestHelper.GetUsers(filterTerm, limit, offset, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public User Get(User user, Field[] fields = null)
        {
            GuardFromNull(user, "user");
            return GetUser(user.Id, fields);
        }

        public User GetUser(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.GetUser(id, fields);
            return _restClient.ExecuteAndDeserialize<User>(request);
        }

        public void Get(Action<User> onSuccess, Action<Error> onFailure, User user, Field[] fields = null)
        {
            GuardFromNull(user, "user");
            GetUser(onSuccess, onFailure, user.Id, fields);
        }

        public void GetUser(Action<User> onSuccess, Action<Error> onFailure, string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.GetUser(id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(User user, bool notify = true, bool force = false)
        {
            GuardFromNull(user, "user");
            DeleteUser(user.Id, notify, force);
        }

        public void DeleteUser(string id, bool notify = true, bool force = false)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.DeleteUser(id, notify, force);
            _restClient.Execute(request);
        }

        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, User user, bool notify = true, bool force = false)
        {
            GuardFromNull(user, "user");
            DeleteUser(onSuccess, onFailure, user.Id, notify, force);
        }

        public void DeleteUser(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id, bool notify = true, bool force = false)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.DeleteUser(id, notify, force);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }


    }
}
