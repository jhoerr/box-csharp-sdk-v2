using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;
using RestSharp;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated. 
        /// </summary>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>Returns a single complete user object. An error is returned if a valid auth token is not included in the API request.</returns>
        public User Me(IEnumerable<UserField> fields = null)
        {
            var request = _requestHelper.Me(fields);
            return _restClient.ExecuteAndDeserialize<User>(request);
        }

        /// <summary>
        ///     Retrieves information about the user who is currently logged in i.e. the user for whom this auth token was generated. 
        /// </summary>
        /// <param name="onSuccess">Action to perform with the current user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Me(Action<User> onSuccess, Action<Error> onFailure, IEnumerable<UserField> fields = null)
        {
            var request = _requestHelper.Me(fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     For an individual user, this provides their own user information and content. For an enterprise admin, this provides information on all users in the admin’s enterprise.
        /// </summary>
        /// <param name="filterTerm">Only users whose name or login starts with this value will be returned.  Default is no filter.</param>
        /// <param name="limit">The number of records to return. If no filterTerm, limit, or offset are supplied, all user records are returned.  </param>
        /// <param name="offset">The record at which to start (used in conjunction with 'limit'.)  Default is the first record.</param>
        /// <returns>A collection of users matching the supplied filtering criteria</returns>
        public EnterpriseUserCollection GetUsers(string filterTerm = null, int? limit = null, int? offset = null)
        {
            var request = _requestHelper.GetUsers(filterTerm, limit, offset);
            return _restClient.ExecuteAndDeserialize<EnterpriseUserCollection>(request);
        }

        /// <summary>
        ///     For an individual user, this provides their own user information and content. For an enterprise admin, this provides information on all users in the admin’s enterprise.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the returned users</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="filterTerm">Only users whose name or login starts with this value will be returned.  Default is no filter.</param>
        /// <param name="limit">The number of records to return. Default is all users.</param>
        /// <param name="offset">The record at which to start (used in conjunction with 'limit'.)  Default is the first record.</param>
        public void GetUsers(Action<EnterpriseUserCollection> onSuccess, Action<Error> onFailure, string filterTerm = null, int? limit = null, int? offset = null)
        {
            var request = _requestHelper.GetUsers(filterTerm, limit, offset);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="user">The user to retrieve</param>
        /// <returns>The requested user</returns>
        public EnterpriseUser Get(EnterpriseUser user)
        {
            GuardFromNull(user, "user");
            return GetUser(user.Id);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <returns>The requested user</returns>
        public EnterpriseUser GetUser(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetUser(id);
            return _restClient.ExecuteAndDeserialize<EnterpriseUser>(request);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user to retrieve</param>
        public void Get(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user)
        {
            GuardFromNull(user, "user");
            GetUser(onSuccess, onFailure, user.Id);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="id">The ID of the user to retrieve</param>
        public void GetUser(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetUser(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Used to provision a new user in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="user">The properties to set for the new user.  Name and Login are required.  Warning -- Box tracks a user's space amount in GB, so if you choose to specify the SpaceAmount, it must be at least 1 GB (2^30).</param>
        /// <returns>The new user</returns>
        public EnterpriseUser CreateUser(EnterpriseUser user)
        {
            GuardFromNull(user, "user");
            GuardFromNull(user.Name, "user.Name");
            GuardFromNull(user.Login, "user.Login");
            var request = _requestHelper.CreateUser(user);
            return _restClient.ExecuteAndDeserialize<EnterpriseUser>(request);
        }

        /// <summary>
        /// Used to provision a new user in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The properties to set for the new user.  Name and Login are required.  Warning -- Box tracks a user's space amount in GB, so if you choose to specify the SpaceAmount, it must be at least 1 GB (2^30).</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new user</returns>
        public void CreateUser(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user, EnterpriseUserField[] fields = null)
        {
            GuardFromNull(user, "user");
            GuardFromNull(user.Name, "user.Name");
            GuardFromNull(user.Login, "user.Login");
            var request = _requestHelper.CreateUser(user);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Updates the settings and information about a user. This method only works for enterprise admins
        /// </summary>
        /// <param name="user">The user with udpated information</param>
        /// <returns>A user with the updated information</returns>
        public EnterpriseUser UpdateUser(EnterpriseUser user)
        {
            var request = _requestHelper.UpdateUser(user);
            return _restClient.ExecuteAndDeserialize<EnterpriseUser>(request);
        }

        /// <summary>
        /// Updates the settings and information about a user. This method only works for enterprise admins
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user with udpated information</param>
        /// <returns>A user with the updated information</returns>
        public void UpdateUser(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user)
        {
            var request = _requestHelper.UpdateUser(user);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="user">The user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        public void Delete(EnterpriseUser user, bool notify = true, bool force = false)
        {
            GuardFromNull(user, "user");
            DeleteUser(user.Id, notify, force);
        }

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        public void DeleteUser(string id, bool notify = true, bool force = false)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteUser(id, notify, force);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful deletion</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        public void Delete(Action onSuccess, Action<Error> onFailure, EnterpriseUser user, bool notify = true, bool force = false)
        {
            GuardFromNull(user, "user");
            DeleteUser(onSuccess, onFailure, user.Id, notify, force);
        }

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful deletion</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="id">The ID of the user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        public void DeleteUser(Action onSuccess, Action<Error> onFailure, string id, bool notify = true, bool force = false)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteUser(id, notify, force);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="currentOwner">The user whose files will be moved</param>
        /// <param name="newOwner">The user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        /// <returns>The folder as it now exists in the newOwner's Box</returns>
        public Folder MoveRootFolderToAnotherUser(EnterpriseUser currentOwner, EnterpriseUser newOwner, bool notify = true)
        {
            GuardFromNull(currentOwner, "currentOwner");
            GuardFromNull(newOwner, "newOwner");
            return MoveRootFolderToAnotherUser(currentOwner.Id, newOwner.Id, notify);
        }

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="currentOwnerId">The ID of the user whose files will be moved</param>
        /// <param name="newOwnerId">The ID of the user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        /// <returns></returns>
        public Folder MoveRootFolderToAnotherUser(string currentOwnerId, string newOwnerId, bool notify = true)
        {
            GuardFromNull(currentOwnerId, "currentOwnerId");
            GuardFromNull(newOwnerId, "newOwnerId");
            var request = _requestHelper.MoveFolderToAnotherUser(currentOwnerId, Folder.Root, newOwnerId, notify);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="currentOwner">The user whose files will be moved</param>
        /// <param name="newOwner">The user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        public void MoveRootFolderToAnotherUser(Action<Folder> onSuccess, Action<Error> onFailure, EnterpriseUser currentOwner, EnterpriseUser newOwner, bool notify = true)
        {
            GuardFromNull(currentOwner, "currentOwner");
            GuardFromNull(newOwner, "newOwner");
            MoveRootFolderToAnotherUser(onSuccess, onFailure, currentOwner.Id, newOwner.Id, notify);
        }

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="currentOwnerId">The ID of the user whose files will be moved</param>
        /// <param name="newOwnerId">The ID of the user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        public void MoveRootFolderToAnotherUser(Action<Folder> onSuccess, Action<Error> onFailure, string currentOwnerId, string newOwnerId, bool notify = true)
        {
            GuardFromNull(currentOwnerId, "currentOwnerId");
            GuardFromNull(newOwnerId, "newOwnerId");
            var request = _requestHelper.MoveFolderToAnotherUser(currentOwnerId, Folder.Root, newOwnerId, notify);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="user">The user for whom to retrieve the email aliases</param>
        /// <returns>A collection of email aliases</returns>
        public EmailAliasCollection GetEmailAliases(EnterpriseUser user)
        {
            GuardFromNull(user, "user");
            return GetEmailAliases(user.Id);
        }

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="userId">The ID of the user for whom to retrieve the email aliases</param>
        /// <returns>A collection of email aliases</returns>
        public EmailAliasCollection GetEmailAliases(string userId)
        {
            GuardFromNull(userId, "userId");
            IRestRequest request = _requestHelper.GetEmailAliases(userId);
            return _restClient.ExecuteAndDeserialize<EmailAliasCollection>(request);
        }

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="onSuccess">An action to perfrom with the email aliases</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="user">The user for whom to retrieve the email aliases</param>
        public void GetEmailAliases(Action<EmailAliasCollection> onSuccess, Action<Error> onFailure, EnterpriseUser user)
        {
            GuardFromNull(user, "user");
            GetEmailAliases(onSuccess, onFailure, user.Id);
        }

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="onSuccess">An action to perfrom with the email aliases</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the user for whom to retrieve the email aliases</param>
        public void GetEmailAliases(Action<EmailAliasCollection> onSuccess, Action<Error> onFailure, string userId)
        {
            GuardFromNull(userId, "userId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.GetEmailAliases(userId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="user">The user to alias</param>
        /// <param name="alias">The email alias to add </param>
        /// <returns>The updated user</returns>
        public EmailAlias AddEmailAlias(EnterpriseUser user, string alias)
        {
            GuardFromNull(user, "user");
            return AddEmailAlias(user.Id, alias);
        }

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="userId">The ID of the user to alias</param>
        /// <param name="alias">The email alias to add </param>
        /// <returns>The updated user</returns>
        public EmailAlias AddEmailAlias(string userId, string alias)
        {
            GuardFromNull(userId, "userId");
            GuardFromNull(alias, "alias");
            IRestRequest request = _requestHelper.AddAlias(userId, alias);
            return _restClient.ExecuteAndDeserialize<EmailAlias>(request);
        }

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation </param>
        /// <param name="user">The user to alias</param>
        /// <param name="alias">The email alias to add </param>
        public void AddEmailAlias(Action<EmailAlias> onSuccess, Action<Error> onFailure, EnterpriseUser user, string alias)
        {
            GuardFromNull(user, "user");
            AddEmailAlias(onSuccess, onFailure, user.Id, alias);
        }

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the user to alias</param>
        /// <param name="alias">The email alias to add </param>
        public void AddEmailAlias(Action<EmailAlias> onSuccess, Action<Error> onFailure, string userId, string alias)
        {
            GuardFromNull(userId, "userId");
            GuardFromNull(alias, "alias");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.AddAlias(userId, alias);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="user">The aliased user</param>
        /// <param name="alias">The alias to delete </param>
        public void Delete(EnterpriseUser user, EmailAlias alias)
        {
            GuardFromNull(user, "user");
            GuardFromNull(alias, "alias");
            DeleteEmailAlias(user.Id, alias.Id);
        }

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="userId">The ID of the aliased user</param>
        /// <param name="aliasId">The ID of the alias to delete</param>
        public void DeleteEmailAlias(string userId, string aliasId)
        {
            GuardFromNull(userId, "userId");
            GuardFromNull(aliasId, "aliasId");
            IRestRequest request = _requestHelper.DeleteAlias(userId, aliasId);
            _restClient.Execute(request);
        }

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform following a successful delete</param>
        /// <param name="onFailure">An action to perform following a failed User operation </param>
        /// <param name="user">The aliased user</param>
        /// <param name="alias">The alias to delete</param>
        public void Delete(Action onSuccess, Action<Error> onFailure, EnterpriseUser user, EmailAlias alias)
        {
            GuardFromNull(user, "user");
            GuardFromNull(alias, "alias");
            DeleteEmailAlias(onSuccess, onFailure, user.Id, alias.Id);
        }

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform following a successful delete</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the aliased user</param>
        /// <param name="aliasId">The ID of the alias to delete</param>
        public void DeleteEmailAlias(Action onSuccess, Action<Error> onFailure, string userId, string aliasId)
        {
            GuardFromNull(userId, "userId");
            GuardFromNull(aliasId, "aliasId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.DeleteAlias(userId, aliasId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <param name="login">The new login</param>
        /// <returns>The updated user</returns>
        public EnterpriseUser ChangePrimaryLogin(EnterpriseUser user, string login)
        {
            GuardFromNull(user, "user");
            return ChangePrimaryLogin(user.Id, login);
        }

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="userId">The ID of the user to update</param>
        /// <param name="login">The new login</param>
        /// <returns>The updated user</returns>
        public EnterpriseUser ChangePrimaryLogin(string userId, string login)
        {
            GuardFromNull(userId, "userId");
            GuardFromNull(login, "login");
            IRestRequest request = _requestHelper.UpdateLogin(userId, login);
            return _restClient.ExecuteAndDeserialize<EnterpriseUser>(request);
        }

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="user">The user to update</param>
        /// <param name="login">The new login</param>
        public void ChangePrimaryLogin(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user, string login)
        {
            GuardFromNull(user, "user");
            ChangePrimaryLogin(onSuccess, onFailure, user.Id, login);
        }

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the user to update</param>
        /// <param name="login">The new login</param>
        public void ChangePrimaryLogin(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, string userId, string login)
        {
            GuardFromNull(userId, "userId");
            GuardFromNull(login, "login");
            IRestRequest request = _requestHelper.UpdateLogin(userId, login);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// converts them to a standalone free user.
        /// </summary>
        /// <param name="user">The enterprise user to convert</param>
        /// <returns>The standalone user</returns>
        public User ConvertToStandaloneUser(EnterpriseUser user)
        {
            var restRequest = _requestHelper.RemoveUserFromEnterprise(user.Id);
            return _restClient.ExecuteAndDeserialize<User>(restRequest);
        }

        /// <summary>
        /// Rolls a user out of an enterprise and converts them to a standalone free user.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the standalone user</param>
        /// <param name="onFailure">Action to perform following failed conversion</param>
        /// <param name="user">The enterprise user to convert</param>
        public void ConvertToStandaloneUser(Action<User> onSuccess, Action<Error> onFailure, EnterpriseUser user)
        {
            var restRequest = _requestHelper.RemoveUserFromEnterprise(user.Id);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }
    }
}