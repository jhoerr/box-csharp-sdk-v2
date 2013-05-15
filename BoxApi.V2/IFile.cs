using System;
using System.Collections.Generic;
using System.IO;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with Box files
    /// </summary>
    public interface IFile : ISharedFile
    {
        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        File CreateFile(Folder parent, string name, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        File CreateFile(string parentId, string name, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        File CreateFile(Folder parent, string name, Stream content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        File CreateFile(string parentId, string name, Stream content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        File CreateFile(Folder parent, string name, byte[] content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        File CreateFile(string parentId, string name, byte[] content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="content">The file's data</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, Stream content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="content">The file's data</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, Stream content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, byte[] content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, byte[] content, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="file">The file to read</param>
        /// <returns>The raw file contents</returns>
        byte[] Read(File file);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <returns>The raw file contents</returns>
        byte[] Read(string id);

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <returns>The raw file contents</returns>
        byte[] Read(string id, string sharedLinkUrl);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="file">The file to read</param>
        /// <param name="output">A stream to which the file contents will be written</param>
        void Read(File file, Stream output);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="output">A stream to which the file contents will be written</param>
        void Read(string id, Stream output);

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="output">A stream to which the file contents will be written</param>
        void Read(string id, string sharedLinkUrl, Stream output);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file contents</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to read</param>
        void Read(Action<byte[]> onSuccess, Action<Error> onFailure, File file);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file contents</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        void Read(Action<byte[]> onSuccess, Action<Error> onFailure, string id);

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file contents</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        void Read(Action<byte[]> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file stream</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to read</param>
        void ReadToStream(Action<Stream> onSuccess, Action<Error> onFailure, File file);

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file stream</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        void ReadToStream(Action<Stream> onSuccess, Action<Error> onFailure, string id);

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file stream</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        void ReadToStream(Action<Stream> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        File Write(File file, Stream content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="content">The file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        File Write(File file, byte[] content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        File Write(string id, string name, Stream content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        File Write(string id, string name, byte[] content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="content">A stream containing the file's data</param>
        void Write(Action<File> onSuccess, Action<Error> onFailure, File file, Stream content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="content">The file's data</param>
        void Write(Action<File> onSuccess, Action<Error> onFailure, File file, byte[] content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Write(Action<File> onSuccess, Action<Error> onFailure, string id, string name, Stream content, string etag = null);

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Write(Action<File> onSuccess, Action<Error> onFailure, string id, string name, byte[] content, string etag = null);

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="file">The file to copy</param>
        /// <param name="newParent">The destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        File Copy(File file, Folder newParent, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="file">The file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        File Copy(File file, string newParentId, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        File CopyFile(string id, string newParentId, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a shared file to the specified folder
        /// </summary>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        File CopyFile(string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to copy</param>
        /// <param name="newParent">The destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Copy(Action<File> onSuccess, Action<Error> onFailure, File file, Folder newParent, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful File operation</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Copy(Action<File> onSuccess, Action<Error> onFailure, File file, string newParentId, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful File operation</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CopyFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newParentId, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Copies a shared file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful File operation</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void CopyFile(Action<File> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="file">The file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>A file object populated with the shared link</returns>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        File ShareLink(File file, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="id">The ID of the file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>A file object populated with the shared link</returns>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        File ShareFileLink(string id, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the linked file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        void ShareLink(Action<File> onSuccess, Action<Error> onFailure, File file, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the linked file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        void ShareFileLink(Action<File> onSuccess, Action<Error> onFailure, string id, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved file</returns>
        File Move(File file, Folder newParent, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved file</returns>
        File Move(File file, string newParentId, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="id">The ID of the file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved file</returns>
        File MoveFile(string id, string newParentId, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Move(Action<File> onSuccess, Action<Error> onFailure, File file, Folder newParent, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Move(Action<File> onSuccess, Action<Error> onFailure, File file, string newParentId, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void MoveFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newParentId, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="file">The file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed file</returns>
        File Rename(File file, string newName, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="id">The ID of the file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed file</returns>
        File RenameFile(string id, string newName, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Rename(Action<File> onSuccess, Action<Error> onFailure, File file, string newName, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void RenameFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newName, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated file</returns>
        File UpdateDescription(File file, string description, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated file</returns>
        File UpdateFileDescription(string id, string description, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void UpdateDescription(Action<File> onSuccess, Action<Error> onFailure, File file, string description, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void UpdateFileDescription(Action<File> onSuccess, Action<Error> onFailure, string id, string description, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Update one or more of a file's name, description, parent, or shared link.
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated file</returns>
        File Update(File file, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Update one or more of a file's name, description, parent, or shared link.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Update(Action<File> onSuccess, Action<Error> onFailure, File file, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="file">The file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Delete(File file, string etag = null);

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="id">The ID of the file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void DeleteFile(string id, string etag = null);

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void Delete(Action onSuccess, Action<Error> onFailure, File file, string etag = null);

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        void DeleteFile(Action onSuccess, Action<Error> onFailure, string id, string etag = null);

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="file">The file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of metadata objects</returns>
        VersionCollection GetVersions(File file, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="fileId">The ID of the file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of metadata objects</returns>
        VersionCollection GetVersions(string fileId, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved metadata</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetVersions(Action<VersionCollection> onSuccess, Action<Error> onFailure, File file, IEnumerable<FileField> fields = null);

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved metadata</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="fileId">The ID of the file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetVersions(Action<VersionCollection> onSuccess, Action<Error> onFailure, string fileId, IEnumerable<FileField> fields = null);

        byte[] GetThumbnail(File file, ThumbnailSize? minHeight = null, ThumbnailSize? minWidth = null, ThumbnailSize? maxHeight = null, ThumbnailSize? maxWidth = null, string extension = "png");
    }
}