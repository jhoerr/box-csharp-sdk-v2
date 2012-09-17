using System;
using System.Linq;
using BoxApi.V2.SDK.Model;
using RestSharp;
using Type = BoxApi.V2.SDK.Model.Type;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public void GetAsync(Folder folder, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            GetFolderAsync(folder.Id, onSuccess, onFailure);
        }

        public void GetFolderAsync(string id, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(Type.Folder, id);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetItemsAsync(string id, Action<ItemCollection> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var folderItems = _requestHelper.GetItems(id);
            ExecuteAsync(folderItems, onSuccess, onFailure);
        }

        public void CreateFolderAsync(string parentId, string name, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFolder(parentId, name);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void DeleteAsync(Folder folder, bool recursive, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            DeleteFolderAsync(folder.Id, recursive, onSuccess, onFailure);
        }

        public void DeleteFolderAsync(string id, bool recursive, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFolder(id, recursive);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CopyAsync(Folder folder, Folder newParent, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(newParent, "newParent");
            CopyAsync(folder, newParent.Id, onSuccess, onFailure, newName);
        }

        public void CopyAsync(Folder folder, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(folder, "folder");
            CopyFolderAsync(folder.Id, newParentId, onSuccess, onFailure, newName);
        }

        public void CopyFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareLinkAsync(Folder folder, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            ShareFolderLinkAsync(folder.Id, sharedLink, onSuccess, onFailure);
        }

        public void ShareLinkAsync(File file, SharedLink sharedLink, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            ShareFileLinkAsync(file.Id, sharedLink, onSuccess, onFailure);
        }

        public void ShareFolderLinkAsync(string id, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareFileLinkAsync(string id, SharedLink sharedLink, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.ShareLink(Type.File, id, sharedLink);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void MoveFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Move(Type.Folder, id, newParentId);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void RenameAsync(Folder folder, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            RenameFolderAsync(folder.Id, newName, onSuccess, onFailure);
        }

        public void RenameAsync(File file, string newName, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            RenameFileAsync(file.Id, newName, onSuccess, onFailure);
        }

        public void RenameFolderAsync(string id, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Rename(Type.Folder, id, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void RenameFileAsync(string id, string newName, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Rename(Type.File, id, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetAsync(File file, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            GetFileAsync(file.Id, onSuccess, onFailure);
        }

        public void GetFileAsync(string id, Action<File> onSuccess, Action onFailure)
        {
            GetFileAsync(id, 0, onSuccess, onFailure);
        }

        private void GetFileAsync(string id, int attempt, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);

            var request = _requestHelper.Get(Type.File, id);

            Action<File> onSuccessWrapper = file =>
                {
                    if (String.IsNullOrEmpty(file.Etag) && attempt++ < MaxFileGetAttempts)
                    {
                        // Exponential backoff to give Etag time to populate.  Wait 100ms, then 200ms, then 400ms, then 800ms.
                        Backoff(attempt);
                        GetFileAsync(id, attempt, onSuccess, onFailure);
                    }
                    else
                    {
                        onSuccess(file);
                    }
                };

            ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void DeleteAsync(File file, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            DeleteFileAsync(file.Id, file.Etag, onSuccess, onFailure);
        }

        public void DeleteFileAsync(string id, string etag, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFile(id, etag);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CreateFileAsync(string parentId, string name, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFile(parentId, name, new byte[0]);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            Action<ItemCollection> onSuccessWrapper = items => GetFileAsync(items.Entries.Single().Id, onSuccess, onFailure);
            ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void ReadAsync(File file, Action<byte[]> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            ReadAsync(file.Id, onSuccess, onFailure);
        }

        public void ReadAsync(string id, Action<byte[]> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void WriteAsync(File file, byte[] content, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            WriteAsync(file.Id, file.Name, content, onSuccess, onFailure);
        }

        public void WriteAsync(string id, string name, byte[] content, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(content, "content");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Write(id, name, content);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        private static void GuardFromNullCallbacks(object onSuccess, object onFailure)
        {
            GuardFromNull(onSuccess, "onSuccess");
            GuardFromNull(onFailure, "onFailure");
        }

        public void CopyAsync(File file, Folder newParent, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(newParent, "folder");
            CopyFileAsync(file.Id, newParent.Id, onSuccess, onFailure, newName);
        }

        public void CopyAsync(File file, string newParentId, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(file, "file");
            CopyFileAsync(file.Id, newParentId, onSuccess, onFailure, newName);
        }

        public void CopyFileAsync(string id, string newParentId, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(Type.File, id, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}