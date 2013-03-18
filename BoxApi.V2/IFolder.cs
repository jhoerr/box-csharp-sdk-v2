using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;
using RestSharp;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working wtih Box folders
    /// </summary>
    public interface IFolder : ISharedFolder
    {
        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created folder.</returns>
        Folder CreateFolder(Folder parent, string name, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created folder.</returns>
        Folder CreateFolder(string parentId, string name, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created Folder</param>
        /// <param name="onFailure">Action to perform following a failed Folder creation</param>
        /// <param name="parent">The folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFolder(Action<Folder> onSuccess, Action<Error> onFailure, Folder parent, string name, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Creates a new folder in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created folder</param>
        /// <param name="onFailure">Action to perform following a failed folder creation</param>
        /// <param name="parentId">The ID of he folder in which to create the folder</param>
        /// <param name="name">The folder's name</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFolder(Action<Folder> onSuccess, Action<Error> onFailure, string parentId, string name, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="folder">The folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        void Delete(Folder folder, bool recursive = false, string etag = null);

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="id">The ID of the folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        void DeleteFolder(string id, bool recursive = false, string etag = null);

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform when the folder is deleted</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Folder folder, bool recursive = false, string etag = null);

        /// <summary>
        ///     Removes a folder from a user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform when the folder is deleted</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to delete</param>
        /// <param name="recursive">Remove a non-empty folder and all of its contents</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <exception cref="BoxException">Thrown if folder is not empty and recursive is false.</exception>
        void DeleteFolder(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id, bool recursive = false, string etag = null);

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParent">The destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        Folder Copy(Folder folder, Folder newParent, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        Folder Copy(Folder folder, string newParentId, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        Folder CopyFolder(string id, string newParentId, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Copies a shared folder to the specified folder
        /// </summary>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="sharedLinkUrl">The shared link for the folder</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied folder</returns>
        Folder CopyFolder(string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParent">The destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Copy(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Folder newParent, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Copy(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newParentId, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Copies a folder to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied folder</param>
        /// <param name="newName">The optional new name for the copied folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CopyFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newParentId, string newName = null, IEnumerable<FolderField> fields = null);

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
        void CopyFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FolderField> fields = null);

        /// <summary>
        ///     Creates a shared link to the specified folder
        /// </summary>
        /// <param name="folder">The folder for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An object populated with the shared link</returns>
        /// <remarks>In order for Folder.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        Folder ShareLink(Folder folder, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Creates a shared link to the specified folder
        /// </summary>
        /// <param name="id">The ID of the folder for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An object populated with the shared link</returns>
        /// <remarks>In order for Folder.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        Folder ShareFolderLink(string id, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null);

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
        void ShareLink(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null);

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
        void ShareFolderLink(Action<Folder> onSuccess, Action<Error> onFailure, string id, SharedLink sharedLink, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="folder">The folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The folder with the disabled shared link</returns>
        Folder DisableSharedLink(Folder folder, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="id">The id of the folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The folder with the disabled shared link</returns>
        Folder DisableSharedLink(string id, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="onSuccess">Action to take with the returned folder</param>
        /// <param name="onFailure">Actino to take following a failed folder operation</param>
        /// <param name="folder">The folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void DisableSharedLink(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        /// Disables the shared link for a folder
        /// </summary>
        /// <param name="onSuccess">Action to take with the returned folder</param>
        /// <param name="onFailure">Actino to take following a failed folder operation</param>
        /// <param name="id">The id of the folder whose shared link should be disabled.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void DisableSharedLink(Action<Folder> onSuccess, Action<Error> onFailure, string id, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved folder</returns>
        Folder Move(Folder folder, Folder newParent, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved folder</returns>
        Folder Move(Folder folder, string newParentId, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="id">The ID of the folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved folder</returns>
        Folder MoveFolder(string id, string newParentId, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Move(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Folder newParent, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Move(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newParentId, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a folder to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void MoveFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newParentId, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="folder">The folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed folder</returns>
        Folder Rename(Folder folder, string newName, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="id">The ID of the folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed folder</returns>
        Folder RenameFolder(string id, string newName, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Rename(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newName, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="id">The ID of the folder to rename</param>
        /// <param name="newName">The new name for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void RenameFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newName, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="folder">The folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated folder</returns>
        Folder UpdateDescription(Folder folder, string description, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="id">The ID of the folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated folder</returns>
        Folder UpdateFolderDescription(string id, string description, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a folder's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to update</param>
        /// <param name="description">The new description for the folder</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void UpdateDescription(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string description, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Update one or more of a folder's name, description, parent, or shared link.
        /// </summary>
        /// <param name="folder">The folder to update</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated folder</returns>
        Folder Update(Folder folder, IEnumerable<FolderField> fields = null, string etag = null);

        /// <summary>
        ///     Update one or more of a folder's name, description, parent, or shared link.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the updated folder</param>
        /// <param name="onFailure">Action to perform following a failed operation</param>
        /// <param name="folder">The folder to update</param>
        /// <param name="fields">The properties that should be set on the returned Folder object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Update(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, IEnumerable<FolderField> fields = null, string etag = null);
    }
}