using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using RestSharp;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with Box collaborations
    /// </summary>
    public interface ICollaboration
    {
        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="folder">The folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new collaboration</returns>
        Collaboration CreateCollaboration(Folder folder, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="folderId">The ID of the folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new collaboration</returns>
        Collaboration CreateCollaboration(string folderId, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="folder">The folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, Folder folder, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Add a collaboration for a single user to a folder.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="folderId">The ID of the folder in which to collaborate</param>
        /// <param name="userId">The ID of the collaborating user</param>
        /// <param name="role">The role of the collaborating user</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string folderId, string userId, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The existing collaboration</returns>
        Collaboration Get(Collaboration collaboration, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The existing collaboration</returns>
        Collaboration GetCollaboration(string collaborationId, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the existig collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="collaboration">The collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Get(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve the details of an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the existig collaboration</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation </param>
        /// <param name="collaborationId">The ID of the collaboration to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="folder">The folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's collaborations</returns>
        CollaborationCollection GetCollaborations(Folder folder, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="folderId">The ID of the folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's collaborations</returns>
        CollaborationCollection GetCollaborations(string folderId, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's collaborations</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation</param>
        /// <param name="folder">The folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetCollaborations(Action<CollaborationCollection> onSuccess, Action<Error> onFailure, Folder folder, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Retrieve all existing collaborations in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's collaborations</param>
        /// <param name="onFailure">Action to perform after a failed Collaboration operation</param>
        /// <param name="folderId">The ID of the folder whose collaborations should be retrieved</param>
        /// <param name="pendingCollaborationsOnly">Retrieve only those collaborations whose status is 'Pending'</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned CollaborationCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetCollaborations(Action<CollaborationCollection> onSuccess, Action<Error> onFailure, string folderId, bool pendingCollaborationsOnly = false, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        Collaboration Update(Collaboration collaboration, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        Collaboration Update(Collaboration collaboration, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        Collaboration UpdateCollaboration(string collaborationId, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Updat the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated collaboration</returns>
        Collaboration UpdateCollaboration(string collaborationId, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Update(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaboration">The collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Update(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void UpdateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, CollaborationRole role, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Update the collaborator's role and status in an existing collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated collaboration</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaborationId">The ID of the collaboration to update</param>
        /// <param name="role">The collaborator's new role</param>
        /// <param name="status">The collaborator's new status</param>
        /// <param name="fields">The properties that should be set on the returned Collaboration object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void UpdateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields = null);

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="collaboration">The collaboration to delete</param>
        void Delete(Collaboration collaboration);

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="collaborationId">The ID of the collaboration to delete</param>
        void DeleteCollaboration(string collaborationId);

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform after the collaboration is deleted</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaboration">The collaboration to delete</param>
        void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Collaboration collaboration);

        /// <summary>
        ///     Delete a collaboration
        /// </summary>
        /// <param name="onSuccess">Action to perform after the collaboration is deleted</param>
        /// <param name="onFailure">Action to perform following a failed Collaboration operation</param>
        /// <param name="collaborationId">The ID of the collaboration to delete</param>
        void DeleteCollaboration(Action<IRestResponse> onSuccess, Action<Error> onFailure, string collaborationId);
    }
}