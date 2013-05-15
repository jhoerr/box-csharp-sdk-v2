using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with shared Box folders
    /// </summary>
    public interface ISharedFolder
    {
        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="folder">The folder to get</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>The fetched folder.</returns>
        Folder Get(Folder folder, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved Folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to get</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        void Get(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="id">The ID of the folder to get</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>The fetched folder.</returns>
        Folder GetFolder(string id, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieves a shared folder
        /// </summary>
        /// <param name="id">The ID of the folder to get</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>The fetched folder.</returns>
        Folder GetFolder(string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved Folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to get</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        void GetFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved Folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to get</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        void GetFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="folder">The folder containing the items to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="limit">The maximum number of items to return in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The first item to fetch in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>A collection of items representing the folder's contents.</returns>
        ItemCollection GetItems(Folder folder, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="limit">The maximum number of items to return in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The first item to fetch in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>A collection of items representing the folder's contents.</returns>
        ItemCollection GetItems(string id, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieve a shared folder's items
        /// </summary>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="limit">The maximum number of items to return in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The first item to fetch in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>A collection of items representing the folder's contents.</returns>
        ItemCollection GetItems(string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's items</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder containing the items to retrieve</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (default: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request (default: 0).</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's items</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request.</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, string id, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null);

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's items</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request.</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null);

    }
}