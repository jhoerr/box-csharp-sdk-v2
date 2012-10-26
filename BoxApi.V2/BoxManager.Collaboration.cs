using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoxApi.V2.Model;
using IRestResponse = RestSharp.IRestResponse;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public Collaboration CreateCollaboration(Folder folder, string userId, Role role, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return CreateCollaboration(folder.Id, userId, role, fields);
        }

        public Collaboration CreateCollaboration(string folderId, string userId, Role role, Field[] fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description(), fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public void CreateCollaboration(Folder folder, string userId, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            CreateCollaboration(folder.Id, userId, role, fields, onSuccess, onFailure);
        }

        public void CreateCollaboration(string folderId, string userId, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description(), fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public Collaboration Get(Collaboration collaboration, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return GetCollaboration(collaboration.Id, fields);
        }

        public Collaboration GetCollaboration(string collaborationId, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.GetCollaboration(collaborationId, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public void Get(Collaboration collaboration, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            GetCollaboration(collaboration.Id, fields, onSuccess, onFailure);
        }

        public void GetCollaboration(string collaborationId, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaboration(collaborationId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public CollaborationCollection GetCollaborations(Folder folder, bool pendingCollaborationsOnly = false, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetCollaborations(folder.Id, pendingCollaborationsOnly, fields);
        }

        public CollaborationCollection GetCollaborations(string folderId, bool pendingCollaborationsOnly = false, Field[] fields = null)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetCollaborations(folderId, pendingCollaborationsOnly, fields);
            return _restClient.ExecuteAndDeserialize<CollaborationCollection>(request);
        }

        public void GetCollaborations(Folder folder, bool pendingCollaborationsOnly, Field[] fields, Action<CollaborationCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetCollaborations(folder.Id, pendingCollaborationsOnly, fields, onSuccess, onFailure);
        }

        public void GetCollaborations(string folderId, bool pendingCollaborationsOnly, Field[] fields, Action<CollaborationCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaborations(folderId, pendingCollaborationsOnly, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public Collaboration Update(Collaboration collaboration, Role role, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, fields);
        }

        public Collaboration Update(Collaboration collaboration, Role role, Status status, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, status, fields);
        }

        public Collaboration UpdateCollaboration(string collaborationId, Role role, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public Collaboration UpdateCollaboration(string collaborationId, Role role, Status status, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public void Update(Collaboration collaboration, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(collaboration.Id, role, fields, onSuccess, onFailure);
        }

        public void Update(Collaboration collaboration, Role role, Status status, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(collaboration.Id, role, status, fields, onSuccess, onFailure);
        }

        public void UpdateCollaboration(string collaborationId, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void UpdateCollaboration(string collaborationId, Role role, Status status, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    
        public void Delete(Collaboration collaboration)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(collaboration.Id);
        }

        public void DeleteCollaboration(string collaborationId)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.Execute(request);
        }

        public void Delete(Collaboration collaboration, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(collaboration.Id, onSuccess, onFailure);
        }

        public void DeleteCollaboration(string collaborationId, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}
