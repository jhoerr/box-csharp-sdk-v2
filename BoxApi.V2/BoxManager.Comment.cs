using System;
using BoxApi.V2.Model;
using RestSharp;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public Comment CreateComment(File file, string comment, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return CreateFileComment(file.Id, comment, fields);
        }

        public Comment CreateFileComment(string fileId, string comment, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public Comment CreateComment(Discussion discussion, string comment, Field[] fields = null)
        {
            GuardFromNull(discussion, "discussion");
            return CreateDiscussionComment(discussion.Id, comment, fields);
        }

        public Comment CreateDiscussionComment(string discussionId, string comment, Field[] fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public void CreateComment(Discussion discussion, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            CreateDiscussionComment(discussion.Id, comment, fields, onSuccess, onFailure);
        }

        public void CreateDiscussionComment(string discussionId, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void CreateComment(File file, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            CreateFileComment(file.Id, comment, fields, onSuccess, onFailure);
        }

        public void CreateFileComment(string fileId, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public Comment GetComment(Comment comment, Field[] fields = null)
        {
            GuardFromNull(comment, "comment");
            return GetComment(comment.Id, fields);
        }

        public Comment GetComment(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var restRequest = _requestHelper.Get(ResourceType.Comment, id, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public void GetComment(Comment comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            GetComment(comment.Id, fields, onSuccess, onFailure);
        }

        public void GetComment(string commentId, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(commentId, "commentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.Get(ResourceType.Comment, commentId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public CommentCollection GetComments(Discussion discussion, Field[] fields = null)
        {
            GuardFromNull(discussion, "discussion");
            return GetDiscussionComments(discussion.Id, fields);
        }

        public CommentCollection GetDiscussionComments(string discussionId, Field[] fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            var restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId, fields);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        public CommentCollection GetComments(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return GetFileComments(file.Id, fields);
        }

        public CommentCollection GetFileComments(string fileId, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        public void GetComments(File file, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            GetFileComments(file.Id, fields, onSuccess, onFailure);
        }

        public void GetFileComments(string fileId, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComments(Discussion discussion, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            GetDiscussionComments(discussion.Id, fields, onSuccess, onFailure);
        }

        public void GetDiscussionComments(string discussionId, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public Comment Update(Comment comment, Field[] fields = null)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(ResourceType.Comment, comment.Id, fields, message: comment.Message);
            return _restClient.ExecuteAndDeserialize<Comment>(request);
        }

        public void Update(Comment comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(ResourceType.Comment, comment.Id, fields, message: comment.Message);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Comment comment)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(comment.Id);
        }

        public void DeleteComment(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteComment(id);
            _restClient.Execute(request);
        }

        public void Delete(Comment comment, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(comment.Id, onSuccess, onFailure);
        }

        public void DeleteComment(string id, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteComment(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }


    }
}