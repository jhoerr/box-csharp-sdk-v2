using System;
using System.Collections.Generic;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Model.Fields;
using BoxApi.V2.Serialization;
using Newtonsoft.Json;
using RestSharp;

namespace BoxApi.V2
{
    internal class RequestHelper
    {
        public IRestRequest Get(ResourceType resourceResourceType, IEnumerable<IField> fields = null, string etag = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, null, Method.GET, fields);
            TryAddIfNoneMatchHeader(request, etag);
            return request;
        }

        public IRestRequest Get(ResourceType resourceResourceType, string id, IEnumerable<IField> fields = null, string etag = null, int? limit = null, int? offset = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}", Method.GET, fields);
            TryAddIfNoneMatchHeader(request, etag);
            request.AddUrlSegment("id", id.Trim());
            TryAddParameter(request, "limit", limit);
            TryAddParameter(request, "offset", offset);
            return request;
        }

        private void TryAddParameter(IRestRequest request, string name, int? value)
        {
            if (value.HasValue)
            {
                request.AddParameter(name, value.Value);
            }
        }

        public IRestRequest GetItems(string id, IEnumerable<FolderField> fields = null, int? limit = null, int? offset = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, "{id}/items", Method.GET, fields);
            request.AddUrlSegment("id", id.Trim());
            TryAddParameter(request, "limit", limit);
            TryAddParameter(request, "offset", offset);
            return request;
        }

        public IRestRequest GetDiscussions(string folderId, IEnumerable<DiscussionField> fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, "{id}/discussions", Method.GET, fields);
            request.AddUrlSegment("id", folderId.Trim());
            return request;
        }

        public IRestRequest CreateFolder(string parentId, string name, IEnumerable<FolderField> fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, null, Method.POST, fields);
            request.AddBody(new {name, parent = new {id = parentId.Trim()}});
            return request;
        }

        public IRestRequest CreateFile(string parentId, string name, byte[] content, IEnumerable<FileField> fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.File, "content", Method.POST, fields);
            request.AddFile("filename1", content, name.Trim());
            request.AddParameter("folder_id", parentId.Trim());
            return request;
        }

        public IRestRequest DeleteFolder(string id, bool recursive, string etag)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Folder, id.Trim());
            TryAddIfMatchHeader(request, etag);
            request.AddParameter("recursive", AddBoolParameter(recursive));
            return request;
        }

        private static string AddBoolParameter(bool parameter)
        {
            return parameter.ToString().ToLower();
        }

        public IRestRequest DeleteFile(string id, string etag)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.File, id.Trim());
            TryAddIfMatchHeader(request, etag);
            return request;
        }

        public IRestRequest DeleteComment(string id)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Comment, id.Trim());
            return request;
        }

        public IRestRequest DeleteDiscussion(string id)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Discussion, id.Trim());
            return request;
        }

        private IRestRequest GetDeleteRequest(ResourceType resourceResourceType, string id)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id.Trim());
            return request;
        }

        public IRestRequest Copy(ResourceType resourceResourceType, string id, string newParentId, string name = null, IEnumerable<IField> fields = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}/copy", Method.POST, fields);
            request.AddUrlSegment("id", id.Trim());
            if (string.IsNullOrWhiteSpace(name))
            {
                request.AddBody(new {parent = new {id = newParentId.Trim()}});
            }
            else
            {
                request.AddBody(new {parent = new {id = newParentId.Trim()}, name = name.Trim()});
            }
            return request;
        }

        public IRestRequest Update<TField>(ResourceType resourceResourceType, string id, string etag, IEnumerable<TField> fields, string parentId = null, string name = null, string description = null, SharedLink sharedLink = null, string message = null) where TField:IField
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", id.Trim());

            TryAddIfMatchHeader(request, etag);

            var body = new Dictionary<string, object>();
            TryAddToBody(body, "parent", Massage(parentId), new { id = Massage(parentId) });
            TryAddToBody(body, "name", Massage(name));
            TryAddToBody(body, "description", Massage(description));
            TryAddToBody(body, "shared_link", sharedLink);
            TryAddToBody(body, "message", Massage(message));

            request.AddBody(body);
            return request;
        }

        public IRestRequest DisableSharedLink(ResourceType resourceType, string id, string etag, IEnumerable<FolderField> fields)
        {
            IRestRequest request = JsonRequest(resourceType, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", id.Trim());

            TryAddIfMatchHeader(request, etag);

            request.AddBody(new {shared_link = (object) null});
            return request;
        }

        private static string Massage(string name)
        {
            return name == null ? null : name.Trim();
        }

        private static void TryAddIfMatchHeader(IRestRequest request, string etag)
        {
            if (!string.IsNullOrWhiteSpace(etag))
            {
                request.AddHeader("If-Match", etag);
            }
        }

        private static void TryAddIfNoneMatchHeader(IRestRequest request, string etag)
        {
            if (!string.IsNullOrWhiteSpace(etag))
            {
                request.AddHeader("If-None-Match", etag);
            }
        }

        private static void TryAddToBody(Dictionary<string, object> body, string parent, object value, object field = null)
        {
            if (value != null)
            {
                body.Add(parent, field ?? value);
            }
        }

        public IRestRequest Read(string id)
        {
            IRestRequest request = RawRequest(ResourceType.File, "{id}/content");
            request.AddUrlSegment("id", id.Trim());
            return request;
        }

        public IRestRequest Write(string id, string name, string etag, byte[] content)
        {
            IRestRequest request = JsonRequest(ResourceType.File, "{id}/content", Method.POST);
            request.AddUrlSegment("id", id.Trim());
            TryAddIfMatchHeader(request, etag);
            request.AddFile("filename", content, name.Trim());

            return request;
        }

        public IRestRequest GetVersions(string fileId, IEnumerable<FileField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.File, "{id}/versions", Method.GET, fields);
            request.AddUrlSegment("id", fileId.Trim());
            return request;
        }

        public IRestRequest CreateComment(ResourceType resourceType, string id, string message, IEnumerable<CommentField> fields)
        {
            IRestRequest request = JsonRequest(resourceType, "{id}/comments", Method.POST, fields);
            request.AddUrlSegment("id", id.Trim());
            request.AddBody(new {message});
            return request;
        }

        public IRestRequest GetComments(ResourceType resourceResourceType, string id, IEnumerable<CommentField> fields)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}/comments", Method.GET, fields);
            request.AddUrlSegment("id", id.Trim());
            return request;
        }

        public IRestRequest CreateDiscussion(string parentId, string name, string description, IEnumerable<DiscussionField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Discussion, null, Method.POST, fields);
            request.AddBody(new {parent = new {id = parentId.Trim()}, name, description});
            return request;
        }

        public IRestRequest CreateCollaboration(string folderId, string userId, string role, IEnumerable<CollaborationField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, null, Method.POST, fields);
            request.AddBody(new {item = new {type = "folder", id = folderId.Trim()}, accessible_by = new {id = userId.Trim()}, role});
            return request;
        }

        public IRestRequest CreateCollaborationByEmail(string folderId, string emailAddress, string role, IEnumerable<CollaborationField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, null, Method.POST, fields);
            request.AddBody(new { item = new { type = "folder", id = folderId.Trim() }, accessible_by = new { login = emailAddress.Trim() }, role });
            return request;
        }

        public IRestRequest GetCollaboration(string collaborationId, IEnumerable<CollaborationField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, "{id}", Method.GET, fields);
            request.AddUrlSegment("id", collaborationId.Trim());
            return request;
        }

        public IRestRequest GetCollaborations(string folderId, bool onlyPending, IEnumerable<CollaborationField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, "{id}/collaborations", Method.GET, fields);
            request.AddUrlSegment("id", folderId.Trim());
            if (onlyPending)
            {
                request.AddParameter("status", "pending");
            }
            return request;
        }

        public IRestRequest UpdateCollaboration(string collaborationId, CollaborationRole role, IEnumerable<CollaborationField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", collaborationId.Trim());
            request.AddBody(new {role = role.Description()});
            return request;
        }

        public IRestRequest UpdateCollaboration(string collaborationId, CollaborationRole role, Status status, IEnumerable<CollaborationField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", collaborationId.Trim());
            request.AddBody(new {role = role.Description(), status = status.Description()});
            return request;
        }

        public IRestRequest DeleteCollaboration(string collaborationId)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Collaboration, collaborationId.Trim());
            return request;
        }

        public IRestRequest GetUserEvents(string streamPosition, StreamType streamType, int limit)
        {
            IRestRequest request = JsonRequest(ResourceType.Event, null, Method.GET);
            request.AddParameter("stream_position", streamPosition);
            request.AddParameter("stream_type", streamType.Description());
            request.AddParameter("limit", limit);
            return request;
        }

        public IRestRequest GetEnterpriseEvents(int offset, int limit, DateTime? createdAfter, DateTime? createdBefore, EnterpriseEventType[] eventTypes)
        {
            IRestRequest request = JsonRequest(ResourceType.Event, null, Method.GET);
            request.AddParameter("stream_type", StreamType.AdminLogs.Description());
            request.AddParameter("offset", offset);
            request.AddParameter("limit", limit);
            if (createdAfter.HasValue)
            {
                request.AddParameter("created_after", createdAfter);
            }
            if (createdBefore.HasValue)
            {
                request.AddParameter("created_before", createdBefore);
            }
            if (eventTypes != null && eventTypes.Any())
            {
                string eventTypeList = string.Join(",", eventTypes.Select(f => f.Description()));
                request.AddParameter("event_type", eventTypeList);
            }
            return request;
        }

        public IRestRequest CreateToken(string emailAddress)
        {
            IRestRequest request = JsonRequest(ResourceType.Token, null, Method.POST);
            request.AddBody(new {email = emailAddress});
            return request;
        }

        public IRestRequest Me(IEnumerable<UserField> fields)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "me", Method.GET, fields);
            return request;
        }

        public IRestRequest GetUsers(string filterTerm, int? limit, int? offset)
        {
            IRestRequest request = JsonRequest(ResourceType.User, null, Method.GET, EnterpriseUserField.All);
            if (!string.IsNullOrWhiteSpace(filterTerm))
            {
                request.AddParameter("filter_term", filterTerm.Trim());
            }
            if (limit.HasValue)
            {
                request.AddParameter("limit", limit);
            }
            if (offset.HasValue)
            {
                request.AddParameter("offset", offset);
            }
            return request;
        }

        public IRestRequest GetUser(string id)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.GET, EnterpriseUserField.All);
            request.AddUrlSegment("id", id.Trim());
            return request;
        }

        public IRestRequest CreateUser(EnterpriseUser user)
        {
            IRestRequest request = JsonRequest(ResourceType.User, null, Method.POST, EnterpriseUserField.All);
            request.AddBody(user);
            return request;
        }

        public IRestRequest UpdateUser(EnterpriseUser user)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.PUT, EnterpriseUserField.All);
            request.AddUrlSegment("id", user.Id.Trim());
            request.AddBody(new UpdateableEnterpriseUser(user));
            return request;
        }

        public IRestRequest RemoveUserFromEnterprise(string id)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.PUT);
            request.AddUrlSegment("id", id.Trim());
            request.AddBody(new Rollout());
            return request;
        }

        private class Rollout
        {
            public string Enterprise { get; set; }
        }

        public IRestRequest DeleteUser(string id, bool notify, bool force)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id.Trim());
            request.AddParameter("notify", AddBoolParameter(notify));
            request.AddParameter("force", AddBoolParameter(force));
            return request;
        }

        public IRestRequest MoveFolderToAnotherUser(string currentOwnerId, string folderId, string newOwnerId, bool notify)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{userId}/{folderType}/{folderId}", Method.PUT);
            request.AddUrlSegment("userId", currentOwnerId.Trim());
            request.AddUrlSegment("folderType", ResourceType.Folder.Description());
            request.AddUrlSegment("folderId", folderId.Trim());
            request.AddParameter("notify", AddBoolParameter(notify));
            request.AddBody(new {owned_by = new {id = newOwnerId.Trim()}});
            return request;
        }

        public IRestRequest GetEmailAliases(string id)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}/email_aliases", Method.GET);
            request.AddUrlSegment("id", id.Trim());
            return request;
        }

        public IRestRequest AddAlias(string id, string alias)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}/email_aliases", Method.POST);
            request.AddUrlSegment("id", id.Trim());
            request.AddBody(new {email = alias.Trim()});
            return request;
        }

        public IRestRequest DeleteAlias(string userId, string aliasId)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}/email_aliases/{aliasId.Trim()}", Method.DELETE);
            request.AddUrlSegment("id", userId.Trim());
            request.AddUrlSegment("aliasId", aliasId.Trim());
            return request;
        }

        public IRestRequest UpdateLogin(string userId, string login)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.PUT);
            request.AddUrlSegment("id", userId.Trim());
            request.AddBody(new {login = login.Trim()});
            return request;
        }

        public IRestRequest Search(string query, uint? limit, uint? offset)
        {
            IRestRequest request = JsonRequest(ResourceType.Search, null, Method.GET);
            request.AddParameter("query", query);
            if (limit != null)
            {
                request.AddParameter("limit", limit);
            }
            if (offset != null)
            {
                request.AddParameter("offset", offset);
            }
            return request;
        }

        public IRestRequest GetThumbnail(string fileId, ThumbnailSize? minHeight = null, ThumbnailSize? minWidth = null, ThumbnailSize? maxHeight = null, ThumbnailSize? maxWidth = null, string extension = "png")
        {
            IRestRequest request = JsonRequest(ResourceType.File, "{id}/thumbnail.{extension}", Method.GET);
            request.AddUrlSegment("id", fileId.Trim());
            request.AddUrlSegment("extension", extension);
            if (minHeight != null)
            {
                request.AddParameter("min_height", minHeight.Description());
            }
            if (minWidth != null)
            {
                request.AddParameter("min_width", minWidth.Description());
            }
            if (maxHeight != null)
            {
                request.AddParameter("max_height", maxHeight.Description());
            }
            if (maxWidth != null)
            {
                request.AddParameter("max_width", maxWidth.Description());
            }
            return request;
        }

        private IRestRequest RawRequest(ResourceType resourceResourceType, string resource, Method method = Method.GET)
        {
            string path = "{version}/{type}" + (string.IsNullOrEmpty(resource) ? string.Empty : string.Format("/{0}", resource));
            var request = new RestRequest(path, method);
            request.AddUrlSegment("version", "2.0");
            request.AddUrlSegment("type", resourceResourceType.Description());
            return request;
        }

        private IRestRequest JsonRequest(ResourceType resourceResourceType, string resource, Method method)
        {
            IRestRequest request = RawRequest(resourceResourceType, resource, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new AttributableJsonSerializer();
            return request;
        }

        private IRestRequest JsonRequest<TField>(ResourceType resourceResourceType, string resource, Method method, IEnumerable<TField> fields = null) where TField : IField
        {
            var request = JsonRequest(resourceResourceType, resource, method);
            if (fields != null && fields.Any())
            {
                string fieldList = string.Join(",", fields.Select(f => f.Value));
                if (method == Method.GET)
                {
                    request.AddParameter("fields", fieldList);
                }
                else
                {
                    request.Resource += string.Format("?fields={0}", fieldList);
                }
            }

            return request;
        }


        private class UpdateableEnterpriseUser : EnterpriseUser
        {
            public UpdateableEnterpriseUser(EnterpriseUser user)
            {
                Address = user.Address;
                CanSeeManagedUsers = user.CanSeeManagedUsers;
                IsExemptFromDeviceLimits = user.IsExemptFromDeviceLimits;
                IsExemptFromLoginVerification = user.IsExemptFromLoginVerification;
                IsPasswordResetRequired = user.IsPasswordResetRequired;
                IsSyncEnabled = user.IsSyncEnabled;
                JobTitle = user.JobTitle;
                Language = user.Language;
                Name = user.Name;
                Phone = user.Phone;
                Role = user.Role;
                SpaceAmount = user.SpaceAmount;
                Status = user.Status;
                TrackingCodes = user.TrackingCodes;
            }

            [JsonIgnore]
            public new string Login { get; set; }
        }
    }
}