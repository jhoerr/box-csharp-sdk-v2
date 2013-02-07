using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        /// <summary>
        ///     Gets information about a shared item.
        /// </summary>
        /// <typeparam name="T">The shared item's type (File or Folder)</typeparam>
        /// <param name="sharedLinkUrl">The link to the shared item (SharedLink.Url)</param>
        /// <param name="fields">The properties that should be set on the returned File/Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The shared File/Folder</returns>
        /// <remarks>The user does not need an authorization token to use this method. Only the API key and shared link url are required.</remarks>
        public T GetSharedItem<T>(string sharedLinkUrl, IEnumerable<IContentField> fields = null, string etag = null) 
            where T : File, new()
        {
            var request = _requestHelper.Get(ResourceType.SharedItem, fields, etag);
            return _restClient.WithSharedLink(sharedLinkUrl).ExecuteAndDeserialize<T>(request);
        }

        /// <summary>
        ///     Gets information about a shared item.
        /// </summary>
        /// <typeparam name="T">The shared item's type (File or Folder)</typeparam>
        /// <param name="onSuccess">Action to perform with the shared item</param>
        /// <param name="onFailure">Action to perform on a failed Shared Item operation</param>
        /// <param name="sharedLinkUrl">The link to the shared item (SharedLink.Url)</param>
        /// <param name="fields">The properties that should be set on the returned File/Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <remarks>The user does not need an authorization token to use this method. Only the API key and shared link url are required.</remarks>
        public void GetSharedItem<T>(Action<T> onSuccess, Action<Error> onFailure, string sharedLinkUrl, IEnumerable<IContentField> fields = null, string etag = null) 
            where T : File, new() 
        {
            var request = _requestHelper.Get(ResourceType.SharedItem, fields, etag);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}