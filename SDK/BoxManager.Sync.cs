using System.Net;
using BoxApi.V2.SDK.Model;
using RestSharp;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public Folder GetFolder(string id)
        {
            var request = _requestHelper.Get(Type.Folder, id);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public ItemCollection GetFolderItems(string id)
        {
            var request = _requestHelper.GetItems(id);
            return Execute<ItemCollection>(request, HttpStatusCode.OK);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            var request = _requestHelper.Create(Type.Folder, parentId, name);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public void DeleteFolder(Folder folder, bool recursive)
        {
            DeleteFolder(folder.Id, recursive);
        }

        public void DeleteFolder(string id, bool recursive)
        {
            var request = _requestHelper.Delete(Type.Folder, id, recursive);
            Execute(request, HttpStatusCode.OK);
        }

        public Folder CopyFolder(Folder folder, string newParentId, string newName = null)
        {
            return CopyFolder(folder.Id, newParentId, newName);
        }

        public Folder CopyFolder(string folderId, string newParentId, string newName = null)
        {
            RestRequest request = _requestHelper.Copy(Type.Folder, folderId, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public Folder ShareFolderLink(string folderId, SharedLink sharedLink)
        {
            RestRequest request = _requestHelper.ShareLink(Type.Folder, folderId, sharedLink);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder MoveFolder(string folderId, string newParentId)
        {
            RestRequest request = _requestHelper.Move(Type.Folder, folderId, newParentId);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder RenameFolder(string folderId, string newName)
        {
            RestRequest request = _requestHelper.Rename(Type.Folder, folderId, newName);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }
    }
}