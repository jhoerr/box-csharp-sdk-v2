using System;
using System.IO;
using System.Linq;
using BoxApi.V2.Model;
using RestSharp;
using File = BoxApi.V2.Model.File;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public void Get(Folder folder, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetFolder(folder.Id, null, onSuccess, onFailure);
        }

        public void GetFolder(string id, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Folder, id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetItems(Folder folder, Field[] fields, Action<ItemCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetItems(folder.Id, fields, onSuccess, onFailure);
        }

        public void GetItems(string id, Field[] fields, Action<ItemCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(fields, "fields");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var folderItems = _requestHelper.GetItems(id, fields);
            _restClient.ExecuteAsync(folderItems, onSuccess, onFailure);
        }

        public void CreateFolder(string parentId, string name, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFolder(parentId, name, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Folder folder, bool recursive, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(folder.Id, recursive, onSuccess, onFailure);
        }

        public void DeleteFolder(string id, bool recursive, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFolder(id, recursive);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Copy(Folder folder, Folder newParent, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(newParent, "newParent");
            Copy(folder, newParent.Id, newName, fields, onSuccess, onFailure);
        }

        public void Copy(Folder folder, string newParentId, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            CopyFolder(folder.Id, newParentId, newName, fields, onSuccess, onFailure);
        }

        public void CopyFolder(string id, string newParentId, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareLink(Folder folder, SharedLink sharedLink, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            ShareFolderLink(folder.Id, sharedLink, fields, onSuccess, onFailure);
        }

        public void ShareLink(File file, SharedLink sharedLink, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            ShareFileLink(file.Id, sharedLink, fields, onSuccess, onFailure);
        }

        public void ShareFolderLink(string id, SharedLink sharedLink, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareFileLink(string id, SharedLink sharedLink, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Move(Folder folder, Folder newParent, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(newParent, "newParent");
            Move(folder, newParent.Id, fields, onSuccess, onFailure);
        }

        public void Move(Folder folder, string newParentId, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            MoveFolder(folder.Id, newParentId, fields, onSuccess, onFailure);
        }

        public void MoveFolder(string id, string newParentId, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Move(File file, Folder newParent, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(newParent, "newParent");
            Move(file, newParent.Id, fields, onSuccess, onFailure);
        }

        public void Move(File file, string newParentId, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            MoveFile(file.Id, newParentId, fields, onSuccess, onFailure);
        }

        public void MoveFile(string id, string newParentId, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Rename(Folder folder, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            RenameFolder(folder.Id, newName, fields, onSuccess, onFailure);
        }

        public void Rename(File file, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            RenameFile(file.Id, newName, fields, onSuccess, onFailure);
        }

        public void RenameFolder(string id, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void RenameFile(string id, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.File, id, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void UpdateDescription(Folder folder, string description, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            UpdateFolderDescription(folder.Id, description, fields, onSuccess, onFailure);
        }

        private void UpdateFolderDescription(string id, string description, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void UpdateDescription(File file, string description, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            UpdateFileDescription(file.Id, description, fields, onSuccess, onFailure);
        }

        public void UpdateFileDescription(string id, string description, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.File, id, fields, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Folder folder, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, fields, parentId, folder.Name, folder.Description, folder.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(File file, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(ResourceType.File, file.Id, fields, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Comment comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(ResourceType.Comment, comment.Id, fields, message: comment.Message);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Get(File file, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            GetFile(file.Id, onSuccess, onFailure);
        }

        public void GetFile(string id, Action<File> onSuccess, Action<Error> onFailure)
        {
            GetFile(id, 0, onSuccess, onFailure);
        }

        private void GetFile(string id, int attempt, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);

            var request = _requestHelper.Get(ResourceType.File, id);

            Action<File> onSuccessWrapper = file =>
                {
                    if (String.IsNullOrEmpty(file.Etag) && attempt++ < MaxFileGetAttempts)
                    {
                        // Exponential backoff to give Etag time to populate.  Wait 100ms, then 200ms, then 400ms, then 800ms.
                        Backoff(attempt);
                        GetFile(id, attempt, onSuccess, onFailure);
                    }
                    else
                    {
                        onSuccess(file);
                    }
                };

            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void Delete(File file, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, file.Etag, onSuccess, onFailure);
        }

        public void DeleteFile(string id, string etag, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Comment comment, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(comment.Id, onSuccess, onFailure);
        }

        public void DeleteComment(string id, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteComment(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CreateFile(string parentId, string name, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFile(parentId, name, new byte[0]);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            Action<ItemCollection> onSuccessWrapper = items => GetFile(items.Entries.Single().Id, onSuccess, onFailure);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void Read(File file, Action<byte[]> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            Read(file.Id, onSuccess, onFailure);
        }

        public void Read(string id, Action<byte[]> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void Write(File file, Stream content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            Write(file, ReadFully(content), onSuccess, onFailure);
        }

        public void Write(File file, byte[] content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            Write(file.Id, file.Name, file.Etag, content, onSuccess, onFailure);
        }

        public void Write(string id, string name, string etag, Stream content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(content, "content");
            Write(id, name, etag, ReadFully(content), onSuccess, onFailure);
        }

        public void Write(string id, string name, string etag, byte[] content, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(content, "content");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Write(id, name, etag, content);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private static void GuardFromNullCallbacks(object onSuccess, object onFailure)
        {
            GuardFromNull(onSuccess, "onSuccess");
            GuardFromNull(onFailure, "onFailure");
        }

        public void Copy(File file, Folder newParent, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            GuardFromNull(newParent, "folder");
            CopyFile(file.Id, newParent.Id, newName, fields, onSuccess, onFailure);
        }

        public void Copy(File file, string newParentId, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            CopyFile(file.Id, newParentId, newName, fields, onSuccess, onFailure);
        }

        public void CopyFile(string id, string newParentId, string newName, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.File, id, newParentId, newName, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CreateComment(File file, string comment, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            CreateFileComment(file.Id, comment, onSuccess, onFailure);
        }

        private void CreateFileComment(string fileId, string comment, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComment(Comment comment, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            GetComment(comment.Id, onSuccess, onFailure);
        }

        private void GetComment(string commentId, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(commentId, "commentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.Get(ResourceType.Comment, commentId);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComments(File file, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            GetFileComments(file.Id, onSuccess, onFailure);
        }

        private void GetFileComments(string fileId, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void CreateDiscussion(Folder parent, string name, string description, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parent, "parent");
            CreateDiscussion(parent.Id, name, description, onSuccess, onFailure);
        }

        public void CreateDiscussion(string parentId, string name, string description, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.CreateDiscussion(parentId, name, description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetComments(Discussion discussion, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            GetDiscussionComments(discussion.Id);
        }

        public void GetDiscussionComments(string discussionId, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void CreateComment(Discussion discussion, string comment, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            CreateDiscussionComment(discussion.Id, comment, onSuccess, onFailure);
        }

        public void CreateDiscussionComment(string discussionId, string comment, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void Delete(Discussion discussion, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            DeleteDiscussion(discussion.Id, onSuccess, onFailure);
        }

        public void DeleteDiscussion(string id, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.DeleteDiscussion(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetDiscussion(string id, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Get(ResourceType.Discussion, id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetDiscussions(Folder folder, Action<DiscussionCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetDiscussions(folder.Id, onSuccess, onFailure);
        }

        private void GetDiscussions(string folderId, Action<DiscussionCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.GetDiscussions(folderId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CreateCollaboration(Folder folder, string userId, Role role, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            CreateCollaboration(folder.Id, userId, role, onSuccess, onFailure);
        }

        public void CreateCollaboration(string folderId, string userId, Role role, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description());
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private void GetCollaboration(string collaborationId, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaboration(collaborationId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetCollaborations(Folder folder, Action<CollaborationCollection> onSuccess, Action<Error> onFailure, bool onlyPending = false)
        {
            GuardFromNull(folder, "folder");
            GetCollaborations(folder.Id, onSuccess, onFailure, onlyPending);
        }

        private void GetCollaborations(string folderId, Action<CollaborationCollection> onSuccess, Action<Error> onFailure, bool onlyPending = false)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaborations(folderId, onlyPending);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Collaboration collaboration, Role role, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(collaboration.Id, role, onSuccess, onFailure);
        }

        public void Update(Collaboration collaboration, Role role, Status status, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(collaboration.Id, role, status, onSuccess, onFailure);
        }

        private void UpdateCollaboration(string collaborationId, Role role, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private void UpdateCollaboration(string collaborationId, Role role, Status status, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Collaboration collaboration, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            DeleteCollaboration(collaboration.Id, onSuccess, onFailure);
        }

        private void DeleteCollaboration(string collaborationId, Action<IRestResponse> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.DeleteCollaboration(collaborationId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}