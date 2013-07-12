using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using RestSharp;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        private const int MaxFileGetAttempts = 4;

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(Folder parent, string name, IEnumerable<FileField> fields = null)
        {
            return CreateFile(parent, name, new byte[0], fields);
        }

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(string parentId, string name, IEnumerable<FileField> fields = null)
        {
            return CreateFile(parentId, name, new byte[0], fields);
        }

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(Folder parent, string name, Stream content, IEnumerable<FileField> fields = null)
        {
            return CreateFile(parent, name, ReadFully(content), fields);
        }

        /// <summary>
        ///     Creates a new empty file in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(string parentId, string name, Stream content, IEnumerable<FileField> fields = null)
        {
            return CreateFile(parentId, name, ReadFully(content), fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(Folder parent, string name, byte[] content, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(parent, "folder");
            return CreateFile(parent.Id, name, content, fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(string parentId, string name, byte[] content, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            IRestRequest request = _requestHelper.CreateFile(parentId, name, content, fields);
            return WriteFile(request);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, IEnumerable<FileField> fields = null)
        {
            CreateFile(onSuccess, onFailure, parent, name, new byte[0], fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, IEnumerable<FileField> fields = null)
        {
            CreateFile(onSuccess, onFailure, parentId, name, new byte[0], fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="content">The file's data</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, Stream content, IEnumerable<FileField> fields = null)
        {
            CreateFile(onSuccess, onFailure, parent, name, ReadFully(content), fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="content">The file's data</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, Stream content, IEnumerable<FileField> fields = null)
        {
            CreateFile(onSuccess, onFailure, parentId, name, ReadFully(content), fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, byte[] content, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(parent, "folder");
            CreateFile(onSuccess, onFailure, parent.Id, name, content, fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, byte[] content, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.CreateFile(parentId, name, content, fields);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            Action<ItemCollection> onSuccessWrapper = items => GetFile(onSuccess, onFailure, items.Entries.Single().Id, fields);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="file">The file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The fetched file</returns>
        public File Get(File file, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            return GetFile(file.Id, fields, etag);
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The fetched file</returns>
        public File GetFile(string id, IEnumerable<FileField> fields = null, string etag = null)
        {
            return GetFile(id, restRequest => _restClient.ExecuteAndDeserialize<File>(restRequest), fields, etag);
        }

        /// <summary>
        ///     Retrieves a shared file
        /// </summary>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <returns>The shared file</returns>
        public File GetFile(string id, string sharedLinkUrl, IEnumerable<FileField> fields = null, string etag = null)
        {
            return GetFile(id, _restClient.WithSharedLink(sharedLinkUrl).ExecuteAndDeserialize<File>, fields, etag);
        }

        private File GetFile(string id, Func<IRestRequest, File> getFileOperation, IEnumerable<FileField> fields = null, string etag = null)
        {
            IRestRequest request = _requestHelper.Get(ResourceType.File, id, fields, etag);
            return getFileOperation(request);
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to get</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Get(Action<File> onSuccess, Action<Error> onFailure, File file, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            GetFile(onSuccess, onFailure, file.Id, fields, etag);
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, IEnumerable<FileField> fields = null, string etag = null)
        {
            GetFile(onSuccess, onFailure, id, _restClient.ExecuteAsync, fields, etag);
        }

        /// <summary>
        ///     Retrieves a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="etag">Include the item's etag to prevent unnecessary response data if you already have the latest version of the item.  A BoxItemNotModifiedException will be thrown if the item is up to date.</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, IEnumerable<FileField> fields = null, string etag = null)
        {
            GetFile(onSuccess, onFailure, id, _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync, fields, etag);
        }

        private void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, Action<IRestRequest, Action<File>, Action<Error>> getFileAsync, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);

            IRestRequest request = _requestHelper.Get(ResourceType.File, id, fields, etag);
            getFileAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="file">The file to read</param>
        /// <returns>The raw file contents</returns>
        public byte[] Read(File file)
        {
            GuardFromNull(file, "file");
            return Read(file.Id);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <returns>The raw file contents</returns>
        public byte[] Read(string id)
        {
            GuardFromNull(id, "id");
            try
            {
                IRestRequest request = _requestHelper.Read(id);
                return _restClient.Execute(request).RawBytes;
            }
            catch (BoxDownloadNotReadyException e)
            {
                Thread.Sleep(e.RetryAfter*1000);
                return Read(id);
            }
        }

        /// <summary>
        ///     Retrieve a range of the specified file content
        /// </summary>
        /// <param name="file">The file to read</param>
        /// <param name="firstByte">The first byte to of the range</param>
        /// <param name="lastByte">The last byte of the range</param>
        /// <returns>The raw file content range</returns>
        public byte[] Read(File file, int firstByte, int lastByte)
        {
            return Read(file.Id, firstByte, lastByte);
        }


        /// <summary>
        ///     Retrieve a range of the specified file content
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="firstByte">The first byte to of the range</param>
        /// <param name="lastByte">The last byte of the range</param>
        /// <returns>The raw file content range</returns>
        public byte[] Read(string id, long firstByte, long lastByte)
        {
            GuardFromNull(id, "id");
            try
            {
                var downloadUrl = GetDownloadUrl(id);
                var restRequest = new RestRequest((string)downloadUrl.Value);
                restRequest.AddHeader("Range", string.Format("bytes={0}-{1}", firstByte, lastByte));
                return _downloadClient.Execute(restRequest).RawBytes;
            }
            catch (BoxDownloadNotReadyException e)
            {
                Thread.Sleep(e.RetryAfter*1000);
                return Read(id, firstByte, lastByte);
            }
        }

        private Parameter GetDownloadUrl(string id)
        {
            try
            {
                _restClient.FollowRedirects = false;
                IRestRequest request = _requestHelper.Read(id);
                var restResponse = _restClient.Execute(request);
                var downloadUrl = restResponse.Headers.SingleOrDefault(h => h.Name.Equals("Location", StringComparison.InvariantCultureIgnoreCase));
                if (!restResponse.StatusCode.Equals(HttpStatusCode.Found) || downloadUrl == null)
                {
                    throw new Exception("Did not receive a redirect URL for the content request");
                }
                return downloadUrl;
            }
            finally
            {
                _restClient.FollowRedirects = true;
            }
        }


        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <returns>The raw file contents</returns>
        public byte[] Read(string id, string sharedLinkUrl)
        {
            GuardFromNull(id, "id");
            try
            {
                IRestRequest request = _requestHelper.Read(id);
                return _restClient.WithSharedLink(sharedLinkUrl).Execute(request).RawBytes;
            }
            catch (BoxDownloadNotReadyException e)
            {
                Thread.Sleep(e.RetryAfter*1000);
                return Read(id);
            }
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="file">The file to read</param>
        /// <param name="output">A stream to which the file contents will be written</param>
        public void Read(File file, Stream output)
        {
            GuardFromNull(file, "file");
            Read(file.Id, output);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="output">A stream to which the file contents will be written</param>
        public void Read(string id, Stream output)
        {
            GuardFromNull(id, "id");
            byte[] buffer = Read(id);
            output.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="output">A stream to which the file contents will be written</param>
        public void Read(string id, string sharedLinkUrl, Stream output)
        {
            GuardFromNull(id, "id");
            byte[] buffer = Read(id, sharedLinkUrl);
            output.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file contents</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to read</param>
        public void Read(Action<byte[]> onSuccess, Action<Error> onFailure, File file)
        {
            GuardFromNull(file, "file");
            Read(onSuccess, onFailure, file.Id);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file contents</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        public void Read(Action<byte[]> onSuccess, Action<Error> onFailure, string id)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            Action<Error> onFailureWrapper = error =>
                {
                    if (error.RetryAfter > 0)
                    {
                        Thread.Sleep(error.RetryAfter*1000);
                        Read(onSuccess, onFailure, id);
                    }
                    else
                    {
                        onFailure(error);
                    }
                };
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailureWrapper);
        }

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file contents</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        public void Read(Action<byte[]> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            Action<Error> onFailureWrapper = error =>
                {
                    if (error.RetryAfter > 0)
                    {
                        Thread.Sleep(error.RetryAfter*1000);
                        Read(onSuccess, onFailure, id, sharedLinkUrl);
                    }
                    else
                    {
                        onFailure(error);
                    }
                };
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccessWrapper, onFailureWrapper);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file stream</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to read</param>
        public void ReadToStream(Action<Stream> onSuccess, Action<Error> onFailure, File file)
        {
            GuardFromNull(file, "file");
            ReadToStream(onSuccess, onFailure, file.Id);
        }

        /// <summary>
        ///     Retrieve the contents of the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file stream</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        public void ReadToStream(Action<Stream> onSuccess, Action<Error> onFailure, string id)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response =>
                {
                    using (var stream = new MemoryStream(response.RawBytes))
                    {
                        onSuccess(stream);
                    }
                };
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        /// <summary>
        ///     Retrieve the contents of a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the file stream</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to read</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        public void ReadToStream(Action<Stream> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response =>
                {
                    using (var stream = new MemoryStream(response.RawBytes))
                    {
                        onSuccess(stream);
                    }
                };
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        public File Write(File file, Stream content, string etag = null)
        {
            return Write(file, ReadFully(content), etag);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="content">The file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        public File Write(File file, byte[] content, string etag = null)
        {
            GuardFromNull(file, "file");
            return Write(file.Id, file.Name, content, etag);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        public File Write(string id, string name, Stream content, string etag = null)
        {
            return Write(id, name, ReadFully(content), etag);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>An updated file object</returns>
        public File Write(string id, string name, byte[] content, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(name, "name");
            IRestRequest request = _requestHelper.Write(id, name, etag, content);
            return WriteFile(request);
        }

        private File WriteFile(IRestRequest request)
        {
            var itemCollection = _uploadClient.ExecuteAndDeserialize<ItemCollection>(request);
            return itemCollection.Entries.Single();
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="content">A stream containing the file's data</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, File file, Stream content, string etag = null)
        {
            GuardFromNull(file, "file");
            Write(onSuccess, onFailure, file, ReadFully(content), etag);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <param name="content">The file's data</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, File file, byte[] content, string etag = null)
        {
            GuardFromNull(file, "file");
            Write(onSuccess, onFailure, file.Id, file.Name, content, etag);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, string id, string name, Stream content, string etag = null)
        {
            GuardFromNull(content, "content");
            Write(onSuccess, onFailure, id, name, ReadFully(content), etag);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, string id, string name, byte[] content, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(content, "content");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Write(id, name, etag, content);
            _uploadClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="file">The file to copy</param>
        /// <param name="newParent">The destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        public File Copy(File file, Folder newParent, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(newParent, "folder");
            return CopyFile(file.Id, newParent.Id, newName, fields);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="file">The file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        public File Copy(File file, string newParentId, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(file, "file");
            return CopyFile(file.Id, newParentId, newName, fields);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        public File CopyFile(string id, string newParentId, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            IRestRequest request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Copies a shared file to the specified folder
        /// </summary>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        public File CopyFile(string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            IRestRequest request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            return _restClient.WithSharedLink(sharedLinkUrl).ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the copied file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to copy</param>
        /// <param name="newParent">The destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Copy(Action<File> onSuccess, Action<Error> onFailure, File file, Folder newParent, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(newParent, "folder");
            CopyFile(onSuccess, onFailure, file.Id, newParent.Id, newName, fields);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful File operation</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Copy(Action<File> onSuccess, Action<Error> onFailure, File file, string newParentId, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(file, "file");
            CopyFile(onSuccess, onFailure, file.Id, newParentId, newName, fields);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful File operation</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to copy</param>
        /// <param name="newParentId">The ID of the destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CopyFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newParentId, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

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
        public void CopyFile(Action<File> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, string newParentId, string newName = null, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="file">The file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>A file object populated with the shared link</returns>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public File ShareLink(File file, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            return ShareFileLink(file.Id, sharedLink, fields, etag);
        }

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="id">The ID of the file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>A file object populated with the shared link</returns>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public File ShareFileLink(string id, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

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
        public void ShareLink(Action<File> onSuccess, Action<Error> onFailure, File file, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            ShareFileLink(onSuccess, onFailure, file.Id, sharedLink, fields, etag);
        }

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
        public void ShareFileLink(Action<File> onSuccess, Action<Error> onFailure, string id, SharedLink sharedLink, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved file</returns>
        public File Move(File file, Folder newParent, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(newParent, "newParent");
            return Move(file, newParent.Id, fields, etag);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved file</returns>
        public File Move(File file, string newParentId, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            return MoveFile(file.Id, newParentId, fields, etag);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="id">The ID of the file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The moved file</returns>
        public File MoveFile(string id, string newParentId, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, newParentId);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Move(Action<File> onSuccess, Action<Error> onFailure, File file, Folder newParent, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(newParent, "newParent");
            Move(onSuccess, onFailure, file, newParent.Id, fields, etag);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Move(Action<File> onSuccess, Action<Error> onFailure, File file, string newParentId, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            MoveFile(onSuccess, onFailure, file.Id, newParentId, fields, etag);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void MoveFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newParentId, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="file">The file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed file</returns>
        public File Rename(File file, string newName, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            return RenameFile(file.Id, newName, fields, etag);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="id">The ID of the file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The renamed file</returns>
        public File RenameFile(string id, string newName, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, name: newName);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Rename(Action<File> onSuccess, Action<Error> onFailure, File file, string newName, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            RenameFile(onSuccess, onFailure, file.Id, newName, fields, etag);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void RenameFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newName, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated file</returns>
        public File UpdateDescription(File file, string description, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            return UpdateFileDescription(file.Id, description, fields, etag);
        }

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated file</returns>
        public File UpdateFileDescription(string id, string description, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, description: description);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void UpdateDescription(Action<File> onSuccess, Action<Error> onFailure, File file, string description, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            UpdateFileDescription(onSuccess, onFailure, file.Id, description, fields, etag);
        }

        /// <summary>
        ///     Updates a file's description
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="description">The new description for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void UpdateFileDescription(Action<File> onSuccess, Action<Error> onFailure, string id, string description, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            IRestRequest request = _requestHelper.Update(ResourceType.File, id, etag, fields, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Update one or more of a file's name, description, parent, or shared link.
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        /// <returns>The updated file</returns>
        public File Update(File file, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            IRestRequest request = _requestHelper.Update(ResourceType.File, file.Id, etag, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Update one or more of a file's name, description, parent, or shared link.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Update(Action<File> onSuccess, Action<Error> onFailure, File file, IEnumerable<FileField> fields = null, string etag = null)
        {
            GuardFromNull(file, "file");
            IRestRequest request = _requestHelper.Update(ResourceType.File, file.Id, etag, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="file">The file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Delete(File file, string etag = null)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, etag);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="id">The ID of the file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void DeleteFile(string id, string etag = null)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.DeleteFile(id, etag);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void Delete(Action onSuccess, Action<Error> onFailure, File file, string etag = null)
        {
            GuardFromNull(file, "file");
            DeleteFile(onSuccess, onFailure, file.Id, file.Etag);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to delete</param>
        /// <param name="etag">Include the item's etag to prevent the completion of this operation if you don't have the must current version of the item.  A BoxItemModifiedException will be thrown if the item is stale.</param>
        public void DeleteFile(Action onSuccess, Action<Error> onFailure, string id, string etag = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.DeleteFile(id, etag);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="file">The file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of metadata objects</returns>
        public VersionCollection GetVersions(File file, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(file, "file");
            return GetVersions(file.Id);
        }

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="fileId">The ID of the file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of metadata objects</returns>
        public VersionCollection GetVersions(string fileId, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(fileId, "fileId");
            IRestRequest request = _requestHelper.GetVersions(fileId, fields);
            return _restClient.ExecuteAndDeserialize<VersionCollection>(request);
        }

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved metadata</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetVersions(Action<VersionCollection> onSuccess, Action<Error> onFailure, File file, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(file, "file");
            GetVersions(onSuccess, onFailure, file.Id);
        }

        /// <summary>
        ///     Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved metadata</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="fileId">The ID of the file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetVersions(Action<VersionCollection> onSuccess, Action<Error> onFailure, string fileId, IEnumerable<FileField> fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.GetVersions(fileId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public byte[] GetThumbnail(File file, ThumbnailSize? minHeight = null, ThumbnailSize? minWidth = null, ThumbnailSize? maxHeight = null, ThumbnailSize? maxWidth = null, string extension = "png")
        {
            GuardFromNull(file, "file");
            return GetThumbnail(file.Id, minHeight, minWidth, maxHeight, maxWidth, extension);
        }

        private byte[] GetThumbnail(string fileId, ThumbnailSize? minHeight = null, ThumbnailSize? minWidth = null, ThumbnailSize? maxHeight = null, ThumbnailSize? maxWidth = null, string extension = "png")
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(extension, "extension");
            IRestRequest request = _requestHelper.GetThumbnail(fileId, minHeight, minWidth, maxHeight, maxWidth, extension);
            var restResponse = _restClient.Execute(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.Redirect))
            {
                return GetRedirectedThumbnail(restResponse.Headers.SingleOrDefault(h => h.Name.Equals("Location")));
            }
            return restResponse.RawBytes;
        }

        private byte[] GetRedirectedThumbnail(Parameter locationHeader)
        {
            if (locationHeader == null)
            {
                return new byte[0];
            }

            var redirectUri = new Uri((string) locationHeader.Value);
            var restClient = new RestClient(redirectUri.Host);
            var restRequest = new RestRequest(redirectUri.AbsolutePath);
            return restClient.Execute(restRequest).RawBytes;
        }

    }
}