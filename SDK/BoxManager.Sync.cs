using System.Net;
using BoxApi.V2.SDK.Model;
using RestSharp;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public Folder GetFolder(string id)
        {
            var request = _requestHelper.GetFolder(id);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public ItemCollection GetFolderItems(string id)
        {
            var request = _requestHelper.GetFolderItems(id);
            return Execute<ItemCollection>(request, HttpStatusCode.OK);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            var request = _requestHelper.CreateFolder(parentId, name);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public void DeleteFolder(Folder folder, bool recursive)
        {
            DeleteFolder(folder.Id, recursive);
        }

        public void DeleteFolder(string id, bool recursive)
        {
            var request = _requestHelper.DeleteFolder(id, recursive);
            Execute(request, HttpStatusCode.OK);
        }

        public Folder CopyFolder(Folder folder, string newParentId, string newName = null)
        {
            return CopyFolder(folder.Id, newParentId, newName);
        }

        public Folder CopyFolder(string folderId, string newParentId, string newName = null)
        {
            RestRequest request = _requestHelper.CopyFolder(folderId, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public Folder ShareFolderLink(string folderId, SharedLink sharedLink)
        {
            RestRequest request = _requestHelper.ShareFolderLink(folderId, sharedLink);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder MoveFolder(string folderId, string newParentId)
        {
            RestRequest request = _requestHelper.MoveFolder(folderId, newParentId);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder RenameFolder(string folderId, string newName)
        {
            RestRequest request = _requestHelper.RenameFolder(folderId, newName);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }
        
        private void Execute(RestRequest request, HttpStatusCode expectedStatusCode)
        {
            var restResponse = _restContentClient.Execute(request);
            if (!WasSuccessful(restResponse, expectedStatusCode))
            {
                throw new BoxException(restResponse);
            }
        }

        private T Execute<T>(IRestRequest request, HttpStatusCode expectedStatusCode) where T : class, new()
        {
            var restResponse = _restContentClient.Execute<T>(request);
            if (!WasSuccessful(restResponse, expectedStatusCode))
            {
                throw new BoxException(restResponse);
            }
            return restResponse.Data;
        }

    }
}