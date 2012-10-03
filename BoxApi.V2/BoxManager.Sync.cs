using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using BoxApi.V2.Model;
using RestSharp;

namespace BoxApi.V2
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
            var request = _requestHelper.Get(ResourceType.Folder, id);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public ItemCollection GetItems(Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetItems(folder.Id, fields);
        }

        public ItemCollection GetItems(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.GetItems(id, fields);
            return _restClient.ExecuteAndDeserialize<ItemCollection>(request);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
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
            _restClient.Execute(request);
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

        public Model.File Copy(Model.File file, Folder folder, string newName = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(folder, "folder");
            return CopyFile(file.Id, folder.Id, newName);
        }

        public Model.File Copy(Model.File file, string newParentId, string newName = null)
        {
            GuardFromNull(file, "file");
            return CopyFile(file.Id, newParentId, newName);
        }

        public Folder CopyFolder(string id, string newParentId, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Model.File CopyFile(string id, string newParentId, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName);
            return _restClient.ExecuteAndDeserialize<Model.File>(request);
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
            var request = _requestHelper.Update(ResourceType.Folder, id, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Model.File ShareLink(Model.File file, SharedLink sharedLink)
        {
            GuardFromNull(file, "file");
            return ShareFileLink(file.Id, sharedLink);
        }

        private Model.File ShareFileLink(string id, SharedLink sharedLink)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(ResourceType.File, id, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<Model.File>(request);
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

        public Model.File Move(Model.File file, Folder newParent)
        {
            GuardFromNull(newParent, "newParent");
            return Move(file, newParent.Id);
        }

        public Model.File Move(Model.File file, string newParentId)
        {
            GuardFromNull(file, "file");
            return MoveFile(file.Id, newParentId);
        }

        public Folder MoveFolder(string id, string newParentId)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.Folder, id, newParentId);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Model.File MoveFile(string id, string newParentId)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.File, id, newParentId);
            return _restClient.ExecuteAndDeserialize<Model.File>(request);
        }

        public Folder Rename(Folder folder, string newName)
        {
            GuardFromNull(folder, "folder");
            return RenameFolder(folder.Id, newName);
        }

        public Model.File Rename(Model.File file, string newName)
        {
            GuardFromNull(file, "file");
            return RenameFile(file.Id, newName);
        }

        public Model.File RenameFile(string id, string newName)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.File, id, name: newName);
            return _restClient.ExecuteAndDeserialize<Model.File>(request);
        }

        public Folder RenameFolder(string id, string newName)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.Folder, id, name: newName);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
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
            var request = _requestHelper.Update(ResourceType.Folder, id, description: description);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Model.File UpdateDescription(Model.File file, string description)
        {
            GuardFromNull(file, "file");
            return UpdateFileDescription(file.Id, description);
        }

        public Model.File UpdateFileDescription(string id, string description)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.File, id, description: description);
            return _restClient.ExecuteAndDeserialize<Model.File>(request);
        }

        public Model.File Update(Model.File file)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            return _restClient.ExecuteAndDeserialize<Model.File>(request);
        }

        public Folder Update(Folder folder)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, parentId, folder.Name, folder.Description, folder.SharedLink);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Comment Update(Comment comment)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(ResourceType.Comment, comment.Id, message:comment.Message);
            return _restClient.ExecuteAndDeserialize<Comment>(request);
        }

        public Model.File Get(Model.File file)
        {
            GuardFromNull(file, "file");
            return GetFile(file.Id);
        }

        public Model.File GetFile(string id)
        {
            return GetFile(id, 0);
        }

        private Model.File GetFile(string id, int attempt)
        {
            GuardFromNull(id, "id");
            // Exponential backoff to give Etag time to populate.  Wait 200ms, then 400ms, then 800ms.
            if (attempt > 0)
            {
                Backoff(attempt);
            }
            var request = _requestHelper.Get(ResourceType.File, id);
            var file = _restClient.ExecuteAndDeserialize<Model.File>(request);
            return string.IsNullOrEmpty(file.Etag) && (attempt < MaxFileGetAttempts) ? GetFile(id, attempt++) : file;
        }

        public Model.File CreateFile(string parentId, string name)
        {
            return CreateFile(parentId, name, new byte[0]);
        }

        public Model.File CreateFile(string parentId, string name, byte[] content)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFile(parentId, name, content);
            return WriteFile(request);
        }

        public void Delete(Model.File file)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, file.Etag);
        }

        public void DeleteFile(string id, string etag)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.Execute(request);
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
            _restClient.Execute(request);
        }

        public byte[] Read(Model.File file)
        {
            GuardFromNull(file, "file");
            return Read(file.Id);
        }

        public byte[] Read(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Read(id);
            return _restClient.Execute(request).RawBytes;
        }

        public void Read(Model.File file, Stream output)
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

        public Model.File Write(Model.File file, Stream content)
        {
            return Write(file, ReadFully(content));
        }

        public Model.File Write(Model.File file, byte[] content)
        {
            GuardFromNull(file, "file");
            return Write(file.Id, file.Name, file.Etag, content);
        }

        public Model.File Write(string id, string name, string etag, Stream content)
        {
            return Write(id, name, etag, ReadFully(content));
        }

        public Model.File Write(string id, string name, string etag, byte[] content)
        {
            GuardFromNull(id, "id");
            GuardFromNull(name, "name");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.Write(id, name, etag, content);
            return WriteFile(request);
        }

        private Model.File WriteFile(IRestRequest request)
        {
            var itemCollection = _restClient.ExecuteAndDeserialize<ItemCollection>(request);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            return GetFile(itemCollection.Entries.Single().Id);
        }

        public Comment CreateComment (Model.File file, string comment)
        {
            GuardFromNull(file, "file");
            return CreateFileComment(file.Id, comment);
        }

        public Comment CreateFileComment(string fileId, string comment)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            IRestRequest restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public CommentCollection GetComments(Discussion discussion)
        {
            GuardFromNull(discussion, "discussion");
            return GetDiscussionComments(discussion.Id);
        }

        public CommentCollection GetDiscussionComments(string discussionId)
        {
            GuardFromNull(discussionId, "discussionId");
            IRestRequest restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        public Comment CreateComment(Discussion discussion, string comment)
        {
            GuardFromNull(discussion, "discussion");
            return CreateDiscussionComment(discussion.Id, comment);
        }

        public Comment CreateDiscussionComment(string discussionId, string comment)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            IRestRequest restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public CommentCollection GetComments(Model.File file)
        {
            GuardFromNull(file, "file");
            return GetFileComments(file.Id);
        }

        public CommentCollection GetFileComments(string fileId)
        {
            GuardFromNull(fileId, "fileId");
            IRestRequest restRequest = _requestHelper.GetComments(ResourceType.File, fileId);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        public Comment GetComment(Comment comment)
        {
            GuardFromNull(comment, "comment");
            return GetComment(comment.Id);
        }

        private Comment GetComment(string id)
        {
            GuardFromNull(id, "id");
            IRestRequest restRequest = _requestHelper.Get(ResourceType.Comment, id);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public Discussion CreateDiscussion(Folder parent, string name, string description = null)
        {
            GuardFromNull(parent, "parent");
            return CreateDiscussion(parent.Id, name, description);
        }

        public Discussion CreateDiscussion(string parentId, string name, string description = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            IRestRequest request = _requestHelper.CreateDiscussion(parentId, name, description);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        public void Delete (Discussion discussion)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(discussion.Id);
        }

        public void DeleteDiscussion(string id)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.DeleteDiscussion(id);
            _restClient.Execute(request);
        }

        public Discussion GetDiscussion(string id)
        {
            GuardFromNull(id, "id");
            IRestRequest request = _requestHelper.Get(ResourceType.Discussion, id);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        public DiscussionCollection GetDiscussions(Folder folder)
        {
            GuardFromNull(folder, "folder");
            return GetDiscussions(folder.Id);
        }

        private DiscussionCollection GetDiscussions(string folderId)
        {
            GuardFromNull(folderId, "folderId");
            IRestRequest request = _requestHelper.GetDiscussions(folderId);
            return _restClient.ExecuteAndDeserialize<DiscussionCollection>(request);
        }
    }
}