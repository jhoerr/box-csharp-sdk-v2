using System;
using BoxApi.V2.Model;
using RestSharp;

namespace BoxApi.V2
{
    public partial class BoxManager
    {
        public Folder Get(Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            return GetFolder(folder.Id, fields);
        }

        public void Get(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            GetFolder(onSuccess, onFailure, folder.Id, fields);
        }

        public Folder GetFolder(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Folder, id, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public void GetFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Get(ResourceType.Folder, id, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            GetItems(onSuccess, onFailure, folder.Id, fields);
        }

        public void GetItems(Action<ItemCollection> onSuccess, Action<Error> onFailure, string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(fields, "fields");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var folderItems = _requestHelper.GetItems(id, fields);
            _restClient.ExecuteAsync(folderItems, onSuccess, onFailure);
        }

        public Folder CreateFolder(string parentId, string name, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public void CreateFolder(Action<Folder> onSuccess, Action<Error> onFailure, string parentId, string name, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.CreateFolder(parentId, name, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void Delete(Action<IRestResponse> onSuccess, Action<Error> onFailure, Folder folder, bool recursive)
        {
            GuardFromNull(folder, "folder");
            DeleteFolder(onSuccess, onFailure, folder.Id, recursive);
        }

        public void DeleteFolder(Action<IRestResponse> onSuccess, Action<Error> onFailure, string id, bool recursive)
        {
            GuardFromNull(id, "id");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.DeleteFolder(id, recursive);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void Copy(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Folder newParent, string newName, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            Copy(onSuccess, onFailure, folder, newParent.Id, newName, fields);
        }

        public void Copy(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            CopyFolder(onSuccess, onFailure, folder.Id, newParentId, newName, fields);
        }

        public void CopyFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newParentId, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Copy(ResourceType.Folder, id, newParentId, newName, fields);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void ShareLink(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            ShareFolderLink(onSuccess, onFailure, folder.Id, sharedLink, fields);
        }

        public void ShareFolderLink(Action<Folder> onSuccess, Action<Error> onFailure, string id, SharedLink sharedLink, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(sharedLink, "sharedLink");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, sharedLink: sharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void Move(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Folder newParent, Field[] fields = null)
        {
            GuardFromNull(newParent, "newParent");
            Move(onSuccess, onFailure, folder, newParent.Id, fields);
        }

        public void Move(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newParentId, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            MoveFolder(onSuccess, onFailure, folder.Id, newParentId, fields);
        }

        public void MoveFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newParentId, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newParentId, "newParentId");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, newParentId);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void Rename(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string newName, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            RenameFolder(onSuccess, onFailure, folder.Id, newName, fields);
        }

        public void RenameFolder(Action<Folder> onSuccess, Action<Error> onFailure, string id, string newName, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(newName, "newName");
            GuardFromNullCallbacks(onSuccess, onFailure);
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, name: newName);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
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

        public void UpdateDescription(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, string description, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            UpdateFolderDescription(onSuccess, onFailure, folder.Id, description, fields);
        }

        private void UpdateFolderDescription(Action<Folder> onSuccess, Action<Error> onFailure, string id, string description, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            GuardFromNull(description, "description");
            var request = _requestHelper.Update(ResourceType.Folder, id, fields, description: description);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }

        public void Update(Action<Folder> onSuccess, Action<Error> onFailure, Folder folder, Field[] fields = null)
        {
            GuardFromNull(folder, "folder");
            var parentId = folder.Parent == null ? null : folder.Parent.Id;
            var request = _requestHelper.Update(ResourceType.Folder, folder.Id, fields, parentId, folder.Name, folder.Description, folder.SharedLink);
            _restClient.ExecuteAsync(request, onSuccess, onFailure);
        }
    }
}