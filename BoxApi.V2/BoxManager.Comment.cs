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
        ///     Add a comment to a file
        /// </summary>
        /// <param name="file">The file on which to commment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The file's new comment</returns>
        public Comment CreateComment(File file, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(file, "file");
            return CreateFileComment(file.Id, message, fields);
        }

        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="fileId">The ID of the file on which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new comment</returns>
        public Comment CreateFileComment(string fileId, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(message, "message");
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, message, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="file">The file on which to commment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateComment(Action<Comment> onSuccess, Action<Error> onFailure, File file, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(file, "file");
            CreateFileComment(onSuccess, onFailure, file.Id, message, fields);
        }

        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="fileId">The ID of the file on which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFileComment(Action<Comment> onSuccess, Action<Error> onFailure, string fileId, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(message, "message");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, message, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="discussion">The discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's new comment</returns>
        public Comment CreateComment(Discussion discussion, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussion, "discussion");
            return CreateDiscussionComment(discussion.Id, message, fields);
        }

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="discussionId">The ID of the discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's new comment</returns>
        public Comment CreateDiscussionComment(string discussionId, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(message, "comment");
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, message, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussion">The discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateComment(Action<Comment> onSuccess, Action<Error> onFailure, Discussion discussion, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussion, "discussion");
            CreateDiscussionComment(onSuccess, onFailure, discussion.Id, message, fields);
        }

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussionId">The ID of the discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateDiscussionComment(Action<Comment> onSuccess, Action<Error> onFailure, string discussionId, string message, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(message, "message");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, message, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="comment">The comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved comment</returns>
        public Comment GetComment(Comment comment, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(comment, "comment");
            return GetComment(comment.Id, fields);
        }

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved comment</returns>
        public Comment GetComment(string id, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(id, "id");
            var restRequest = _requestHelper.Get(ResourceType.Comment, id, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="comment">The comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetComment(Action<Comment> onSuccess, Action<Error> onFailure, Comment comment, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(comment, "comment");
            GetComment(onSuccess, onFailure, comment.Id, fields);
        }

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="id">The ID of the comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetComment(Action<Comment> onSuccess, Action<Error> onFailure, string id, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(id, "commentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.Get(ResourceType.Comment, id, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }


        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="file">The file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The file's comments</returns>
        public CommentCollection GetComments(File file, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(file, "file");
            return GetFileComments(file.Id, fields);
        }

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="fileId">The ID of the file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The file's comments</returns>
        public CommentCollection GetFileComments(string fileId, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(fileId, "fileId");
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved file comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="file">The file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, File file, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(file, "file");
            GetFileComments(onSuccess, onFailure, file.Id, fields);
        }

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved file comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="fileId">The ID of the file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetFileComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, string fileId, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="discussion">The discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's comments</returns>
        public CommentCollection GetComments(Discussion discussion, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussion, "discussion");
            return GetDiscussionComments(discussion.Id, fields);
        }

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="discussionId">The ID of the discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's comments</returns>
        public CommentCollection GetDiscussionComments(string discussionId, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            var restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId, fields);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the discussion's comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussion">The discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, Discussion discussion, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussion, "discussion");
            GetDiscussionComments(onSuccess, onFailure, discussion.Id, fields);
        }

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the discussion's comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussionId">The ID of the discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetDiscussionComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, string discussionId, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="comment">The comment to update</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated comment</returns>
        public Comment Update(Comment comment, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(comment, "comment");
            return UpdateComment(comment.Id, comment.Message, fields);
        }

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="id">The ID of the comment to update</param>
        /// <param name="message">The comment's new message</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated comment</returns>
        public Comment UpdateComment(string id, string message, IEnumerable<CommentField> fields = null)
        {
            var request = _requestHelper.Update(ResourceType.Comment, id, null, fields, message: message);
            return _restClient.ExecuteAndDeserialize<Comment>(request);
        }

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="comment">The comment to update</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Update(Action<Comment> onSuccess, Action<Error> onFailure, Comment comment, IEnumerable<CommentField> fields = null)
        {
            GuardFromNull(comment, "comment");
            UpdateComment(onSuccess, onFailure, comment.Id, comment.Message, fields);
        }

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="id">The ID of the comment to update</param>
        /// <param name="message">The comment's new message</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void UpdateComment(Action<Comment> onSuccess, Action<Error> onFailure, string id, string message, IEnumerable<CommentField> fields = null)
        {
            var request = _requestHelper.Update(ResourceType.Comment, id, null, fields, message: message);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="comment">The comment to delete</param>
        public void Delete(Comment comment)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(comment.Id);
        }

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="id">The ID of the comment to delete</param>
        public void DeleteComment(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteComment(id);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="comment">The comment to delete</param>
        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Comment comment)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(onSuccess, onFailure, comment.Id);
        }

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="id">The ID of the comment to delete</param>
        public void DeleteComment(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteComment(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}