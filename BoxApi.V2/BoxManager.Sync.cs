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

        public Folder CopyFolder(string id, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
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

        public Folder MoveFolder(string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, newParentId);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }
        public Folder Rename(Folder folder, string newName, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return RenameFolder(folder.Id, newName, fields);
        }

        public Folder RenameFolder(string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, name: newName);
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

    

        public Comment CreateComment(File file, string comment, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return CreateFileComment(file.Id, comment, fields);
        }

        public Comment CreateFileComment(string fileId, string comment, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            var restRequest = _requestHelper.CreateComment(ResourceType.File, fileId, comment, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public CommentCollection GetComments(Discussion discussion, Field[] fields = null)
        {
            GuardFromNull(discussion, "discussion");
            return GetDiscussionComments(discussion.Id, fields);
        }

        public CommentCollection GetDiscussionComments(string discussionId, Field[] fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            var restRequest = _requestHelper.GetComments(ResourceType.Discussion, discussionId, fields);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        public Comment CreateComment(Discussion discussion, string comment, Field[] fields = null)
        {
            GuardFromNull(discussion, "discussion");
            return CreateDiscussionComment(discussion.Id, comment, fields);
        }

        public Comment CreateDiscussionComment(string discussionId, string comment, Field[] fields = null)
        {
            GuardFromNull(discussionId, "discussionId");
            GuardFromNull(comment, "comment");
            var restRequest = _requestHelper.CreateComment(ResourceType.Discussion, discussionId, comment, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public CommentCollection GetComments(File file, Field[] fields = null)
        {
            GuardFromNull(file, "file");
            return GetFileComments(file.Id, fields);
        }

        public CommentCollection GetFileComments(string fileId, Field[] fields = null)
        {
            GuardFromNull(fileId, "fileId");
            var restRequest = _requestHelper.GetComments(ResourceType.File, fileId, fields);
            return _restClient.ExecuteAndDeserialize<CommentCollection>(restRequest);
        }

        public Comment GetComment(Comment comment, Field[] fields = null)
        {
            GuardFromNull(comment, "comment");
            return GetComment(comment.Id, fields);
        }

        private Comment GetComment(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var restRequest = _requestHelper.Get(ResourceType.Comment, id, fields);
            return _restClient.ExecuteAndDeserialize<Comment>(restRequest);
        }

        public Discussion CreateDiscussion(Folder parent, string name, string description = null, Field[] fields = null)
        {
            GuardFromNull(parent, "parent");
            return CreateDiscussion(parent.Id, name, description, fields);
        }

        public Discussion CreateDiscussion(string parentId, string name, string description = null, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateDiscussion(parentId, name, description, fields);
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

        public Discussion GetDiscussion(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Discussion, id, fields);
            return _restClient.ExecuteAndDeserialize<Discussion>(request);
        }

        public DiscussionCollection GetDiscussions(Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetDiscussions(folder.Id, fields);
        }

        private DiscussionCollection GetDiscussions(string folderId, Field[] fields = null)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetDiscussions(folderId, fields);
            return _restClient.ExecuteAndDeserialize<DiscussionCollection>(request);
        }

        public Collaboration CreateCollaboration(Folder folder, string userId, Role role, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return CreateCollaboration(folder.Id, userId, role, fields);
        }

        public Collaboration CreateCollaboration(string folderId, string userId, Role role, Field[] fields = null)
        {
            GuardFromNull(folderId, "folderId");
            GuardFromNull(userId, "userId");
            GuardFromNull(role, "role");
            var request = _requestHelper.CreateCollaboration(folderId, userId, role.Description(), fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public Collaboration Get(Collaboration collaboration, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return GetCollaboration(collaboration.Id, fields);
        }

        private Collaboration GetCollaboration(string collaborationId, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.GetCollaboration(collaborationId, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        public CollaborationCollection GetCollaborations(Folder folder, bool pendingCollaborationsOnly = false, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetCollaborations(folder.Id, pendingCollaborationsOnly, fields);
        }

        private CollaborationCollection GetCollaborations(string folderId, bool pendingCollaborationsOnly = false, Field[] fields = null)
        {
            GuardFromNull(folderId, "folderId");
            var request = _requestHelper.GetCollaborations(folderId, pendingCollaborationsOnly, fields);
            return _restClient.ExecuteAndDeserialize<CollaborationCollection>(request);
        }

        public Collaboration Update(Collaboration collaboration, Role role, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, fields);
        }

        public Collaboration Update(Collaboration collaboration, Role role, Status status, Field[] fields = null)
        {
            GuardFromNull(collaboration, "collaboration");
            return UpdateCollaboration(collaboration.Id, role, status, fields);
        }

        private Collaboration UpdateCollaboration(string collaborationId, Role role, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, fields);
            return _restClient.ExecuteAndDeserialize<Collaboration>(request);
        }

        private Collaboration UpdateCollaboration(string collaborationId, Role role, Status status, Field[] fields = null)
        {
            GuardFromNull(collaborationId, "collaborationId");
            var request = _requestHelper.UpdateCollaboration(collaborationId, role, status, fields);
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