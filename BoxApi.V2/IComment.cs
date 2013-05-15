using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;
using RestSharp;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with Box comments
    /// </summary>
    public interface IComment
    {
        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="file">The file on which to commment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The file's new comment</returns>
        Comment CreateComment(File file, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="fileId">The ID of the file on which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new comment</returns>
        Comment CreateFileComment(string fileId, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="file">The file on which to commment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateComment(Action<Comment> onSuccess, Action<Error> onFailure, File file, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a file
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="fileId">The ID of the file on which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFileComment(Action<Comment> onSuccess, Action<Error> onFailure, string fileId, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="discussion">The discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's new comment</returns>
        Comment CreateComment(Discussion discussion, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="discussionId">The ID of the discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's new comment</returns>
        Comment CreateDiscussionComment(string discussionId, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussion">The discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateComment(Action<Comment> onSuccess, Action<Error> onFailure, Discussion discussion, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Add a comment to a discussion
        /// </summary>
        /// <param name="onSuccess">The action to perform with the added comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussionId">The ID of the discussion in which to comment</param>
        /// <param name="message">The message to add</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateDiscussionComment(Action<Comment> onSuccess, Action<Error> onFailure, string discussionId, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="comment">The comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved comment</returns>
        Comment GetComment(Comment comment, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved comment</returns>
        Comment GetComment(string id, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="comment">The comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetComment(Action<Comment> onSuccess, Action<Error> onFailure, Comment comment, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="id">The ID of the comment to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetComment(Action<Comment> onSuccess, Action<Error> onFailure, string id, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="file">The file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The file's comments</returns>
        CommentCollection GetComments(File file, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="fileId">The ID of the file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The file's comments</returns>
        CommentCollection GetFileComments(string fileId, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved file comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="file">The file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, File file, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments for a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved file comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="fileId">The ID of the file whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetFileComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, string fileId, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="discussion">The discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's comments</returns>
        CommentCollection GetComments(Discussion discussion, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="discussionId">The ID of the discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The discussion's comments</returns>
        CommentCollection GetDiscussionComments(string discussionId, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the discussion's comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussion">The discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, Discussion discussion, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Retrieves all the comments in a discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the discussion's comments</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="discussionId">The ID of the discussion whose comments are to be retrieved</param>
        /// <param name="fields">The properties that should be set on the returned CommentCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetDiscussionComments(Action<CommentCollection> onSuccess, Action<Error> onFailure, string discussionId, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="comment">The comment to update</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated comment</returns>
        Comment Update(Comment comment, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="id">The ID of the comment to update</param>
        /// <param name="message">The comment's new message</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated comment</returns>
        Comment UpdateComment(string id, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="comment">The comment to update</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Update(Action<Comment> onSuccess, Action<Error> onFailure, Comment comment, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Updates a comment's message
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated comment</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="id">The ID of the comment to update</param>
        /// <param name="message">The comment's new message</param>
        /// <param name="fields">The properties that should be set on the returned Comment.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void UpdateComment(Action<Comment> onSuccess, Action<Error> onFailure, string id, string message, IEnumerable<CommentField> fields = null);

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="comment">The comment to delete</param>
        void Delete(Comment comment);

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="id">The ID of the comment to delete</param>
        void DeleteComment(string id);

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="comment">The comment to delete</param>
        void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Comment comment);

        /// <summary>
        ///     Deletes a comment
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed Comment operation</param>
        /// <param name="id">The ID of the comment to delete</param>
        void DeleteComment(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id);
    }
}