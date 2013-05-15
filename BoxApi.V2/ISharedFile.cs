using System;
using System.Collections.Generic;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Fields;

namespace BoxApi.V2
{
    /// <summary>
    /// Methods for working with shared Box files
    /// </summary>
    public interface ISharedFile
    {
        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="file">The file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The fetched file</returns>
        File Get(File file, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The fetched file</returns>
        File GetFile(string id, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Retrieves a shared file
        /// </summary>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The shared file</returns>
        File GetFile(string id, string sharedLinkUrl, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to get</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void Get(Action<File> onSuccess, Action<Error> onFailure, File file, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, IEnumerable<FileField> fields = null, string etag = null);

        /// <summary>
        ///     Retrieves a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, IEnumerable<FileField> fields = null, string etag = null);
    }
}