using System;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods available to Box enterprise admins/co-Admins
    /// </summary>
    public interface IEnterpriseManager : IUserManager
    {
        /// <summary>
        ///     For an individual user, this provides their own user information and content. For an enterprise admin, this provides information on all users in the admin’s enterprise.
        /// </summary>
        /// <param name="filterTerm">Only users whose name or login starts with this value will be returned.  Default is no filter.</param>
        /// <param name="limit">The number of records to return. If no filterTerm, limit, or offset are supplied, all user records are returned.  </param>
        /// <param name="offset">The record at which to start (used in conjunction with 'limit'.)  Default is the first record.</param>
        /// <returns>A collection of users matching the supplied filtering criteria</returns>
        EnterpriseUserCollection GetUsers(string filterTerm = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     For an individual user, this provides their own user information and content. For an enterprise admin, this provides information on all users in the admin’s enterprise.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the returned users</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="filterTerm">Only users whose name or login starts with this value will be returned.  Default is no filter.</param>
        /// <param name="limit">The number of records to return. Default is all users.</param>
        /// <param name="offset">The record at which to start (used in conjunction with 'limit'.)  Default is the first record.</param>
        void GetUsers(Action<EnterpriseUserCollection> onSuccess, Action<Error> onFailure, string filterTerm = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="user">The user to retrieve</param>
        /// <returns>The requested user</returns>
        EnterpriseUser Get(EnterpriseUser user);

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <returns>The requested user</returns>
        EnterpriseUser GetUser(string id);

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user to retrieve</param>
        void Get(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user);

        /// <summary>
        ///     Retrieves a single user
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="id">The ID of the user to retrieve</param>
        void GetUser(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, string id);

        /// <summary>
        /// Used to provision a new user in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="user">The properties to set for the new user.  Name and Login are required.  Warning -- Box tracks a user's space amount in GB, so if you choose to specify the SpaceAmount, it must be at least 1 GB (2^30).</param>
        /// <returns>The new user</returns>
        EnterpriseUser CreateUser(EnterpriseUser user);

        /// <summary>
        /// Used to provision a new user in an enterprise. This method only works for enterprise admins.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The properties to set for the new user.  Name and Login are required.  Warning -- Box tracks a user's space amount in GB, so if you choose to specify the SpaceAmount, it must be at least 1 GB (2^30).</param>
        /// <param name="fields">The properties that should be set on the returned User.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new user</returns>
        void CreateUser(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user, EnterpriseUserField[] fields = null);

        /// <summary>
        /// Updates the settings and information about a user. This method only works for enterprise admins
        /// </summary>
        /// <param name="user">The user with udpated information</param>
        /// <returns>A user with the updated information</returns>
        EnterpriseUser UpdateUser(EnterpriseUser user);

        /// <summary>
        /// Updates the settings and information about a user. This method only works for enterprise admins
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated user</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user with udpated information</param>
        /// <returns>A user with the updated information</returns>
        void UpdateUser(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user);

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="user">The user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        void Delete(EnterpriseUser user, bool notify = true, bool force = false);

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        void DeleteUser(string id, bool notify = true, bool force = false);

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful deletion</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="user">The user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        void Delete(Action onSuccess, Action<Error> onFailure, EnterpriseUser user, bool notify = true, bool force = false);

        /// <summary>
        ///     Deletes a user from an enterprise account
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful deletion</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="id">The ID of the user to delete</param>
        /// <param name="notify">Whether to notify the user by email that their account has been deleted.  Default is to notify them.</param>
        /// <param name="force">Whether to force the deletion if the user has items in their Box.  Default is to not force the deletion.</param>
        /// <exception cref="BoxApi.V2.BoxException">Thrown if the 'force' is false and the user has items in their Box</exception>
        void DeleteUser(Action onSuccess, Action<Error> onFailure, string id, bool notify = true, bool force = false);

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="currentOwner">The user whose files will be moved</param>
        /// <param name="newOwner">The user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        /// <returns>The folder as it now exists in the newOwner's Box</returns>
        Folder MoveRootFolderToAnotherUser(EnterpriseUser currentOwner, EnterpriseUser newOwner, bool notify = true);

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="currentOwnerId">The ID of the user whose files will be moved</param>
        /// <param name="newOwnerId">The ID of the user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        /// <returns></returns>
        Folder MoveRootFolderToAnotherUser(string currentOwnerId, string newOwnerId, bool notify = true);

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="currentOwner">The user whose files will be moved</param>
        /// <param name="newOwner">The user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        void MoveRootFolderToAnotherUser(Action<Folder> onSuccess, Action<Error> onFailure, EnterpriseUser currentOwner, EnterpriseUser newOwner, bool notify = true);

        /// <summary>
        ///     Moves all of the content from within one user’s folder into a new folder in another user’s account. You can move folders across users as long as the you have administrative permissions.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed User operation</param>
        /// <param name="currentOwnerId">The ID of the user whose files will be moved</param>
        /// <param name="newOwnerId">The ID of the user to whom the files will be moved</param>
        /// <param name="notify">Whether to notify the currentOwner that their files are being moved</param>
        void MoveRootFolderToAnotherUser(Action<Folder> onSuccess, Action<Error> onFailure, string currentOwnerId, string newOwnerId, bool notify = true);

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="user">The user for whom to retrieve the email aliases</param>
        /// <returns>A collection of email aliases</returns>
        EmailAliasCollection GetEmailAliases(EnterpriseUser user);

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="userId">The ID of the user for whom to retrieve the email aliases</param>
        /// <returns>A collection of email aliases</returns>
        EmailAliasCollection GetEmailAliases(string userId);

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="onSuccess">An action to perfrom with the email aliases</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="user">The user for whom to retrieve the email aliases</param>
        void GetEmailAliases(Action<EmailAliasCollection> onSuccess, Action<Error> onFailure, EnterpriseUser user);

        /// <summary>
        /// Retrieves the email aliases for a user
        /// </summary>
        /// <param name="onSuccess">An action to perfrom with the email aliases</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the user for whom to retrieve the email aliases</param>
        void GetEmailAliases(Action<EmailAliasCollection> onSuccess, Action<Error> onFailure, string userId);

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="user">The user to alias</param>
        /// <param name="alias">The email alias to add </param>
        /// <returns>The updated user</returns>
        EmailAlias AddEmailAlias(EnterpriseUser user, string alias);

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="userId">The ID of the user to alias</param>
        /// <param name="alias">The email alias to add </param>
        /// <returns>The updated user</returns>
        EmailAlias AddEmailAlias(string userId, string alias);

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation </param>
        /// <param name="user">The user to alias</param>
        /// <param name="alias">The email alias to add </param>
        void AddEmailAlias(Action<EmailAlias> onSuccess, Action<Error> onFailure, EnterpriseUser user, string alias);

        /// <summary>
        /// Add a new email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the user to alias</param>
        /// <param name="alias">The email alias to add </param>
        void AddEmailAlias(Action<EmailAlias> onSuccess, Action<Error> onFailure, string userId, string alias);

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="user">The aliased user</param>
        /// <param name="alias">The alias to delete </param>
        void Delete(EnterpriseUser user, EmailAlias alias);

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="userId">The ID of the aliased user</param>
        /// <param name="aliasId">The ID of the alias to delete</param>
        void DeleteEmailAlias(string userId, string aliasId);

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform following a successful delete</param>
        /// <param name="onFailure">An action to perform following a failed User operation </param>
        /// <param name="user">The aliased user</param>
        /// <param name="alias">The alias to delete</param>
        void Delete(Action onSuccess, Action<Error> onFailure, EnterpriseUser user, EmailAlias alias);

        /// <summary>
        /// Delete an email alias for a user
        /// </summary>
        /// <param name="onSuccess">An action to perform following a successful delete</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the aliased user</param>
        /// <param name="aliasId">The ID of the alias to delete</param>
        void DeleteEmailAlias(Action onSuccess, Action<Error> onFailure, string userId, string aliasId);

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <param name="login">The new login</param>
        /// <returns>The updated user</returns>
        EnterpriseUser ChangePrimaryLogin(EnterpriseUser user, string login);

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="userId">The ID of the user to update</param>
        /// <param name="login">The new login</param>
        /// <returns>The updated user</returns>
        EnterpriseUser ChangePrimaryLogin(string userId, string login);

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="user">The user to update</param>
        /// <param name="login">The new login</param>
        void ChangePrimaryLogin(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, EnterpriseUser user, string login);

        /// <summary>
        /// Changes the primary login email to one of a user's confirmed aliases
        /// </summary>
        /// <param name="onSuccess">An action to perform with the updated user</param>
        /// <param name="onFailure">An action to perform following a failed User operation</param>
        /// <param name="userId">The ID of the user to update</param>
        /// <param name="login">The new login</param>
        void ChangePrimaryLogin(Action<EnterpriseUser> onSuccess, Action<Error> onFailure, string userId, string login);

        /// <summary>
        /// converts them to a standalone free user.
        /// </summary>
        /// <param name="user">The enterprise user to convert</param>
        /// <returns>The standalone user</returns>
        User ConvertToStandaloneUser(EnterpriseUser user);

        /// <summary>
        /// Rolls a user out of an enterprise and converts them to a standalone free user.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the standalone user</param>
        /// <param name="onFailure">Action to perform following failed conversion</param>
        /// <param name="user">The enterprise user to convert</param>
        void ConvertToStandaloneUser(Action<User> onSuccess, Action<Error> onFailure, EnterpriseUser user);

        /// <summary>
        ///     Retrieves the events that have occurred in an enterprise.  Events will occasionally arrive out of order.  You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="offset">The item at which to start.  Deault is 0 (the earliest known event.)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned</param>
        /// <param name="eventTypes">A list of event types to filter by.  Only events of these types will be returned.</param>
        /// <returns>A collection of EnterpriseEvents.</returns>
        EnterpriseEventCollection GetEnterpriseEvents(int offset = 0, int limit = 100, DateTime? createdAfter = null, DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null);

        /// <summary>
        ///     Retrieves the events that have occurred in an enterprise.  Events will occasionally arrive out of order.  You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved events</param>
        /// <param name="onFailure">Action to perform following a failed Event operation</param>
        /// <param name="offset">The item at which to start.  Deault is 0 (the earliest known event.)</param>
        /// <param name="limit">The maximum number of events to return with this request. Default is 100.</param>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned</param>
        /// <param name="eventTypes">A list of event types to filter by.  Only events of these types will be returned.</param>
        void GetEnterpriseEvents(Action<EnterpriseEventCollection> onSuccess, Action<Error> onFailure, int offset = 0, int limit = 100, DateTime? createdAfter = null,
                                 DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null);

        /// <summary>
        ///     Retrieves the events that have occurred in an enterprise.  Events will occasionally arrive out of order.  You may need to buffer events and apply them in a logical order.
        /// </summary>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned</param>
        /// <param name="eventTypes">A list of event types to filter by.  Only events of these types will be returned.</param>
        /// <returns>A collection of EnterpriseEvents.</returns>
        EnterpriseEventCollection GetEnterpriseEvents(DateTime? createdAfter = null, DateTime? createdBefore = null, EnterpriseEventType[] eventTypes = null);
    }
}