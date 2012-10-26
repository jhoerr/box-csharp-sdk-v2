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

        public void ShareFolderLink(string id, SharedLink sharedLink, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, sharedLink: sharedLink);
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

        public void Rename(Folder folder, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            RenameFolder(folder.Id, newName, fields, onSuccess, onFailure);
        }

        public void RenameFolder(string id, string newName, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, name: newName);
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

        public void Update(Folder folder, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, fields, parentId, folder.Name, folder.Description, folder.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Comment comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(ResourceType.Comment, comment.Id, fields, message: comment.Message);
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

        public void CreateFile(Folder folder, string name, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            CreateFile(folder.Id, name, new byte[0], fields, onSuccess, onFailure);
        }

        public void CreateFile(Folder folder, string name, byte[] content, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            CreateFile(folder.Id, name, content, fields, onSuccess, onFailure);
        }

        public void CreateFile(string parentId, string name, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            CreateFile(parentId, name, new byte[0], fields, onSuccess, onFailure);
        }

        public void CreateFile(string parentId, string name, byte[] content, Field[] fields, Action<File> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFile(parentId, name, content, fields);

            // TODO: There are two side effects to to deal with here:
            // 1. Box requires some non-trivial amount of time to calculate the file's etag.
            // 2. This calculation is not performed before the 'upload file' request returns.
            // see also: http://stackoverflow.com/questions/12205183/why-is-etag-null-from-the-returned-file-object-when-uploading-a-file
            // As a result we must wait a bit and then re-fetch the file from the server.

            Action<ItemCollection> onSuccessWrapper = items => GetFile(items.Entries.Single().Id, fields, onSuccess, onFailure);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
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

        public void CreateComment(File file, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            CreateFileComment(file.Id, comment, fields, onSuccess, onFailure);
        }

        private void CreateFileComment(string fileId, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComment(Comment comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(comment, "comment");
            GetComment(comment.Id, fields, onSuccess, onFailure);
        }

        private void GetComment(string commentId, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(commentId, "commentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.Get(ResourceType.Comment, commentId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComments(File file, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(file, "file");
            GetFileComments(file.Id, fields, onSuccess, onFailure);
        }

        private void GetFileComments(string fileId, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void CreateDiscussion(Folder parent, string name, string description, Field[] fields, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parent, "parent");
            CreateDiscussion(parent.Id, name, description, fields, onSuccess, onFailure);
        }

        public void CreateDiscussion(string parentId, string name, string description, Field[] fields, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.CreateDiscussion(parentId, name, description, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetComments(Discussion discussion, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            GetDiscussionComments(discussion.Id, fields, onSuccess, onFailure);
        }

        public void GetDiscussionComments(string discussionId, Field[] fields, Action<CommentCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId, fields);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void CreateComment(Discussion discussion, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussion, "discussion");
            CreateDiscussionComment(discussion.Id, comment, fields, onSuccess, onFailure);
        }

        public void CreateDiscussionComment(string discussionId, string comment, Field[] fields, Action<Comment> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment, fields);
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

        public void GetDiscussion(string id, Field[] fields, Action<Discussion> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.Get(ResourceType.Discussion, id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetDiscussions(Folder folder, Field[] fields, Action<DiscussionCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetDiscussions(folder.Id, fields, onSuccess, onFailure);
        }

        private void GetDiscussions(string folderId, Field[] fields, Action<DiscussionCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            IRestRequest request = _requestHelper.GetDiscussions(folderId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CreateCollaboration(Folder folder, string userId, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            CreateCollaboration(folder.Id, userId, role, fields, onSuccess, onFailure);
        }

        public void CreateCollaboration(string folderId, string userId, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description(), fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private void GetCollaboration(string collaborationId, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaboration(collaborationId, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetCollaborations(Folder folder, bool pendingCollaborationsOnly, Field[] fields, Action<CollaborationCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetCollaborations(folder.Id, pendingCollaborationsOnly, fields, onSuccess, onFailure);
        }

        private void GetCollaborations(string folderId, bool pendingCollaborationsOnly, Field[] fields, Action<CollaborationCollection> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.GetCollaborations(folderId, pendingCollaborationsOnly, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Collaboration collaboration, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(collaboration.Id, role, fields, onSuccess, onFailure);
        }

        public void Update(Collaboration collaboration, Role role, Status status, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaboration, "collaboration");
            UpdateCollaboration(collaboration.Id, role, status, fields, onSuccess, onFailure);
        }

        private void UpdateCollaboration(string collaborationId, Role role, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private void UpdateCollaboration(string collaborationId, Role role, Status status, Field[] fields, Action<Collaboration> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status, fields);
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