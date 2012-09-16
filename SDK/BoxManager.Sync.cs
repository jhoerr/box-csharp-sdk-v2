using System;
using System.Linq;
using System.Net;
using System.Threading;
using BoxApi.V2.SDK.Model;
using Type = BoxApi.V2.SDK.Model.Type;

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

        public Folder CreateFolder(string parentFolderId, string name)
        {
            var request = _requestHelper.CreateFolder(parentFolderId, name);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public void Delete(Folder folder, bool recursive)
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

        public Folder CopyFolder(string id, string newParentId, string newName = null)
        {
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public Folder ShareFolderLink(string id, SharedLink sharedLink)
        {
            var request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder MoveFolder(string id, string newParentId)
        {
            var request = _requestHelper.Move(Type.Folder, id, newParentId);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder RenameFolder(string id, string newName)
        {
            var request = _requestHelper.Rename(Type.Folder, id, newName);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public File GetFile(string id)
        {
            var request = _requestHelper.Get(Type.File, id);
            return Execute<File>(request, HttpStatusCode.OK);
        }

        public File CreateFile(string parentFolderId, string name)
        {
            GuardFromNull(parentFolderId, "parentFolderId");
            GuardFromNull(name, "name");

            var request = _requestHelper.CreateFile(parentFolderId, name, new byte[0]);
            var itemCollection = Execute<ItemCollection>(request, HttpStatusCode.OK);
            
            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.
            
            Thread.Sleep(300);
            return GetFile(itemCollection.Entries.Single().Id); 
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
            Execute(request, HttpStatusCode.OK);
        }
    }
}