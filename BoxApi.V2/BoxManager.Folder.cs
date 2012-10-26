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
            return GetFolder(folder.Id, null);
        }

        public void Get(Folder folder, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
        {
            GuardFromNull(folder, "folder");
            GetFolder(folder.Id, null, onSuccess, onFailure);
        }

        public Folder GetFolder(string id, Field[] fields = null)
        {
            GuardFromNull(id, "id");
            var request = _requestHelper.Get(ResourceType.Folder, id, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public void GetFolder(string id, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
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

        public Folder CreateFolder(string parentId, string name, Field[] fields = null)
        {
            GuardFromNull(parentId, "parentId");
            GuardFromNull(name, "name");
            var request = _requestHelper.CreateFolder(parentId, name, fields);
            return _restClient.ExecuteAndDeserialize<Folder>(request);
        }

        public void CreateFolder(string parentId, string name, Field[] fields, Action<Folder> onSuccess, Action<Error> onFailure)
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
    }
}