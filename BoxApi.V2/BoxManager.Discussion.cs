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
        public Discussion CreateDiscussion(Folder parent, string name, string description = null, Field[] fields = null)
        {
            GuardFromNull(parent, "parent");
            return CreateDiscussion(parent.Id, name, description, fields);
        }

        public Discussion CreateDiscussion(string parentId, string name, string description = null, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateDiscussion(parentId, name, description, fields);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        public void CreateDiscussion(Folder parent, string name, string description, Field[] fields, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parent, "parent");
            CreateDiscussion(parent.Id, name, description, fields, onSuccess, onFailure);
        }

        public void CreateDiscussion(string parentId, string name, string description, Field[] fields, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateDiscussion(parentId, name, description, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
        
        public Discussion GetDiscussion(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Discussion, id, fields);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        public void GetDiscussion(string id, Field[] fields, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Discussion, id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public DiscussionCollection GetDiscussions(Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetDiscussions(folder.Id, fields);
        }

        public DiscussionCollection GetDiscussions(string folderId, Field[] fields = null)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetDiscussions(folderId, fields);
            return _restClient.ExecuteAndDeserialize<DiscussionCollection>(request);
        }

        public void GetDiscussions(Folder folder, Field[] fields, Action<DiscussionCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetDiscussions(folder.Id, fields, onSuccess, onFailure);
        }

        public void GetDiscussions(string folderId, Field[] fields, Action<DiscussionCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetDiscussions(folderId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Discussion discussion)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(discussion.Id);
        }

        public void DeleteDiscussion(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteDiscussion(id);
            _restClient.Execute(request);
        }

        public void Delete(Discussion discussion, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(discussion.Id, onSuccess, onFailure);
        }

        public void DeleteDiscussion(string id, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteDiscussion(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }


    }
}
