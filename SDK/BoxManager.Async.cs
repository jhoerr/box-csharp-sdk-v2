using System;
using System.Net;
using BoxApi.V2.SDK.Model;
using RestSharp;
using Type = BoxApi.V2.SDK.Model.Type;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public void GetFolderAsync(string id, Action<Folder> onSuccess, Action onFailure)
        {
            var request = _requestHelper.Get(Type.Folder, id);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void GetFolderItemsAsync(string id, Action<ItemCollection> onSuccess, Action onFailure)
        {
            RestRequest folderItems = _requestHelper.GetItems(id);
            ExecuteAsync(folderItems, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CreateFolderAsync(string parentId, string name, Action<Folder> onSuccess, Action onFailure)
        {
            var request = _requestHelper.Create(Type.Folder, parentId, name);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void DeleteFolderAsync(string id, bool recursive, Action onSuccess, Action onFailure)
        {
            var request = _requestHelper.Delete(Type.Folder, id, recursive);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CopyFolderAsync(string folderId, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            RestRequest request = _requestHelper.Copy(Type.Folder, folderId, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void ShareFolderLinkAsync(string id, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            RestRequest request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void MoveFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure)
        {
            RestRequest request = _requestHelper.Move(Type.Folder, id, newParentId);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void RenameFolderAsync(string id, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            RestRequest request = _requestHelper.Rename(Type.Folder, id, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

    }
}