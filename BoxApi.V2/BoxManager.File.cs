using System;
using System.IO;
using System.Linq;
using BoxApi.V2.Model;
using RestSharp;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public File CreateFile(Folder parent, string name, Field[] fields = null)
        {
            return CreateFile(parent, name, new byte[0], fields);
        }

        public File CreateFile(string parentId, string name, Field[] fields = null)
        {
            return CreateFile(parentId, name, new byte[0], fields);
        }

        public File CreateFile(Folder parent, string name, byte[] content, Field[] fields = null)
        {
            GuardFromNull(parent, "folder");
            return CreateFile(parent.Id, name, content, fields);
        }

        public File CreateFile(string parentId, string name, byte[] content, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFile(parentId, name, content, fields);
            return WriteFile(request);
        }

        public File Get(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return GetFile(file.Id, fields);
        }

        public File GetFile(string id, Field[] fields = null)
        {
            return GetFile(id, 0, fields);
        }

        private File GetFile(string id, int attempt, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            // Exponential backoff to give Etag time to populate.  Wait 200ms, then 400ms, then 800ms.
            if (attempt > 0)
            {
                Backoff(attempt);
            }
            var request = _requestHelper.Get(ResourceType.File, id, fields);
            var file = _restClient.ExecuteAndDeserialize<File>(request);
            return string.IsNullOrEmpty(file.Etag) && (attempt < MaxFileGetAttempts) ? GetFile(id, ++attempt, fields) : file;
        }

        public void Get(File file, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            GetFile(file.Id, fields, onSuccess, onFailure);
        }

        public void GetFile(string id, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GetFile(id, 0, fields, onSuccess, onFailure);
        }

        private void GetFile(string id, int attempt, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
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
                        GetFile(id, attempt, fields, onSuccess, onFailure);
                    }
                    else
                    {
                        onSuccess(file);
                    }
                };

            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public byte[] Read(File file)
        {
            GuardFromNull(file, "file");
            return Read(file.Id);
        }

        public byte[] Read(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Read(id);
            return _restClient.Execute(request).RawBytes;
        }

        public void Read(File file, Stream output)
        {
            GuardFromNull(file, "file");
            Read(file.Id, output);
        }

        public void Read(string id, Stream output)
        {
            GuardFromNull(id, "id");
            var buffer = Read(id);
            output.Write(buffer, 0, buffer.Length);
        }

        public void Read(File file, Action<byte[]> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            Read(file.Id, onSuccess, onFailure);
        }

        public void Read(string id, Action<byte[]> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public File Write(File file, Stream content)
        {
            return Write(file, ReadFully(content));
        }

        public File Write(File file, byte[] content)
        {
            GuardFromNull(file, "file");
            return Write(file.Id, file.Name, file.Etag, content);
        }

        public File Write(string id, string name, string etag, Stream content)
        {
            return Write(id, name, etag, ReadFully(content));
        }

        public File Write(string id, string name, string etag, byte[] content)
        {
            GuardFromNull(id, "id");
            GuardFromNull(name, "name");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.Write(id, name, etag, content);
            return WriteFile(request);
        }

        private File WriteFile(IRestRequest request)
        {
            var itemCollection = _restClient.ExecuteAndDeserialize<ItemCollection>(request);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            return GetFile(itemCollection.Entries.Single().Id);
        }

        public void Write(File file, Stream content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            Write(file, ReadFully(content), onSuccess, onFailure);
        }

        public void Write(File file, byte[] content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            Write(file.Id, file.Name, file.Etag, content, onSuccess, onFailure);
        }

        public void Write(string id, string name, string etag, Stream content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(content, "content");
            Write(id, name, etag, ReadFully(content), onSuccess, onFailure);
        }

        public void Write(string id, string name, string etag, byte[] content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(content, "content");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Write(id, name, etag, content);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public File Copy(File file, Folder folder, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(folder, "folder");
            return CopyFile(file.Id, folder.Id, newName, fields);
        }

        public File Copy(File file, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return CopyFile(file.Id, newParentId, newName, fields);
        }

        public File CopyFile(string id, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public File ShareLink(File file, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return ShareFileLink(file.Id, sharedLink, fields);
        }

        private File ShareFileLink(string id, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(ResourceType.File, id, fields, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public void ShareLink(File file, SharedLink sharedLink, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            ShareFileLink(file.Id, sharedLink, fields, onSuccess, onFailure);
        }

        public void ShareFileLink(string id, SharedLink sharedLink, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public File Move(File file, Folder newParent, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            return Move(file, newParent.Id, fields);
        }

        public File Move(File file, string newParentId, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return MoveFile(file.Id, newParentId, fields);
        }

        public File MoveFile(string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.File, id, fields, newParentId);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public void Move(File file, Folder newParent, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(newParent, "newParent");
            Move(file, newParent.Id, fields, onSuccess, onFailure);
        }

        public void Move(File file, string newParentId, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            MoveFile(file.Id, newParentId, fields, onSuccess, onFailure);
        }

        public void MoveFile(string id, string newParentId, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public File Rename(File file, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return RenameFile(file.Id, newName, fields);
        }

        public File RenameFile(string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.File, id, fields, name: newName);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public void Rename(File file, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            RenameFile(file.Id, newName, fields, onSuccess, onFailure);
        }

        public void RenameFile(string id, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public File UpdateDescription(File file, string description, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return UpdateFileDescription(file.Id, description, fields);
        }

        public File UpdateFileDescription(string id, string description, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.File, id, fields, description: description);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public File Update(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public void UpdateDescription(File file, string description, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            UpdateFileDescription(file.Id, description, fields, onSuccess, onFailure);
        }

        public void UpdateFileDescription(string id, string description, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.File, id, fields, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(File file, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(File file)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, file.Etag);
        }

        public void DeleteFile(string id, string etag)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.Execute(request);
        }

        public void Delete(File file, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, file.Etag, onSuccess, onFailure);
        }

        public void DeleteFile(string id, string etag, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}