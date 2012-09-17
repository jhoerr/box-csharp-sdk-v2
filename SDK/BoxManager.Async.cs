using System;
using System.Linq;
using System.Threading;
using BoxApi.V2.SDK.Model;
using RestSharp;
using Type = BoxApi.V2.SDK.Model.Type;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public void GetFolderAsync(string id, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(Type.Folder, id);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetFolderItemsAsync(string id, Action<ItemCollection> onSuccess, Action onFailure)
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

        public void DeleteFolderAsync(string id, bool recursive, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFolder(id, recursive);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CopyFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareFolderLinkAsync(string id, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
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

        public void RenameFolderAsync(string id, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Rename(Type.Folder, id, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetFileAsync(string id, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(Type.File, id);
            ExecuteAsync(request, onSuccess, onFailure);
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

            Action<ItemCollection> onSuccessWrapper = items =>
                {

                    Thread.Sleep(300);
                    GetFileAsync(items.Entries.Single().Id, onSuccess, onFailure);
                };
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
            IRestRequest request = _requestHelper.Read(id);
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
            IRestRequest request = _requestHelper.Write(id, name, content);
            ExecuteAsync(request, onSuccess, onFailure);
        }

        private static void GuardFromNullCallbacks(object onSuccess, object onFailure)
        {
            GuardFromNull(onSuccess, "onSuccess");
            GuardFromNull(onFailure, "onFailure");
        }

        public void CopyAsync(File file, Folder folder, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(folder, "folder");
            CopyFileAsync(file.Id, folder.Id, onSuccess, onFailure, newName);
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
            IRestRequest request = _requestHelper.Copy(Type.File, id, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}