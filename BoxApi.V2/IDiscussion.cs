using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;
using RestSharp;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with Box discussions
    /// </summary>
    public interface IDiscussion
    {
        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="folder">The folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new discussion</returns>
        Discussion CreateDiscussion(Folder folder, string name, string description = null, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="folderId">The ID of the folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new discussion</returns>
        Discussion CreateDiscussion(string folderId, string name, string description = null, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new discussion</param>
        /// <param name="onFailure">Action to perform following a failed creation</param>
        /// <param name="folder">The folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, Folder folder, string name, string description = null, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new discussion</param>
        /// <param name="onFailure">Action to perform following a failed creation</param>
        /// <param name="folderId">The ID of the folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, string folderId, string name, string description = null, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="discussion">The discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved discussion</returns>
        Discussion GetDiscussion(Discussion discussion, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="id">The ID of the discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved discussion</returns>
        Discussion GetDiscussion(string id, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved discussion</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="discussion">The discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, Discussion discussion, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved discussion</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="id">The ID of the discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, string id, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="folder">The folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's dicussions</returns>
        DiscussionCollection GetDiscussions(Folder folder, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="folderId">The ID of the folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's dicussions</returns>
        DiscussionCollection GetDiscussions(string folderId, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's discussions</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="folder">The folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetDiscussions(Action<DiscussionCollection> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's discussions</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="folderId">The ID of the folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetDiscussions(Action<DiscussionCollection> onSuccess, Action<Error> onFailure, string folderId, IEnumerable<DiscussionField> fields = null);

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="discussion">The discussion to delete</param>
        void Delete(Discussion discussion);

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="id">The ID of the discussion to delete</param>
        void DeleteDiscussion(string id);

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed delete</param>
        /// <param name="discussion">The discussion to delete</param>
        void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Discussion discussion);

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed delete</param>
        /// <param name="id">The ID of the discussion to delete</param>
        void DeleteDiscussion(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id);
    }
}