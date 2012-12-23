using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
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
        public File CreateFile(Folder parent, string name, Field[] fields = null)
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
        public File CreateFile(string parentId, string name, Field[] fields = null)
        {
            return CreateFile(parentId, name, new byte[0], fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="parent">The folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="content">The file's data</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The created file</returns>
        public File CreateFile(Folder parent, string name, byte[] content, Field[] fields = null)
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
        public File CreateFile(string parentId, string name, byte[] content, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFile(parentId, name, content, fields);
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
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, Field[] fields = null)
        {
            GuardFromNull(parent, "folder");
            CreateFile(onSuccess, onFailure, parent.Id, name, new byte[0], fields);
        }

        /// <summary>
        ///     Creates a new file with the provided content in the specified folder
        /// </summary>
        /// <param name="onSuccess">Action to perform with the created File</param>
        /// <param name="onFailure">Action to perform following a failed File creation</param>
        /// <param name="parentId">The ID of the folder in which to create the file</param>
        /// <param name="name">The file's name</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, Field[] fields = null)
        {
            CreateFile(onSuccess, onFailure, parentId, name, new byte[0], fields);
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
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, Folder parent, string name, byte[] content, Field[] fields = null)
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
        public void CreateFile(Action<File> onSuccess, Action<Error> onFailure, string parentId, string name, byte[] content, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFile(parentId, name, content, fields);

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
        /// <returns>The fetched file</returns>
        public File Get(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return GetFile(file.Id, fields);
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The fetched file</returns>
        public File GetFile(string id, Field[] fields = null)
        {
            return GetFile(id, 0, restRequest => _restClient.ExecuteAndDeserialize<File>(restRequest), fields);
        }

        /// <summary>
        ///     Retrieves a shared file
        /// </summary>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The shared file</returns>
        public File GetFile(string id, string sharedLinkUrl, Field[] fields = null)
        {
            return GetFile(id, 0, _restClient.WithSharedLink(sharedLinkUrl).ExecuteAndDeserialize<File>, fields);
        }

        private File GetFile(string id, int attempt, Func<IRestRequest, File> getFileOperation, Field[] fields = null)
        {
            var getFile = getFileOperation;
            // Exponential backoff to give Etag time to populate.  Wait 200ms, then 400ms, then 800ms.
            if (attempt > 0)
            {
                Backoff(attempt);
            }
            var request = _requestHelper.Get(ResourceType.File, id, fields);
            var file = getFile(request);
            return string.IsNullOrEmpty(file.Etag) && (attempt < MaxFileGetAttempts) ? GetFile(id, ++attempt, _restClient.ExecuteAndDeserialize<File>, fields) : file;
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Get(Action<File> onSuccess, Action<Error> onFailure, File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            GetFile(onSuccess, onFailure, file.Id, fields);
        }

        /// <summary>
        ///     Retrieves a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, Field[] fields = null)
        {
            GetFile(onSuccess, onFailure, id, 0, _restClient.ExecuteAsync, fields);
        }

        /// <summary>
        ///     Retrieves a shared file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved File</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to get</param>
        /// <param name="sharedLinkUrl">The shared link for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, Field[] fields = null)
        {
            GetFile(onSuccess, onFailure, id, 0, _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync, fields);
        }

        private void GetFile(Action<File> onSuccess, Action<Error> onFailure, string id, int attempt, Action<IRestRequest, Action<File>, Action<Error>> getFileAsync, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);

            var request = _requestHelper.Get(ResourceType.File, id, fields);

            Action<File> onSuccessWrapper = file =>
                {
                    if (String.IsNullOrEmpty(file.Etag) && attempt++ < MaxFileGetAttempts)
                    {
                        // Exponential backoff to give Etag time to populate.  Wait 100ms, then 200ms, then 400ms, then 800ms.
                        Backoff(attempt);
                        GetFile(onSuccess, onFailure, id, attempt, getFileAsync, fields);
                    }
                    else
                    {
                        onSuccess(file);
                    }
                };

            getFileAsync(request, onSuccessWrapper, onFailure);
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
            var request = _requestHelper.Read(id);
            return _restClient.Execute(request).RawBytes;
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
            var request = _requestHelper.Read(id);
            return _restClient.WithSharedLink(sharedLinkUrl).Execute(request).RawBytes;
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
            var buffer = Read(id);
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
            var buffer = Read(id, sharedLinkUrl);
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
            var request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
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
            var request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccessWrapper, onFailure);
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
            var request = _requestHelper.Read(id);
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
            var request = _requestHelper.Read(id);
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
        /// <returns>An updated file object</returns>
        public File Write(File file, Stream content)
        {
            return Write(file, ReadFully(content));
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="content">The file's data</param>
        /// <returns>An updated file object</returns>
        public File Write(File file, byte[] content)
        {
            GuardFromNull(file, "file");
            return Write(file.Id, file.Name, file.Etag, content);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="etag">The file's eTag.  This must match the value on the server or the write will fail.</param>
        /// <param name="content">A stream containing the file's data</param>
        /// <returns>An updated file object</returns>
        public File Write(string id, string name, string etag, Stream content)
        {
            return Write(id, name, etag, ReadFully(content));
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="etag">The file's eTag.  This must match the value on the server or the write will fail.</param>
        /// <param name="content">The file's data</param>
        /// <returns>An updated file object</returns>
        public File Write(string id, string name, string etag, byte[] content)
        {
            GuardFromNull(id, "id");
            GuardFromNull(name, "name");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.Write(id, name, etag, content);
            return WriteFile(request);
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public File WriteFile(IRestRequest request)
        {
            var itemCollection = _restClient.ExecuteAndDeserialize<ItemCollection>(request);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            return GetFile(itemCollection.Entries.Single().Id);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="content">A stream containing the file's data</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, File file, Stream content)
        {
            GuardFromNull(file, "file");
            Write(onSuccess, onFailure, file, ReadFully(content));
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="content">The file's data</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, File file, byte[] content)
        {
            GuardFromNull(file, "file");
            Write(onSuccess, onFailure, file.Id, file.Name, file.Etag, content);
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="etag">The file's eTag.  This must match the value on the server or the write will fail.</param>
        /// <param name="content">A stream containing the file's data</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, string id, string name, string etag, Stream content)
        {
            GuardFromNull(content, "content");
            Write(onSuccess, onFailure, id, name, etag, ReadFully(content));
        }

        /// <summary>
        ///     Updates the content of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the successfully written file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to update</param>
        /// <param name="name">The file's name</param>
        /// <param name="etag">The file's eTag.  This must match the value on the server or the write will fail.</param>
        /// <param name="content">The file's data</param>
        public void Write(Action<File> onSuccess, Action<Error> onFailure, string id, string name, string etag, byte[] content)
        {
            GuardFromNull(id, "id");
            GuardFromNull(content, "content");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Write(id, name, etag, content);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Copies a file to the specified folder
        /// </summary>
        /// <param name="file">The file to copy</param>
        /// <param name="newParent">The destination folder for the copied file</param>
        /// <param name="newName">The optional new name for the copied file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The copied file</returns>
        public File Copy(File file, Folder newParent, string newName = null, Field[] fields = null)
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
        public File Copy(File file, string newParentId, string newName = null, Field[] fields = null)
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
        public File CopyFile(string id, string newParentId, string newName = null, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
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
        public File CopyFile(string id, string sharedLinkUrl, string newParentId, string newName = null, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
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
        public void Copy(Action<File> onSuccess, Action<Error> onFailure, File file, Folder newParent, string newName = null, Field[] fields = null)
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
        public void Copy(Action<File> onSuccess, Action<Error> onFailure, File file, string newParentId, string newName = null, Field[] fields = null)
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
        public void CopyFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newParentId, string newName = null, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
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
        public void CopyFile(Action<File> onSuccess, Action<Error> onFailure, string id, string sharedLinkUrl, string newParentId, string newName = null, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            _restClient.WithSharedLink(sharedLinkUrl).ExecuteAsync(request, onSuccess, onFailure);
        }


        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="file">The file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A file object populated with the shared link</returns>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public File ShareLink(File file, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return ShareFileLink(file.Id, sharedLink, fields);
        }

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="id">The ID of the file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A file object populated with the shared link</returns>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public File ShareFileLink(string id, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(ResourceType.File, id, fields, sharedLink: sharedLink);
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
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public void ShareLink(Action<File> onSuccess, Action<Error> onFailure, File file, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            ShareFileLink(onSuccess, onFailure, file.Id, sharedLink, fields);
        }

        /// <summary>
        ///     Creates a shared link to the specified file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the linked file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file for which to create a shared link</param>
        /// <param name="sharedLink">The properties of the shared link</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <remarks>In order for File.SharedLink to be populated, you must either include Field.SharedLink in the fields list, or leave the list null</remarks>
        public void ShareFileLink(Action<File> onSuccess, Action<Error> onFailure, string id, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="newParent">The destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The moved file</returns>
        public File Move(File file, Folder newParent, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            return Move(file, newParent.Id, fields);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The moved file</returns>
        public File Move(File file, string newParentId, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return MoveFile(file.Id, newParentId, fields);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="id">The ID of the file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The moved file</returns>
        public File MoveFile(string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.File, id, fields, newParentId);
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
        public void Move(Action<File> onSuccess, Action<Error> onFailure, File file, Folder newParent, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            Move(onSuccess, onFailure, file, newParent.Id, fields);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Move(Action<File> onSuccess, Action<Error> onFailure, File file, string newParentId, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            MoveFile(onSuccess, onFailure, file.Id, newParentId, fields);
        }

        /// <summary>
        ///     Moves a file to the specified destination
        /// </summary>
        /// <param name="onSuccess">Action to perform with the moved file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to move</param>
        /// <param name="newParentId">The ID of the destination folder</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void MoveFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="file">The file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The renamed file</returns>
        public File Rename(File file, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return RenameFile(file.Id, newName, fields);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="id">The ID of the file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The renamed file</returns>
        public File RenameFile(string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.File, id, fields, name: newName);
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
        public void Rename(Action<File> onSuccess, Action<Error> onFailure, File file, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            RenameFile(onSuccess, onFailure, file.Id, newName, fields);
        }

        /// <summary>
        ///     Renames a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the renamed file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="id">The ID of the file to rename</param>
        /// <param name="newName">The new name for the file</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void RenameFile(Action<File> onSuccess, Action<Error> onFailure, string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Update one or more of a file's name, description, parent, or shared link.
        /// </summary>
        /// <param name="file">The file to update</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>The updated file</returns>
        public File Update(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        /// <summary>
        ///     Update one or more of a file's name, description, parent, or shared link.
        /// </summary>
        /// <param name="onSuccess">Action to perform with the update file</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to update</param>
        /// <param name="fields">The properties that should be set on the returned File object.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void Update(Action<File> onSuccess, Action<Error> onFailure, File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="file">The file to delete</param>
        public void Delete(File file)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, file.Etag);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="id">The ID of the file to delete</param>
        /// <param name="etag">The eTag of the file to delete.  This must match the value on the server</param>
        public void DeleteFile(string id, string etag)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.Execute(request);
        }

        /// <summary>
        ///     Deletes a file from the user's Box
        /// </summary>
        /// <param name="onSuccess">Action to perform following a successful delete</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file to delete</param>
        public void Delete(Action onSuccess, Action<Error> onFailure, File file)
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
        /// <param name="etag">The eTag of the file to delete.  This must match the value on the server</param>
        public void DeleteFile(Action onSuccess, Action<Error> onFailure, string id, string etag)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        /// <summary>
        /// Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="file">The file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of metadata objects</returns>
        public VersionCollection GetVersions(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return GetVersions(file.Id);
        }

        /// <summary>
        /// Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="fileId">The ID of the file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        /// <returns>A collection of metadata objects</returns>
        public VersionCollection GetVersions(string fileId, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            IRestRequest request = _requestHelper.GetVersions(fileId, fields);
            return _restClient.ExecuteAndDeserialize<VersionCollection>(request);
        }

        /// <summary>
        /// Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved metadata</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="file">The file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetVersions(Action<VersionCollection> onSuccess, Action<Error> onFailure, File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            GetVersions(onSuccess, onFailure, file.Id);
        }

        /// <summary>
        /// Retrieves metadata about older versions of a file
        /// </summary>
        /// <param name="onSuccess">Action to perform with the retrieved metadata</param>
        /// <param name="onFailure">Action to perform following a failed File operation</param>
        /// <param name="fileId">The ID of the file for which to retrieve metadata</param>
        /// <param name="fields">The properties that should be set on the returned VersionCollection.Entries.  Type and Id are always set.  If left null, all properties will be set, which can increase response time.</param>
        public void GetVersions(Action<VersionCollection> onSuccess, Action<Error> onFailure, string fileId, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.GetVersions(fileId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}