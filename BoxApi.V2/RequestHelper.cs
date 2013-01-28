using System;
using System.Collections.Generic;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Model.Enum;
using BoxApi.V2.Serialization;
using Newtonsoft.Json;
using RestSharp;

namespace BoxApi.V2
{
    internal class RequestHelper
    {
        public IRestRequest Get(ResourceType resourceResourceType, Field[] fields = null, string etag = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, null, Method.GET, fields);
            TryAddIfNoneMatchHeader(request, etag);
            return request;
        }

        public IRestRequest Get(ResourceType resourceResourceType, string id, Field[] fields = null, string etag = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}", Method.GET, fields);
            TryAddIfNoneMatchHeader(request, etag);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest GetItems(string id, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, "{id}/items", Method.GET, fields);
            request.AddUrlSegment("id", id);

            return request;
        }

        public IRestRequest GetDiscussions(string folderId, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, "{id}/discussions", Method.GET, fields);
            request.AddUrlSegment("id", folderId);
            return request;
        }

        public IRestRequest CreateFolder(string parentId, string name, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, null, Method.POST, fields);
            request.AddBody(new {name, parent = new {id = parentId}});
            return request;
        }

        public IRestRequest CreateFile(string parentId, string name, byte[] content, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.File, "content", Method.POST, fields);
            request.AddFile("filename1", content, name);
            request.AddParameter("folder_id", parentId);
            return request;
        }

        public IRestRequest DeleteFolder(string id, bool recursive, string etag)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Folder, id);
            TryAddIfMatchHeader(request, etag);
            request.AddParameter("recursive", recursive.ToString().ToLower());
            return request;
        }

        public IRestRequest DeleteFile(string id, string etag)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.File, id);
            TryAddIfMatchHeader(request, etag);
            return request;
        }

        public IRestRequest DeleteComment(string id)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Comment, id);
            return request;
        }

        public IRestRequest DeleteDiscussion(string id)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Discussion, id);
            return request;
        }

        private IRestRequest GetDeleteRequest(ResourceType resourceResourceType, string id)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest Copy(ResourceType resourceResourceType, string id, string newParentId, string name = null, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}/copy", Method.POST, fields);
            request.AddUrlSegment("id", id);
            if (string.IsNullOrWhiteSpace(name))
            {
                request.AddBody(new {parent = new {id = newParentId}});
            }
            else
            {
                request.AddBody(new {parent = new {id = newParentId}, name});
            }
            return request;
        }

        public IRestRequest Update(ResourceType resourceResourceType, string id, string etag, Field[] fields, string parentId = null, string name = null, string description = null, SharedLink sharedLink = null, string message = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", id);

            TryAddIfMatchHeader(request, etag);

            var body = new Dictionary<string, object>();
            TryAddToBody(body, "parent", parentId, new {id = parentId});
            TryAddToBody(body, "name", name);
            TryAddToBody(body, "description", description);
            TryAddToBody(body, "shared_link", sharedLink);
            TryAddToBody(body, "message", message);

            request.AddBody(body);
            return request;
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
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest Write(string id, string name, string etag, byte[] content)
        {
            IRestRequest request = JsonRequest(ResourceType.File, "{id}/content", Method.POST);
            request.AddUrlSegment("id", id);
            TryAddIfMatchHeader(request, etag);
            request.AddFile("filename", content, name);

            return request;
        }

        public IRestRequest GetVersions(string fileId, Field[] fields)
        {
            IRestRequest request = JsonRequest(ResourceType.File, "{id}/versions", Method.GET, fields);
            request.AddUrlSegment("id", fileId);
            return request;
        }

        public IRestRequest CreateComment(ResourceType resourceType, string id, string message, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(resourceType, "{id}/comments", Method.POST, fields);
            request.AddUrlSegment("id", id);
            request.AddBody(new {message});
            return request;
        }

        public IRestRequest GetComments(ResourceType resourceResourceType, string id, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(resourceResourceType, "{id}/comments", Method.GET, fields);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest CreateDiscussion(string parentId, string name, string description, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Discussion, null, Method.POST, fields);
            request.AddBody(new {parent = new {id = parentId}, name, description});
            return request;
        }

        public IRestRequest CreateCollaboration(string folderId, string userId, string role, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, null, Method.POST, fields);
            request.AddBody(new {item = new {type = "folder", id = folderId}, accessible_by = new {id = userId}, role});
            return request;
        }

        public IRestRequest GetCollaboration(string collaborationId, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, "{id}", Method.GET, fields);
            request.AddUrlSegment("id", collaborationId);
            return request;
        }

        public IRestRequest GetCollaborations(string folderId, bool onlyPending, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Folder, "{id}/collaborations", Method.GET, fields);
            request.AddUrlSegment("id", folderId);
            if (onlyPending)
            {
                request.AddParameter("status", "pending");
            }
            return request;
        }

        public IRestRequest UpdateCollaboration(string collaborationId, CollaborationRole role, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", collaborationId);
            request.AddBody(new {role = role.Description()});
            return request;
        }

        public IRestRequest UpdateCollaboration(string collaborationId, CollaborationRole role, Status status, Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.Collaboration, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", collaborationId);
            request.AddBody(new {role = role.Description(), status = status.Description()});
            return request;
        }

        public IRestRequest DeleteCollaboration(string collaborationId)
        {
            IRestRequest request = GetDeleteRequest(ResourceType.Collaboration, collaborationId);
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
            if (createdAfter.HasValue)
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

        public IRestRequest Me(Field[] fields = null)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "me", Method.GET, fields);
            return request;
        }

        public IRestRequest GetUsers(string filterTerm, int? limit, int? offset)
        {
            IRestRequest request = JsonRequest(ResourceType.User, null, Method.GET, EnterpriseUser.Fields);
            if (!string.IsNullOrWhiteSpace(filterTerm))
            {
                request.AddParameter("filter_term", filterTerm);
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
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.GET, EnterpriseUser.Fields);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest CreateUser(EnterpriseUser user)
        {
            IRestRequest request = JsonRequest(ResourceType.User, null, Method.POST, EnterpriseUser.Fields);
            request.AddBody(user);
            return request;
        }

        public IRestRequest UpdateUser(EnterpriseUser user)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.PUT, EnterpriseUser.Fields);
            request.AddUrlSegment("id", user.Id);
            request.AddBody(new UpdateableEnterpriseUser(user));
            return request;
        }

        public IRestRequest DeleteUser(string id, bool notify, bool force)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddParameter("notify", notify);
            request.AddParameter("force", force);
            return request;
        }

        public IRestRequest MoveFolderToAnotherUser(string currentOwnerId, string folderId, string newOwnerId, bool notify)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{userId}/{folderType}/{folderId}", Method.PUT, EnterpriseUser.Fields);
            request.AddUrlSegment("userId", currentOwnerId);
            request.AddUrlSegment("folderType", ResourceType.Folder.Description());
            request.AddUrlSegment("folderId", folderId);
            // Notify URL parameter seems to result in a 400..
            // request.AddParameter("notify", notify);
            request.AddBody(new {owned_by = new {id = newOwnerId}});
            return request;
        }

        public IRestRequest GetEmailAliases(string id)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}/email_aliases", Method.GET);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest AddAlias(string id, string alias)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}/email_aliases", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new {email = alias});
            return request;
        }

        public IRestRequest DeleteAlias(string userId, string aliasId)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}/email_aliases/{aliasId}", Method.DELETE);
            request.AddUrlSegment("id", userId);
            request.AddUrlSegment("aliasId", aliasId);
            return request;
        }

        public IRestRequest UpdateLogin(string userId, string login)
        {
            IRestRequest request = JsonRequest(ResourceType.User, "{id}", Method.PUT);
            request.AddUrlSegment("id", userId);
            request.AddBody(new {login});
            return request;
        }

        public IRestRequest Search(string query, uint limit, uint offset)
        {
            IRestRequest request = JsonRequest(ResourceType.Search, null, Method.GET);
            request.AddParameter("query", query);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);
            return request;
        }

        public IRestRequest GetThumbnail(string fileId, ThumbnailSize? minHeight = null, ThumbnailSize? minWidth = null, ThumbnailSize? maxHeight = null, ThumbnailSize? maxWidth = null, string extension = "png")
        {
            IRestRequest request = JsonRequest(ResourceType.File, "{id}/thumbnail.{extension}", Method.GET);
            request.AddUrlSegment("id", fileId);
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

        private IRestRequest RawRequest(ResourceType resourceResourceType, string resource, Method method = Method.GET, string fieldList = null)
        {
            string path = "{version}/{type}" + (string.IsNullOrEmpty(resource) ? string.Empty : string.Format("/{0}", resource));
            if (!string.IsNullOrWhiteSpace(fieldList) && method != Method.GET)
            {
                path += string.Format("?fields={0}", fieldList);
            }
            var request = new RestRequest(path, method);
            request.AddUrlSegment("version", "2.0");
            request.AddUrlSegment("type", resourceResourceType.Description());
            if (!string.IsNullOrWhiteSpace(fieldList) && method == Method.GET)
            {
                request.AddParameter("fields", fieldList);
            }
            return request;
        }

        private IRestRequest JsonRequest(ResourceType resourceResourceType, string resource, Method method, Field[] fields = null)
        {
            string fieldList = null;
            if (fields != null && fields.Any())
            {
                fieldList = string.Join(",", fields.Select(f => f.Description()));
            }

            IRestRequest request = RawRequest(resourceResourceType, resource, method, fieldList);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new AttributableJsonSerializer();
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