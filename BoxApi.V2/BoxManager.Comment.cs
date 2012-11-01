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

        public void CreateComment(Action<Comment> onSuccess, Action<Error> onFailure, Discussion discussion, string comment, Field[] fields = null)
        {
            GuardFromNull(discussion, "discussion");
            CreateDiscussionComment(onSuccess, onFailure, discussion.Id, comment, fields);
        }

        public void CreateDiscussionComment(Action<Comment> onSuccess, Action<Error> onFailure, string discussionId, string comment, Field[] fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void CreateComment(Action<Comment> onSuccess, Action<Error> onFailure, File file, string comment, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            CreateFileComment(onSuccess, onFailure, file.Id, comment, fields);
        }

        public void CreateFileComment(Action<Comment> onSuccess, Action<Error> onFailure, string fileId, string comment, Field[] fields = null)
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

        public void GetComment(Action<Comment> onSuccess, Action<Error> onFailure, Comment comment, Field[] fields = null)
        {
            GuardFromNull(comment, "comment");
            GetComment(onSuccess, onFailure, comment.Id, fields);
        }

        public void GetComment(Action<Comment> onSuccess, Action<Error> onFailure, string commentId, Field[] fields = null)
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

        public void GetComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            GetFileComments(onSuccess, onFailure, file.Id, fields);
        }

        public void GetFileComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, string fileId, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, Discussion discussion, Field[] fields = null)
        {
            GuardFromNull(discussion, "discussion");
            GetDiscussionComments(onSuccess, onFailure, discussion.Id, fields);
        }

        public void GetDiscussionComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, string discussionId, Field[] fields = null)
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

        public void Update(Action<Comment> onSuccess, Action<Error> onFailure, Comment comment, Field[] fields = null)
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

        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Comment comment)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(onSuccess, onFailure, comment.Id);
        }

        public void DeleteComment(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteComment(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }


    }
}