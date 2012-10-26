using System.IO;
using System.Linq;
using BoxApi.V2.Model;
using RestSharp;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        private const int MaxFileGetAttempts = 4;

        public Folder Get(Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetFolder(folder.Id, null);
        }

        public Folder GetFolder(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Folder, id, fields);
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

        public Folder CreateFolder(string parentId, string name, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name, fields);
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

        public Folder Copy(Folder folder, Folder newParent, string newName, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParent.Id, newName, fields);
        }

        public Folder Copy(Folder folder, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return CopyFolder(folder.Id, newParentId, newName, fields);
        }

        public File Copy(File file, Folder folder, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(folder, "folder");
            return CopyFile(file.Id, folder.Id, newName, fields);
        }

        public File Copy(File file, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return CopyFile(file.Id, newParentId, newName, fields);
        }

        public Folder CopyFolder(string id, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public File CopyFile(string id, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public Folder ShareLink(Folder folder, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return ShareFolderLink(folder.Id, sharedLink, fields);
        }

        public Folder ShareFolderLink(string id, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public File ShareLink(File file, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return ShareFileLink(file.Id, sharedLink, fields);
        }

        private File ShareFileLink(string id, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            var request = _requestHelper.Update(ResourceType.File, id, fields, sharedLink: sharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public Folder Move(Folder folder, Folder newParent, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            return Move(folder, newParent.Id, fields);
        }

        public Folder Move(Folder folder, string newParentId, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return MoveFolder(folder.Id, newParentId, fields);
        }

        public File Move(File file, Folder newParent, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            return Move(file, newParent.Id, fields);
        }

        public File Move(File file, string newParentId, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return MoveFile(file.Id, newParentId, fields);
        }

        public Folder MoveFolder(string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, newParentId);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public File MoveFile(string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.File, id, fields, newParentId);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public Folder Rename(Folder folder, string newName, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return RenameFolder(folder.Id, newName, fields);
        }

        public File Rename(File file, string newName, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return RenameFile(file.Id, newName, fields);
        }

        public File RenameFile(string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.File, id, fields, name: newName);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public Folder RenameFolder(string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.Folder, id,fields, name: newName);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Folder UpdateDescription(Folder folder, string description, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return UpdateFolderDescription(folder.Id, description, fields);
        }

        public Folder UpdateFolderDescription(string id, string description, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, description: description);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public File UpdateDescription(File file, string description, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return UpdateFileDescription(file.Id, description, fields);
        }

        public File UpdateFileDescription(string id, string description, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.File, id, fields, description: description);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public File Update(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            return _restClient.ExecuteAndDeserialize<File>(request);
        }

        public Folder Update(Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, fields, parentId, folder.Name, folder.Description, folder.SharedLink);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public Comment Update(Comment comment, Field[] fields = null)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(ResourceType.Comment, comment.Id, fields, message: comment.Message);
            return _restClient.ExecuteAndDeserialize<Comment>(request);
        }

        public File Get(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return GetFile(file.Id, fields);
        }

        public File GetFile(string id, Field[] fields = null)
        {
            return GetFile(id, 0, fields);
        }

        private File GetFile(string id, int attempt, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            // Exponential backoff to give Etag time to populate.  Wait 200ms, then 400ms, then 800ms.
            if (attempt > 0)
            {
                Backoff(attempt);
            }
            var request = _requestHelper.Get(ResourceType.File, id, fields);
            var file = _restClient.ExecuteAndDeserialize<File>(request);
            return string.IsNullOrEmpty(file.Etag) && (attempt < MaxFileGetAttempts) ? GetFile(id, ++attempt, fields) : file;
        }

        public File CreateFile(Folder parent, string name, Field[] fields = null)
        {
            return CreateFile(parent, name, new byte[0], fields);
        }

        public File CreateFile(string parentId, string name, Field[] fields = null)
        {
            return CreateFile(parentId, name, new byte[0], fields);
        }

        public File CreateFile(Folder parent, string name, byte[] content, Field[] fields = null)
        {
            GuardFromNull(parent, "folder");
            return CreateFile(parent.Id, name, content, fields);
        }

        public File CreateFile(string parentId, string name, byte[] content, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentFolderId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFile(parentId, name, content, fields);
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

        public byte[] Read(File file)
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

        public File Write(File file, Stream content)
        {
            return Write(file, ReadFully(content));
        }

        public File Write(File file, byte[] content)
        {
            GuardFromNull(file, "file");
            return Write(file.Id, file.Name, file.Etag, content);
        }

        public File Write(string id, string name, string etag, Stream content)
        {
            return Write(id, name, etag, ReadFully(content));
        }

        public File Write(string id, string name, string etag, byte[] content)
        {
            GuardFromNull(id, "id");
            GuardFromNull(name, "name");
            GuardFromNull(etag, "etag");
            var request = _requestHelper.Write(id, name, etag, content);
            return WriteFile(request);
        }

        private File WriteFile(IRestRequest request)
        {
            var itemCollection = _restClient.ExecuteAndDeserialize<ItemCollection>(request);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            return GetFile(itemCollection.Entries.Single().Id);
        }

        public Comment CreateComment(File file, string comment)
        {
            GuardFromNull(file, "file");
            return CreateFileComment(file.Id, comment);
        }

        public Comment CreateFileComment(string fileId, string comment)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment);
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
            var restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId);
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
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public CommentCollection GetComments(File file)
        {
            GuardFromNull(file, "file");
            return GetFileComments(file.Id);
        }

        public CommentCollection GetFileComments(string fileId)
        {
            GuardFromNull(fileId, "fileId");
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId);
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
            var restRequest = _requestHelper.Get(ResourceType.Comment, id);
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
            var request = _requestHelper.CreateDiscussion(parentId, name, description);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        public void Delete(Discussion discussion)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(discussion.Id);
        }

        public void DeleteDiscussion(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.DeleteDiscussion(id);
            _restClient.Execute(request);
        }

        public Discussion GetDiscussion(string id)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Discussion, id);
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
            var request = _requestHelper.GetDiscussions(folderId);
            return _restClient.ExecuteAndDeserialize<DiscussionCollection>(request);
        }

        public Collaboration CreateCollaboration(Folder folder, string userId, Role role)
        {
            GuardFromNull(folder, "folder");
            return CreateCollaboration(folder.Id, userId, role);
        }

        public Collaboration CreateCollaboration(string folderId, string userId, Role role)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description());
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public Collaboration Get(Collaboration collaboration)
        {
            GuardFromNull(collaboration, "collaboration");
            return GetCollaboration(collaboration.Id);
        }

        private Collaboration GetCollaboration(string collaborationId)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.GetCollaboration(collaborationId);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public CollaborationCollection GetCollaborations(Folder folder, bool onlyPending = false)
        {
            GuardFromNull(folder, "folder");
            return GetCollaborations(folder.Id, onlyPending);
        }

        private CollaborationCollection GetCollaborations(string folderId, bool onlyPending = false)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetCollaborations(folderId, onlyPending);
            return _restClient.ExecuteAndDeserialize<CollaborationCollection>(request); ;
        }

        public Collaboration Update(Collaboration collaboration, Role role)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role);
        }

        public Collaboration Update(Collaboration collaboration, Role role, Status status)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, status);
        }

        private Collaboration UpdateCollaboration(string collaborationId, Role role)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        private Collaboration UpdateCollaboration(string collaborationId, Role role, Status status)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public void Delete(Collaboration collaboration)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(collaboration.Id);
        }

        private void DeleteCollaboration(string collaborationId)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.Execute(request);
        }
    }
}