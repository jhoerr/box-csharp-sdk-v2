using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using RestSharp;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="folder">The folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new collaboration</returns>
        public Collaboration CreateCollaboration(Folder folder, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return CreateCollaboration(folder.Id, userId, role, fields);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="folderId">The ID of the folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new collaboration</returns>
        public Collaboration CreateCollaboration(string folderId, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description(), fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="folder">The folder in which to collaborate</param>
        /// <param name="emailAddress">The email address of the collaborator (does not need to be a Box user)</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new collaboration</returns>
        public Collaboration CreateCollaborationByEmail(Folder folder, string emailAddress, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return CreateCollaborationByEmail(folder.Id, emailAddress, role, fields);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="folderId">The ID of the folder in which to collaborate</param>
        /// <param name="emailAddress">The email address of the collaborator (does not need to be a Box user)</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new collaboration</returns>
        public Collaboration CreateCollaborationByEmail(string folderId, string emailAddress, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(emailAddress, "emailAddress");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaborationByEmail(folderId, emailAddress, role.Description(), fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="folder">The folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, Folder folder, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folder, "folder");
            CreateCollaboration(onSuccess, onFailure, folder.Id, userId, role, fields);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="folderId">The ID of the folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string folderId, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description(), fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="folder">The folder in which to collaborate</param>
        /// <param name="emailAddress">The email address of the collaborator (does not need to be a Box user)</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateCollaborationByEmail(Action<Collaboration> onSuccess, Action<Error> onFailure, Folder folder, string emailAddress, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folder, "folder");
            CreateCollaborationByEmail(onSuccess, onFailure, folder.Id, emailAddress, role, fields);
        }

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="folderId">The ID of the folder in which to collaborate</param>
        /// <param name="emailAddress">The email address of the collaborator (does not need to be a Box user)</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateCollaborationByEmail(Action<Collaboration> onSuccess, Action<Error> onFailure, string folderId, string emailAddress, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(emailAddress, "emailAddress");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaborationByEmail(folderId, emailAddress, role.Description(), fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The existing collaboration</returns>
        public Collaboration Get(Collaboration collaboration, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return GetCollaboration(collaboration.Id, fields);
        }

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The existing collaboration</returns>
        public Collaboration GetCollaboration(string collaborationId, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.GetCollaboration(collaborationId, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the existig collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="collaboration">The collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Get(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            GetCollaboration(onSuccess, onFailure, collaboration.Id, fields);
        }

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the existig collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="collaborationId">The ID of the collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaboration(collaborationId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="folder">The folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's collaborations</returns>
        public CollaborationCollection GetCollaborations(Folder folder, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetCollaborations(folder.Id, pendingCollaborationsOnly, fields);
        }

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="folderId">The ID of the folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's collaborations</returns>
        public CollaborationCollection GetCollaborations(string folderId, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetCollaborations(folderId, pendingCollaborationsOnly, fields);
            return _restClient.ExecuteAndDeserialize<CollaborationCollection>(request);
        }

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's collaborations</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation</param>
        /// <param name="folder">The folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetCollaborations(Action<CollaborationCollection> onSuccess, Action<Error> onFailure, Folder folder, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folder, "folder");
            GetCollaborations(onSuccess, onFailure, folder.Id, pendingCollaborationsOnly, fields);
        }

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's collaborations</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation</param>
        /// <param name="folderId">The ID of the folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetCollaborations(Action<CollaborationCollection> onSuccess, Action<Error> onFailure, string folderId, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaborations(folderId, pendingCollaborationsOnly, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        public Collaboration Update(Collaboration collaboration, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, fields);
        }

        /// <summary>
        ///     Update the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        public Collaboration Update(Collaboration collaboration, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, status, fields);
        }

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        public Collaboration UpdateCollaboration(string collaborationId, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        /// <summary>
        ///     Updat the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        public Collaboration UpdateCollaboration(string collaborationId, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Update(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(onSuccess, onFailure, collaboration.Id, role, fields);
        }

        /// <summary>
        ///     Update the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Update(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(onSuccess, onFailure, collaboration.Id, role, status, fields);
        }

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void UpdateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, CollaborationRole role, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Update the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void UpdateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to delete</param>
        public void Delete(Collaboration collaboration)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(collaboration.Id);
        }

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to delete</param>
        public void DeleteCollaboration(string collaborationId)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform after the collaboration is deleted</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaboration">The collaboration to delete</param>
        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Collaboration collaboration)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(onSuccess, onFailure, collaboration.Id);
        }

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform after the collaboration is deleted</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaborationId">The ID of the collaboration to delete</param>
        public void DeleteCollaboration(Action<IRestResponse> onSuccess, Action<Error> onFailure, string collaborationId)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}