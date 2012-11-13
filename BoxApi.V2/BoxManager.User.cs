using System;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     For an individual user, this provides their own user information and content. For an enterprise admin, this provides information on all users in the admin’s enterprise.
        /// </summary>
        /// <param name="filterTerm">Only users whose name or login starts with this value will be returned.  Default is no filter.</param>
        /// <param name="limit">The number of records to return. Default is all users.</param>
        /// <param name="offset">The record at which to start (used in conjunction with 'limit'.)  Default is the first record.</param>
        /// <param name="fields">The properties that should be set on the returned UserCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of users matching the supplied filtering criteria</returns>
        public UserCollection GetUsers(string filterTerm = null, int? limit = null, int? offset = null, Field[] fields = null)
        {
            var request = _requestHelper.GetUsers(filterTerm, limit, offset, fields);
            return _restClient.ExecuteAndDeserialize<UserCollection>(request);
        }

        /// <summary>
        ///     For an individual user, this provides their own user information and content. For an enterprise admin, this provides information on all users in the admin’s enterprise.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the returned users</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="filterTerm">Only users whose name or login starts with this value will be returned.  Default is no filter.</param>
        /// <param name="limit">The number of records to return. Default is all users.</param>
        /// <param name="offset">The record at which to start (used in conjunction with 'limit'.)  Default is the first record.</param>
        /// <param name="fields">The properties that should be set on the returned UserCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetUsers(Action<UserCollection> onSuccess, Action<Error> onFailure, string filterTerm = null, int? limit = null, int? offset = null, Field[] fields = null)
        {
            var request = _requestHelper.GetUsers(filterTerm, limit, offset, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="user">The user to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The requested user</returns>
        public User Get(User user, Field[] fields = null)
        {
            GuardFromNull(user, "user");
            return GetUser(user.Id, fields);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The requested user</returns>
        public User GetUser(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetUser(id, fields);
            return _restClient.ExecuteAndDeserialize<User>(request);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Get(Action<User> onSuccess, Action<Error> onFailure, User user, Field[] fields = null)
        {
            GuardFromNull(user, "user");
            GetUser(onSuccess, onFailure, user.Id, fields);
        }

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetUser(Action<User> onSuccess, Action<Error> onFailure, string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetUser(id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="user">The user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        public void Delete(User user, bool notify = true, bool force = false)
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
        public void Delete(Action onSuccess, Action<Error> onFailure, User user, bool notify = true, bool force = false)
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
        /// <param name="fields">The properties that should be set on the returned Folder.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder as it now exists in the newOwner's Box</returns>
        public Folder MoveRootFolderToAnotherUser(User currentOwner, User newOwner, bool notify = true, Field[] fields = null)
        {
            GuardFromNull(currentOwner, "currentOwner");
            GuardFromNull(newOwner, "newOwner");
            return MoveRootFolderToAnotherUser(currentOwner.Id, newOwner.Id, notify, fields);
        }

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="currentOwnerId">The ID of the user whose files will be moved</param>
        /// <param name="newOwnerId">The ID of the user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        /// <param name="fields">The properties that should be set on the returned Folder.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns></returns>
        public Folder MoveRootFolderToAnotherUser(string currentOwnerId, string newOwnerId, bool notify = true, Field[] fields = null)
        {
            GuardFromNull(currentOwnerId, "currentOwnerId");
            GuardFromNull(newOwnerId, "newOwnerId");
            var request = _requestHelper.MoveFolderToAnotherUser(currentOwnerId, Folder.Root, newOwnerId, notify, fields);
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
        /// <param name="fields">The properties that should be set on the returned Folder.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void MoveRootFolderToAnotherUser(Action<Folder> onSuccess, Action<Error> onFailure, User currentOwner, User newOwner, bool notify = true, Field[] fields = null)
        {
            GuardFromNull(currentOwner, "currentOwner");
            GuardFromNull(newOwner, "newOwner");
            MoveRootFolderToAnotherUser(onSuccess, onFailure, currentOwner.Id, newOwner.Id, notify, fields);
        }

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="currentOwnerId">The ID of the user whose files will be moved</param>
        /// <param name="newOwnerId">The ID of the user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        /// <param name="fields">The properties that should be set on the returned Folder.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void MoveRootFolderToAnotherUser(Action<Folder> onSuccess, Action<Error> onFailure, string currentOwnerId, string newOwnerId, bool notify = true, Field[] fields = null)
        {
            GuardFromNull(currentOwnerId, "currentOwnerId");
            GuardFromNull(newOwnerId, "newOwnerId");
            var request = _requestHelper.MoveFolderToAnotherUser(currentOwnerId, Folder.Root, newOwnerId, notify, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}