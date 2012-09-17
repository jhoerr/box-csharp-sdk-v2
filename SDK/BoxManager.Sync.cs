using System;
using System.Collections.Generic;
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
        public Folder GetFolder(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(Type.Folder, id);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public ItemCollection GetFolderItems(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetItems(id);
            return Execute<ItemCollection>(request, HttpStatusCode.OK);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public void Delete(Folder folder, bool recursive)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(folder.Id, recursive);
        }

        public void DeleteFolder(string id, bool recursive)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteFolder(id, recursive);
            Execute(request, HttpStatusCode.OK);
        }

        public Folder CopyFolder(Folder folder, string newParentId, string newName = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParentId, newName);
        }

        public Folder CopyFolder(string id, string newParentId, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public Folder ShareFolderLink(string id, SharedLink sharedLink)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.ShareLink(Type.Folder, id, sharedLink);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder MoveFolder(string id, string newParentId)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Move(Type.Folder, id, newParentId);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public Folder RenameFolder(string id, string newName)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Rename(Type.Folder, id, newName);
            return Execute<Folder>(request, HttpStatusCode.OK);
        }

        public File GetFile(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(Type.File, id);
            return Execute<File>(request, HttpStatusCode.OK);
        }

        public File CreateFile(string parentId, string name)
        {
            return CreateFile(parentId, name, new byte[0]);
        }

        public File CreateFile(string parentId, string name, byte[] content)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFile(parentId, name, content);
            return WriteFile(request);
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

        public byte[] Read(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Read(id);
            return Execute(request, HttpStatusCode.OK).RawBytes;
        }

        public File Write(File file, byte[] content)
        {
            return Write(file.Id, file.Name, content);
        }

        public File Write(string id, string name, byte[] content)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Write(id, name, content);
            return WriteFile(request);
        }

        private File WriteFile(IRestRequest request)
        {
            var itemCollection = Execute<ItemCollection>(request, HttpStatusCode.OK);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            Thread.Sleep(500);
            return GetFile(itemCollection.Entries.Single().Id);
        }
    }
}