using System;
using System.IO;
using System.Linq;
using System.Threading;
using BoxApi.V2.SDK.Model;
using RestSharp;
using File = BoxApi.V2.SDK.Model.File;
using Type = BoxApi.V2.SDK.Model.Type;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        private const int MaxFileGetAttempts = 4;

        public Folder Get(Folder folder)
        {
            GuardFromNull(folder, "folder");
            return GetFolder(folder.Id);
        }

        public Folder GetFolder(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(Type.Folder, id);
            return Execute<Folder>(request);
        }

        public ItemCollection GetItems(Folder folder)
        {
            GuardFromNull(folder, "folder");
            return GetItems(folder.Id);
        }

        public ItemCollection GetItems(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetItems(id);
            return Execute<ItemCollection>(request);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name);
            return Execute<Folder>(request);
        }

        public void Delete(Folder folder)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(folder.Id, true);
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
            Execute(request);
        }

        public Folder Copy(Folder folder, Folder newParent, string newName = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParent.Id, newName);
        }

        public Folder Copy(Folder folder, string newParentId, string newName = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParentId, newName);
        }

        public File Copy(File file, Folder folder, string newName = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(folder, "folder");
            return CopyFile(file.Id, folder.Id, newName);
        }

        public File Copy(File file, string newParentId, string newName = null)
        {
            GuardFromNull(file, "file");
            return CopyFile(file.Id, newParentId, newName);
        }

        public Folder CopyFolder(string id, string newParentId, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            return Execute<Folder>(request);
        }

        public File CopyFile(string id, string newParentId, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(Type.File, id, newParentId, newName);
            return Execute<File>(request);
        }

        public Folder ShareLink(Folder folder, SharedLink sharedLink)
        {
            GuardFromNull(folder, "folder");
            return ShareFolderLink(folder.Id, sharedLink);
        }

        public Folder ShareFolderLink(string id, SharedLink sharedLink)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(Type.Folder, id, sharedLink: sharedLink);
            return Execute<Folder>(request);
        }

        public File ShareLink(File file, SharedLink sharedLink)
        {
            GuardFromNull(file, "file");
            return ShareFileLink(file.Id, sharedLink);
        }

        private File ShareFileLink(string id, SharedLink sharedLink)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(Type.File, id, sharedLink: sharedLink);
            return Execute<File>(request);
        }

        public Folder Move(Folder folder, Folder newParent)
        {
            GuardFromNull(newParent, "newParent");
            return Move(folder, newParent.Id);
        }

        public Folder Move(Folder folder, string newParentId)
        {
            GuardFromNull(folder, "folder");
            return MoveFolder(folder.Id, newParentId);
        }

        public File Move(File file, Folder newParent)
        {
            GuardFromNull(newParent, "newParent");
            return Move(file, newParent.Id);
        }

        public File Move(File file, string newParentId)
        {
            GuardFromNull(file, "file");
            return MoveFile(file.Id, newParentId);
        }

        public Folder MoveFolder(string id, string newParentId)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(Type.Folder, id, newParentId);
            return Execute<Folder>(request);
        }

        public File MoveFile(string id, string newParentId)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(Type.File, id, newParentId);
            return Execute<File>(request);
        }

        public Folder Rename(Folder folder, string newName)
        {
            GuardFromNull(folder, "folder");
            return RenameFolder(folder.Id, newName);
        }

        public File Rename(File file, string newName)
        {
            GuardFromNull(file, "file");
            return RenameFile(file.Id, newName);
        }

        public File RenameFile(string id, string newName)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(Type.File, id, name: newName);
            return Execute<File>(request);
        }

        public Folder RenameFolder(string id, string newName)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(Type.Folder, id, name: newName);
            return Execute<Folder>(request);
        }

        public Folder UpdateDescription(Folder folder, string description)
        {
            GuardFromNull(folder, "folder");
            return UpdateFolderDescription(folder.Id, description);
        }

        public Folder UpdateFolderDescription(string id, string description)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(Type.Folder, id, description: description);
            return Execute<Folder>(request);
        }

        public File UpdateDescription(File file, string description)
        {
            GuardFromNull(file, "file");
            return UpdateFileDescription(file.Id, description);
        }

        public File UpdateFileDescription(string id, string description)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(Type.File, id, description: description);
            return Execute<File>(request);
        }

        public File Update(File file)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(Type.File, file.Id, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            return Execute<File>(request);
        }

        public Folder Update(Folder folder)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(Type.Folder, folder.Id, parentId, folder.Name, folder.Description, folder.SharedLink);
            return Execute<Folder>(request);
        }

        public Comment Update(Comment comment)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(Type.Comment, comment.Id, message:comment.Message);
            return Execute<Comment>(request);
        }

        public File Get(File file)
        {
            GuardFromNull(file, "file");
            return GetFile(file.Id);
        }

        public File GetFile(string id)
        {
            return GetFile(id, 0);
        }

        private File GetFile(string id, int attempt)
        {
            GuardFromNull(id, "id");
            // Exponential backoff to give Etag time to populate.  Wait 200ms, then 400ms, then 800ms.
            if (attempt > 0)
            {
                Backoff(attempt);
            }
            var request = _requestHelper.Get(Type.File, id);
            var file = Execute<File>(request);
            return string.IsNullOrEmpty(file.Etag) && (attempt < MaxFileGetAttempts) ? GetFile(id, attempt++) : file;
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
            Execute(request);
        }

        public void Delete(Comment comment)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(comment.Id);
        }

        private void DeleteComment(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteComment(id);
            Execute(request);
        }

        public byte[] Read(File file)
        {
            GuardFromNull(file, "file");
            return Read(file.Id);
        }

        public byte[] Read(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Read(id);
            return Execute(request).RawBytes;
        }

        public void Read(File file, Stream output)
        {
            GuardFromNull(file, "file");
            Read(file.Id, output);
        }

        public void Read(string id, Stream output)
        {
            GuardFromNull(id, "id");
            var buffer = Read(id);
            output.Write(buffer, 0, buffer.Length);
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
            var itemCollection = Execute<ItemCollection>(request);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            return GetFile(itemCollection.Entries.Single().Id);
        }

        public Comment AddComment (File file, string comment)
        {
            GuardFromNull(file, "file");
            return AddComment(file.Id, comment);
        }

        public Comment AddComment(string fileId, string comment)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            IRestRequest restRequest = _requestHelper.AddComment(fileId, comment);
            return Execute<Comment>(restRequest);
        }

        public CommentCollection GetComments(File file)
        {
            GuardFromNull(file, "file");
            return GetFileComments(file.Id);
        }

        public CommentCollection GetFileComments(string fileId)
        {
            GuardFromNull(fileId, "fileId");
            IRestRequest restRequest = _requestHelper.GetComments(Type.File, fileId);
            return Execute<CommentCollection>(restRequest);
        }

        public Comment GetComment(Comment comment)
        {
            GuardFromNull(comment, "comment");
            return GetComment(comment.Id);
        }

        private Comment GetComment(string id)
        {
            GuardFromNull(id, "id");
            IRestRequest restRequest = _requestHelper.Get(Type.Comment, id);
            return Execute<Comment>(restRequest);
        }
    }
}