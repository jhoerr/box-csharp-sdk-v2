using System;
using System.Linq;
using BoxApi.V2.SDK.Model;
using RestSharp;
using Type = BoxApi.V2.SDK.Model.Type;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public void Get(Folder folder, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            GetFolder(folder.Id, onSuccess, onFailure);
        }

        public void GetFolder(string id, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(Type.Folder, id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void GetItems(string id, Action<ItemCollection> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var folderItems = _requestHelper.GetItems(id);
            _restClient.ExecuteAsync(folderItems, onSuccess, onFailure);
        }

        public void CreateFolder(string parentId, string name, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFolder(parentId, name);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Folder folder, bool recursive, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(folder.Id, recursive, onSuccess, onFailure);
        }

        public void DeleteFolder(string id, bool recursive, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFolder(id, recursive);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Copy(Folder folder, Folder newParent, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(newParent, "newParent");
            Copy(folder, newParent.Id, onSuccess, onFailure, newName);
        }

        public void Copy(Folder folder, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(folder, "folder");
            CopyFolder(folder.Id, newParentId, onSuccess, onFailure, newName);
        }

        public void CopyFolder(string id, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(Type.Folder, id, newParentId, newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareLink(Folder folder, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            ShareFolderLink(folder.Id, sharedLink, onSuccess, onFailure);
        }

        public void ShareLink(File file, SharedLink sharedLink, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            ShareFileLink(file.Id, sharedLink, onSuccess, onFailure);
        }

        public void ShareFolderLink(string id, SharedLink sharedLink, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(Type.Folder, id, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void ShareFileLink(string id, SharedLink sharedLink, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(Type.File, id, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Move(Folder folder, Folder newParent, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(newParent, "newParent");
            Move(folder, newParent.Id, onSuccess, onFailure);
        }

        public void Move(Folder folder, string newParentId, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            MoveFolder(folder.Id, newParentId, onSuccess, onFailure);
        }

        public void MoveFolder(string id, string newParentId, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(Type.Folder, id, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Move(File file, Folder newParent, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(newParent, "newParent");
            Move(file, newParent.Id, onSuccess, onFailure);
        }

        public void Move(File file, string newParentId, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            MoveFile(file.Id, newParentId, onSuccess, onFailure);
        }

        public void MoveFile(string id, string newParentId, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(Type.File, id, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Rename(Folder folder, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            RenameFolder(folder.Id, newName, onSuccess, onFailure);
        }

        public void Rename(File file, string newName, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            RenameFile(file.Id, newName, onSuccess, onFailure);
        }

        public void RenameFolder(string id, string newName, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(Type.Folder, id, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void RenameFile(string id, string newName, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(Type.File, id, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void UpdateDescription(Folder folder, string description, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            UpdateFolderDescription(folder.Id, description, onSuccess, onFailure);
        }

        private void UpdateFolderDescription(string id, string description, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(Type.Folder, id, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void UpdateDescription(File file, string description, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            UpdateFileDescription(file.Id, description, onSuccess, onFailure);
        }

        public void UpdateFileDescription(string id, string description, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(Type.File, id, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Folder folder, Action<Folder> onSuccess, Action onFailure)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(Type.Folder, folder.Id, parentId, folder.Name, folder.Description, folder.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(File file, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            var request = _requestHelper.Update(Type.File, file.Id, file.Parent.Id, file.Name, file.Description, file.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Comment comment, Action<Comment> onSuccess, Action onFailure)
        {
            GuardFromNull(comment, "comment");
            var request = _requestHelper.Update(Type.Comment, comment.Id, message: comment.Message);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Get(File file, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            GetFile(file.Id, onSuccess, onFailure);
        }

        public void GetFile(string id, Action<File> onSuccess, Action onFailure)
        {
            GetFile(id, 0, onSuccess, onFailure);
        }

        private void GetFile(string id, int attempt, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);

            var request = _requestHelper.Get(Type.File, id);

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

        public void Delete(File file, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            DeleteFile(file.Id, file.Etag, onSuccess, onFailure);
        }

        public void DeleteFile(string id, string etag, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(etag, "etag");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFile(id, etag);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Delete(Comment comment, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(comment, "comment");
            DeleteComment(comment.Id, onSuccess, onFailure);
        }

        public void DeleteComment(string id, Action<IRestResponse> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteComment(id);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void CreateFile(string parentId, string name, Action<File> onSuccess, Action onFailure)
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

        public void Read(File file, Action<byte[]> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            Read(file.Id, onSuccess, onFailure);
        }

        public void Read(string id, Action<byte[]> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Read(id);
            Action<IRestResponse> onSuccessWrapper = response => onSuccess(response.RawBytes);
            _restClient.ExecuteAsync(request, onSuccessWrapper, onFailure);
        }

        public void Write(File file, byte[] content, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            Write(file.Id, file.Name, content, onSuccess, onFailure);
        }

        public void Write(string id, string name, byte[] content, Action<File> onSuccess, Action onFailure)
        {
            GuardFromNull(id, "id");
            GuardFromNull(content, "content");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Write(id, name, content);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        private static void GuardFromNullCallbacks(object onSuccess, object onFailure)
        {
            GuardFromNull(onSuccess, "onSuccess");
            GuardFromNull(onFailure, "onFailure");
        }

        public void Copy(File file, Folder newParent, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(file, "file");
            GuardFromNull(newParent, "folder");
            CopyFile(file.Id, newParent.Id, onSuccess, onFailure, newName);
        }

        public void Copy(File file, string newParentId, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(file, "file");
            CopyFile(file.Id, newParentId, onSuccess, onFailure, newName);
        }

        public void CopyFile(string id, string newParentId, Action<File> onSuccess, Action onFailure, string newName = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(Type.File, id, newParentId, newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void AddComment(File file, string comment, Action<Comment> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            AddComment(file.Id, comment, onSuccess, onFailure);
        }

        private void AddComment(string fileId, string comment, Action<Comment> onSuccess, Action onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNull(comment, "comment");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.AddComment(fileId, comment);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComment(Comment comment, Action<Comment> onSuccess, Action onFailure)
        {
            GuardFromNull(comment, "comment");
            GetComment(comment.Id, onSuccess, onFailure);
        }

        private void GetComment(string commentId, Action<Comment> onSuccess, Action onFailure)
        {
            GuardFromNull(commentId, "commentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.Get(Type.Comment, commentId);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }

        public void GetComments(File file, Action<CommentCollection> onSuccess, Action onFailure)
        {
            GuardFromNull(file, "file");
            GetFileComments(file.Id, onSuccess, onFailure);
        }

        private void GetFileComments(string fileId, Action<CommentCollection> onSuccess, Action onFailure)
        {
            GuardFromNull(fileId, "fileId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var restRequest = _requestHelper.GetComments(Type.File, fileId);
            _restClient.ExecuteAsync(restRequest, onSuccess, onFailure);
        }
    }
}