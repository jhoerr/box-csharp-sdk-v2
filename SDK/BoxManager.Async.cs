using System;
using System.Net;
using BoxApi.V2.SDK.Model;
using RestSharp;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public void GetFolderAsync(string id, Action<Folder> onSuccess, Action onFailure)
        {
            var request = _requestHelper.GetFolder(id);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void GetFolderItemsAsync(string id, Action<ItemCollection> onSuccess, Action onFailure)
        {
            RestRequest folderItems = _requestHelper.GetFolderItems(id);
            ExecuteAsync(folderItems, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CreateFolderAsync(string parentId, string name, Action<Folder> onSuccess, Action onFailure)
        {
            var request = _requestHelper.CreateFolder(parentId, name);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void DeleteFolderAsync(string id, bool recursive, Action onSuccess, Action onFailure)
        {
            var request = _requestHelper.DeleteFolder(id, recursive);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void CopyFolderAsync(string folderId, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            RestRequest request = _requestHelper.CopyFolder(folderId, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void ShareFolderLinkAsync(string id, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            RestRequest request = _requestHelper.ShareFolderLink(id, sharedLink);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void MoveFolderAsync(string id, string newParentId, Action<Folder> onSuccess, Action onFailure)
        {
            RestRequest request = _requestHelper.MoveFolder(id, newParentId);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public void RenameFolderAsync(string id, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            RestRequest request = _requestHelper.RenameFolder(id, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.OK);
        }

        private void ExecuteAsync<T>(RestRequest request, Action<T> onSuccess, Action onFailure, HttpStatusCode expectedStatusCode) where T : class, new()
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("onSuccess can not be null");
            }

            _restContentClient.ExecuteAsync<T>(request, response =>
                {
                    if (WasSuccessful(response, expectedStatusCode))
                    {
                        onSuccess(response.Data);
                    }
                    else if (onFailure != null)
                    {
                        onFailure();
                    }
                });
        }

        private void ExecuteAsync(RestRequest request, Action onSuccess, Action onFailure, HttpStatusCode expectedStatusCode)
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("callback can not be null");
            }

            _restContentClient.ExecuteAsync(request, response =>
                {
                    if (WasSuccessful(response, expectedStatusCode))
                    {
                        onSuccess();
                    }
                    else if (onFailure != null)
                    {
                        onFailure();
                    }
                });
        }
    }
}