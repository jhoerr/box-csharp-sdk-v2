using System;
using System.Linq;
using System.Net;
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
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.Get(Type.Folder, id);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void GetFolderItemsAsync(string id, Action<ItemCollection> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var folderItems = _requestHelper.GetItems(id);
            ExecuteAsync(folderItems, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CreateFolderAsync(string parentId, string name, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(parentId, "parentId");
            GuardFromNullOrEmpty(name, "name");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.CreateFolder(parentId, name);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void DeleteFolderAsync(string id, bool recursive, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.DeleteFolder(id, recursive);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CopyFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(newParentId, "newParentId");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void ShareFolderLinkAsync(string id, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(sharedLink, "sharedLink");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void MoveFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(newParentId, "newParentId");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.Move(Type.Folder, id, newParentId);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void RenameFolderAsync(string id, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(newName, "newName");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.Rename(Type.Folder, id, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void GetFileAsync(string id, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.Get(Type.File, id);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void DeleteFileAsync(string id, string etag, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(etag, "etag");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            var request = _requestHelper.DeleteFile(id, etag);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CreateFileAsync(string parentId, string name, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(parentId, "parentId");
            GuardFromNullOrEmpty(name, "name");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
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
            ExecuteAsync(request, onSuccessWrapper, onFailure, HttpStatusCode.OK);
        }

        public void ReadFileAsync(string id, Action<byte[]> onSuccess, Action onFailure)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(onSuccess, "onSuccess");
            GuardFromNullOrEmpty(onFailure, "onFailure");
            IRestRequest request = _requestHelper.ReadFile(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            ExecuteAsync(request, onSuccessWrapper, onFailure, HttpStatusCode.OK);
        }
    }
}