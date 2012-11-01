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

        public void CreateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, Folder folder, string userId, Role role, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            CreateCollaboration(onSuccess, onFailure, folder.Id, userId, role, fields);
        }

        public void CreateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string folderId, string userId, Role role, Field[] fields = null)
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

        public void Get(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            GetCollaboration(onSuccess, onFailure, collaboration.Id, fields);
        }

        public void GetCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, Field[] fields = null)
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

        public void GetCollaborations(Action<CollaborationCollection> onSuccess, Action<Error> onFailure, Folder folder, bool pendingCollaborationsOnly = false, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            GetCollaborations(onSuccess, onFailure, folder.Id, pendingCollaborationsOnly, fields);
        }

        public void GetCollaborations(Action<CollaborationCollection> onSuccess, Action<Error> onFailure, string folderId, bool pendingCollaborationsOnly = false, Field[] fields = null)
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

        public void Update(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, Role role, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(onSuccess, onFailure, collaboration.Id, role, fields);
        }

        public void Update(Action<Collaboration> onSuccess, Action<Error> onFailure, Collaboration collaboration, Role role, Status status, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(onSuccess, onFailure, collaboration.Id, role, status, fields);
        }

        public void UpdateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, Role role, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void UpdateCollaboration(Action<Collaboration> onSuccess, Action<Error> onFailure, string collaborationId, Role role, Status status, Field[] fields = null)
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

        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Collaboration collaboration)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(onSuccess, onFailure, collaboration.Id);
        }

        public void DeleteCollaboration(Action<IRestResponse> onSuccess, Action<Error> onFailure, string collaborationId)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}
