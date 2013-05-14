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
        private const int MaxItems = 1000;

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created folder.</returns>
        public Folder CreateFolder(Folder parent, string name, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(parent, "parent");
            return CreateFolder(parent.Id, name, fields);
        }

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created folder.</returns>
        public Folder CreateFolder(string parentId, string name, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created Folder</param>
        /// <param name="onFailure">Action to perform following a failed Folder creation</param>
        /// <param name="parent">The folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFolder(Action<Folder> onSuccess, Action<Error> onFailure, Folder parent, string name, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(parent, "parent");
            CreateFolder(onSuccess, onFailure, parent.Id, name, fields);
        }

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created folder</param>
        /// <param name="onFailure">Action to perform following a failed folder creation</param>
        /// <param name="parentId">The ID of he folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFolder(Action<Folder> onSuccess, Action<Error> onFailure, string parentId, string name, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFolder(parentId, name, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="folder">The folder to get</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>The fetched folder.</returns>
        public Folder Get(Folder folder, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(folder, "folder");
            return GetFolder(folder.Id, fields, etag, limit, offset);
        }

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
        public void Get(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(folder, "folder");
            GetFolder(onSuccess, onFailure, folder.Id, fields, etag, limit, offset);
        }

        /// <summary>
        ///     Retrieves a folder
        /// </summary>
        /// <param name="id">The ID of the folder to get</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>The fetched folder.</returns>
        public Folder GetFolder(string id, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            return DoGetFolderSync(id, fields, etag, limit, offset, client => client);
        }

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
        public Folder GetFolder(string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            return DoGetFolderSync(id, fields, etag, limit, offset, client => client.WithSharedLink(sharedLinkUrl));
        }

        private Folder DoGetFolderSync(string id, IEnumerable<FolderField> fields, string etag, int? limit, int? offset, Func<BoxRestClient, BoxRestClient> prepareClient)
        {
            GuardFromNull(id, "id");
            var paginate = limit.HasValue || offset.HasValue;
            var request = _requestHelper.Get(ResourceType.Folder, id, fields, etag, paginate ? limit : MaxItems, paginate ? offset : 0);
            var folder = prepareClient(_restClient).ExecuteAndDeserialize<Folder>(request);

            if (!paginate)
            {
                offset = MaxItems;
                while (folder.ItemCollection.Entries.Count < folder.ItemCollection.TotalCount)
                {
                    request = _requestHelper.Get(ResourceType.Folder, id, fields, etag, MaxItems, offset);
                    var next = prepareClient(_restClient).ExecuteAndDeserialize<Folder>(request);
                    folder.ItemCollection.Entries.AddRange(next.ItemCollection.Entries);
                    offset += MaxItems;
                }
            }
            return folder;
        }

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
        public void GetFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Folder, id, fields, etag, limit, offset);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

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
        public void GetFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Folder, id, fields, etag, limit, offset);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="folder">The folder containing the items to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="limit">The maximum number of items to return in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The first item to fetch in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>A collection of items representing the folder's contents.</returns>
        public ItemCollection GetItems(Folder folder, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(folder, "folder");
            return GetItems(folder.Id, fields, limit, offset);
        }

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="limit">The maximum number of items to return in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The first item to fetch in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>A collection of items representing the folder's contents.</returns>
        public ItemCollection GetItems(string id, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            return DoGetItemsSync(id, fields, limit, offset, client => client);
        }

        /// <summary>
        ///     Retrieve a shared folder's items
        /// </summary>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="limit">The maximum number of items to return in this request (max: 1000).  If limit and offset are both left null, all items will be fetched.</param>
        /// <param name="offset">The first item to fetch in this request. If limit and offset are both left null, all items will be fetched.</param>
        /// <returns>A collection of items representing the folder's contents.</returns>
        public ItemCollection GetItems(string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            return DoGetItemsSync(id, fields, limit, offset, client => client.WithSharedLink(sharedLinkUrl));
        }

        private ItemCollection DoGetItemsSync(string id, IEnumerable<FolderField> fields, int? limit, int? offset, Func<BoxRestClient, BoxRestClient> prepareClient)
        {
            GuardFromNull(id, "id");
            var paginate = limit.HasValue || offset.HasValue;
            var request = _requestHelper.GetItems(id, fields, paginate ? limit : MaxItems, paginate ? offset : 0);
            var items = prepareClient(_restClient).ExecuteAndDeserialize<ItemCollection>(request);

            if (!paginate)
            {
                while (items.Entries.Count < items.TotalCount)
                {
                    request = _requestHelper.GetItems(id, fields, MaxItems, items.Entries.Count);
                    var next = prepareClient(_restClient).ExecuteAndDeserialize<ItemCollection>(request);
                    items.Entries.AddRange(next.Entries);
                }
            }
            return items;
        }

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's items</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder containing the items to retrieve</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (default: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request (default: 0).</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(folder, "folder");
            GetItems(onSuccess, onFailure, folder.Id, fields, limit, offset);
        }

        /// <summary>
        ///     Retrieve a folder's items
        /// </summary>
        /// <param name="onSuccess">Action to perform with the folder's items</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder containing the items to retrieve</param>
        /// <param name="limit">The maximum number of items to return with the folder's ItemCollection in this request (max: 1000).</param>
        /// <param name="offset">The item at which to start fetching the folder's ItemCollection in this request.</param>
        /// <param name="fields">The properties that should be set on the returned items.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, string id, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(fields, "fields");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var folderItems = _requestHelper.GetItems(id, fields, limit, offset);
            _restClient.ExecuteAsync(folderItems, onSuccess, onFailure);
        }

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
        public void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(fields, "fields");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var folderItems = _requestHelper.GetItems(id, fields, limit, offset);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(folderItems, onSuccess, onFailure);
        }

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="folder">The folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        public void Delete(Folder folder, bool recursive = false, string etag = null)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(folder.Id, recursive, etag);
        }

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="id">The ID of the folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        public void DeleteFolder(string id, bool recursive = false, string etag = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteFolder(id, recursive, etag);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform when the folder is deleted</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Folder folder, bool recursive = false, string etag = null)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(onSuccess, onFailure, folder.Id, recursive, etag);
        }

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform when the folder is deleted</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        public void DeleteFolder(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id, bool recursive = false, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFolder(id, recursive, etag);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParent">The destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        public Folder Copy(Folder folder, Folder newParent, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParent.Id, newName, fields);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        public Folder Copy(Folder folder, string newParentId, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParentId, newName, fields);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        public Folder CopyFolder(string id, string newParentId, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Copies a shared folder to the specified folder
        /// </summary>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        public Folder CopyFolder(string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            return _restClient.WithSharedLink(sharedLinkUrl).ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParent">The destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Copy(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Folder newParent, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(newParent, "newParent");
            Copy(onSuccess, onFailure, folder, newParent.Id, newName, fields);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Copy(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newParentId, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(folder, "folder");
            CopyFolder(onSuccess, onFailure, folder.Id, newParentId, newName, fields);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CopyFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newParentId, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CopyFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FolderField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Creates a shared link to the specified folder
        /// </summary>
        /// <param name="folder">The folder for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An object populated with the shared link</returns>
        /// <remarks>In order for Folder.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public Folder ShareLink(Folder folder, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            return ShareFolderLink(folder.Id, sharedLink, fields, etag);
        }

        /// <summary>
        ///     Creates a shared link to the specified folder
        /// </summary>
        /// <param name="id">The ID of the folder for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An object populated with the shared link</returns>
        /// <remarks>In order for Folder.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public Folder ShareFolderLink(string id, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Creates a shared link to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the linked folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <remarks>In order for Folder.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public void ShareLink(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            ShareFolderLink(onSuccess, onFailure, folder.Id, sharedLink, fields, etag);
        }

        /// <summary>
        ///     Creates a shared link to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the linked folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <remarks>In order for Folder.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public void ShareFolderLink(Action<Folder> onSuccess, Action<Error> onFailure, string id, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="folder">The folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The folder with the disabled shared link</returns>
        public Folder DisableSharedLink(Folder folder, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            return DisableSharedLink(folder.Id, fields, etag);
        }

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="id">The id of the folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The folder with the disabled shared link</returns>
        public Folder DisableSharedLink(string id, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.DisableSharedLink(ResourceType.Folder, id, etag, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="onSuccess">Action to take with the returned folder</param>
        /// <param name="onFailure">Actino to take following a failed folder operation</param>
        /// <param name="folder">The folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void DisableSharedLink(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            DisableSharedLink(onSuccess, onFailure, folder.Id, fields, etag);
        }

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="onSuccess">Action to take with the returned folder</param>
        /// <param name="onFailure">Actino to take following a failed folder operation</param>
        /// <param name="id">The id of the folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void DisableSharedLink(Action<Folder> onSuccess, Action<Error> onFailure, string id, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.DisableSharedLink(ResourceType.Folder, id, etag, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved folder</returns>
        public Folder Move(Folder folder, Folder newParent, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(newParent, "newParent");
            return Move(folder, newParent.Id, fields, etag);
        }

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved folder</returns>
        public Folder Move(Folder folder, string newParentId, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            return MoveFolder(folder.Id, newParentId, fields, etag);
        }

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="id">The ID of the folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved folder</returns>
        public Folder MoveFolder(string id, string newParentId, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, newParentId);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Move(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Folder newParent, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(newParent, "newParent");
            Move(onSuccess, onFailure, folder, newParent.Id, fields, etag);
        }

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Move(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newParentId, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            MoveFolder(onSuccess, onFailure, folder.Id, newParentId, fields, etag);
        }

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void MoveFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newParentId, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="folder">The folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed folder</returns>
        public Folder Rename(Folder folder, string newName, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            return RenameFolder(folder.Id, newName, fields, etag);
        }

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="id">The ID of the folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed folder</returns>
        public Folder RenameFolder(string id, string newName, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, name: newName);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Rename(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newName, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            RenameFolder(onSuccess, onFailure, folder.Id, newName, fields, etag);
        }

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void RenameFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newName, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="folder">The folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated folder</returns>
        public Folder UpdateDescription(Folder folder, string description, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            return UpdateFolderDescription(folder.Id, description, fields, etag);
        }

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="id">The ID of the folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated folder</returns>
        public Folder UpdateFolderDescription(string id, string description, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, description: description);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void UpdateDescription(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string description, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            UpdateFolderDescription(onSuccess, onFailure, folder.Id, description, fields, etag);
        }

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        private void UpdateFolderDescription(Action<Folder> onSuccess, Action<Error> onFailure, string id, string description, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.Folder, id, etag, fields, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Update one or more of a folder's name, description, parent, or shared link.
        /// </summary>
        /// <param name="folder">The folder to update</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated folder</returns>
        public Folder Update(Folder folder, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, etag, fields, parentId, folder.Name, folder.Description, folder.SharedLink);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        /// <summary>
        ///     Update one or more of a folder's name, description, parent, or shared link.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to update</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Update(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, string etag = null)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, etag, fields, parentId, folder.Name, folder.Description, folder.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}