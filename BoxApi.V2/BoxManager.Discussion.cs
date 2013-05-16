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
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="folder">The folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new discussion</returns>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public Discussion CreateDiscussion(Folder folder, string name, string description = null, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return CreateDiscussion(folder.Id, name, description, fields);
        }

        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="folderId">The ID of the folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The new discussion</returns>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public Discussion CreateDiscussion(string folderId, string name, string description = null, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateDiscussion(folderId, name, description, fields);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new discussion</param>
        /// <param name="onFailure">Action to perform following a failed creation</param>
        /// <param name="folder">The folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void CreateDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, Folder folder, string name, string description = null, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folder, "folder");
            CreateDiscussion(onSuccess, onFailure, folder.Id, name, description, fields);
        }

        /// <summary>
        ///     Creates a new discussion for a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the new discussion</param>
        /// <param name="onFailure">Action to perform following a failed creation</param>
        /// <param name="folderId">The ID of the folder for which to create a discussion</param>
        /// <param name="name">The name of the discussion</param>
        /// <param name="description">An optional discription of the discussion</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void CreateDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, string folderId, string name, string description = null, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateDiscussion(folderId, name, description, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="discussion">The discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved discussion</returns>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public Discussion GetDiscussion(Discussion discussion, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(discussion, "discussion");
            var request = _requestHelper.Get(ResourceType.Discussion, discussion.Id, fields);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="id">The ID of the discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The retrieved discussion</returns>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public Discussion GetDiscussion(string id, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Discussion, id, fields);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved discussion</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="discussion">The discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void GetDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, Discussion discussion, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(discussion, "discussion");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Discussion, discussion.Id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves an existing discussion
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved discussion</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="id">The ID of the discussion to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned Discussion.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void GetDiscussion(Action<Discussion> onSuccess, Action<Error> onFailure, string id, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Discussion, id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="folder">The folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's dicussions</returns>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public DiscussionCollection GetDiscussions(Folder folder, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetDiscussions(folder.Id, fields);
        }

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="folderId">The ID of the folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The folder's dicussions</returns>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public DiscussionCollection GetDiscussions(string folderId, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetDiscussions(folderId, fields);
            return _restClient.ExecuteAndDeserialize<DiscussionCollection>(request);
        }

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's discussions</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="folder">The folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void GetDiscussions(Action<DiscussionCollection> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folder, "folder");
            GetDiscussions(onSuccess, onFailure, folder.Id, fields);
        }

        /// <summary>
        ///     Retrieves all discussions for the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's discussions</param>
        /// <param name="onFailure">Action to perform following a failed retrieval</param>
        /// <param name="folderId">The ID of the folder whose dicussions should be retrieved</param>
        /// <param name="fields">The properties that should be set on the Entries of the returned DiscussionCollection.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void GetDiscussions(Action<DiscussionCollection> onSuccess, Action<Error> onFailure, string folderId, IEnumerable<DiscussionField> fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetDiscussions(folderId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="discussion">The discussion to delete</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void Delete(Discussion discussion)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(discussion.Id);
        }

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="id">The ID of the discussion to delete</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void DeleteDiscussion(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteDiscussion(id);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed delete</param>
        /// <param name="discussion">The discussion to delete</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Discussion discussion)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(onSuccess, onFailure, discussion.Id);
        }

        /// <summary>
        ///     Deletes a discussion from a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed delete</param>
        /// <param name="id">The ID of the discussion to delete</param>
        [Obsolete("Discussions have been deprecated from the v2 API.  This method will be removed in a future version.")]
        public void DeleteDiscussion(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteDiscussion(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}