using System;
using System.Collections.Generic;
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
            GuardFromNullOrEmpty(id, "id");
            var request = _requestHelper.Get(Type.Folder, id);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public ItemCollection GetFolderItems(string id)
        {
            GuardFromNullOrEmpty(id, "id");
            var request = _requestHelper.GetItems(id);
            return Execute<ItemCollection>(request, HttpStatusCode.OK);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            GuardFromNullOrEmpty(parentId, "parentFolderId");
            GuardFromNullOrEmpty(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public void Delete(Folder folder, bool recursive)
        {
            GuardFromNullOrEmpty(folder, "folder");
            DeleteFolder(folder.Id, recursive);
        }

        public void DeleteFolder(string id, bool recursive)
        {
            GuardFromNullOrEmpty(id, "id");
            var request = _requestHelper.DeleteFolder(id, recursive);
            Execute(request, HttpStatusCode.OK);
        }

        public Folder CopyFolder(Folder folder, string newParentId, string newName = null)
        {
            GuardFromNullOrEmpty(folder, "folder");
            return CopyFolder(folder.Id, newParentId, newName);
        }

        public Folder CopyFolder(string id, string newParentId, string newName = null)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(newParentId, "newParentId");
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public Folder ShareFolderLink(string id, SharedLink sharedLink)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(sharedLink, "sharedLink");
            var request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder MoveFolder(string id, string newParentId)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(newParentId, "newParentId");
            var request = _requestHelper.Move(Type.Folder, id, newParentId);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder RenameFolder(string id, string newName)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(newName, "newName");
            var request = _requestHelper.Rename(Type.Folder, id, newName);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public File GetFile(string id)
        {
            GuardFromNullOrEmpty(id, "id");
            var request = _requestHelper.Get(Type.File, id);
            return Execute<File>(request, HttpStatusCode.OK);
        }

        public File CreateFile(string parentId, string name)
        {
            return CreateFile(parentId, name, new byte[0]);
        }

        public File CreateFile(string parentId, string name, byte[] content)
        {
            GuardFromNullOrEmpty(parentId, "parentFolderId");
            GuardFromNullOrEmpty(name, "name");
            var request = _requestHelper.CreateFile(parentId, name, content);
            var itemCollection = Execute<ItemCollection>(request, HttpStatusCode.OK);
            
            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.
            
            Thread.Sleep(500);
            return GetFile(itemCollection.Entries.Single().Id); 
        }

        public void Delete(File file)
        {
            GuardFromNullOrEmpty(file, "file");
            DeleteFile(file.Id, file.Etag);
        }

        public void DeleteFile(string id, string etag)
        {
            GuardFromNullOrEmpty(id, "id");
            GuardFromNullOrEmpty(etag, "etag");
            var request = _requestHelper.DeleteFile(id, etag);
            Execute(request, HttpStatusCode.OK);
        }

        public byte[] ReadFile(string id)
        {
            GuardFromNullOrEmpty(id, "id");
            var request = _requestHelper.ReadFile(id);
            return Execute(request, HttpStatusCode.OK).RawBytes;
        }
    }
}