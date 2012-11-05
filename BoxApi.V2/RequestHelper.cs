using System;
using System.Collections.Generic;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Serialization;
using RestSharp;

namespace BoxApi.V2
{
    internal class RequestHelper
    {
        public IRestRequest Get(ResourceType resourceResourceType, string id, Field[] fields = null)
        {
            var request = JsonRequest(resourceResourceType, "{id}", Method.GET, fields);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest GetItems(string id, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Folder, "{id}/items", Method.GET, fields);
            request.AddUrlSegment("id", id);

            return request;
        }

        public IRestRequest GetDiscussions(string folderId, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Folder, "{id}/discussions", Method.GET, fields);
            request.AddUrlSegment("id", folderId);
            return request;
        }

        public IRestRequest CreateFolder(string parentId, string name, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Folder, null, Method.POST, fields);
            request.AddBody(new {name, parent = new {id = parentId}});
            return request;
        }

        public IRestRequest CreateFile(string parentId, string name, byte[] content, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.File, "content", Method.POST, fields);
            request.AddFile("filename1", content, name);
            request.AddParameter("folder_id", parentId);
            return request;
        }

        public IRestRequest DeleteFolder(string id, bool recursive)
        {
            var request = GetDeleteRequest(ResourceType.Folder, id);
            request.AddParameter("recursive", recursive.ToString().ToLower());
            return request;
        }

        public IRestRequest DeleteFile(string id, string etag)
        {
            var request = GetDeleteRequest(ResourceType.File, id);
            request.AddHeader("If-Match", etag ?? string.Empty);
            return request;
        }

        public IRestRequest DeleteComment(string id)
        {
            var request = GetDeleteRequest(ResourceType.Comment, id);
            return request;
        }

        public IRestRequest DeleteDiscussion(string id)
        {
            var request = GetDeleteRequest(ResourceType.Discussion, id);
            return request;
        }

        private IRestRequest GetDeleteRequest(ResourceType resourceResourceType, string id)
        {
            var request = JsonRequest(resourceResourceType, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest Copy(ResourceType resourceResourceType, string id, string newParentId, string name = null, Field[] fields = null)
        {
            var request = JsonRequest(resourceResourceType, "{id}/copy", Method.POST, fields);
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

        public IRestRequest Update(ResourceType resourceResourceType, string id, Field[] fields, string parentId = null, string name = null, string description = null, SharedLink sharedLink = null,
                                   string message = null)
        {
            var request = JsonRequest(resourceResourceType, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", id);
            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(parentId))
            {
                body.Add("parent", new {id = parentId});
            }
            if (!string.IsNullOrEmpty(name))
            {
                body.Add("name", name);
            }
            if (!string.IsNullOrEmpty(description))
            {
                body.Add("description", description);
            }
            if (sharedLink != null)
            {
                body.Add("shared_link", sharedLink);
            }
            if (!string.IsNullOrEmpty(message))
            {
                body.Add("message", message);
            }

            request.AddBody(body);
            return request;
        }

        public IRestRequest Read(string id)
        {
            var request = RawRequest(ResourceType.File, "{id}/content");
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest Write(string id, string name, string etag, byte[] content)
        {
            var request = JsonRequest(ResourceType.File, "{id}/content", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddHeader("If-Match", etag ?? string.Empty);
            request.AddFile("filename", content, name);

            return request;
        }

        public IRestRequest CreateComment(ResourceType resourceType, string id, string message, Field[] fields = null)
        {
            var request = JsonRequest(resourceType, "{id}/comments", Method.POST, fields);
            request.AddUrlSegment("id", id);
            request.AddBody(new {message});
            return request;
        }

        public IRestRequest GetComments(ResourceType resourceResourceType, string id, Field[] fields = null)
        {
            var request = JsonRequest(resourceResourceType, "{id}/comments", Method.GET, fields);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest CreateDiscussion(string parentId, string name, string description, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Discussion, null, Method.POST, fields);
            request.AddBody(new {parent = new {id = parentId}, name, description});
            return request;
        }

        public IRestRequest CreateCollaboration(string folderId, string userId, string role, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Collaboration, null, Method.POST, fields);
            request.AddBody(new {item = new {type = "folder", id = folderId}, accessible_by = new {id = userId}, role});
            return request;
        }

        public IRestRequest GetCollaboration(string collaborationId, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Collaboration, "{id}", Method.GET, fields);
            request.AddUrlSegment("id", collaborationId);
            return request;
        }

        public IRestRequest GetCollaborations(string folderId, bool onlyPending, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Folder, "{id}/collaborations", Method.GET, fields);
            request.AddUrlSegment("id", folderId);
            if (onlyPending)
            {
                request.AddParameter("status", "pending");
            }
            return request;
        }

        public IRestRequest UpdateCollaboration(string collaborationId, Role role, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Collaboration, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", collaborationId);
            request.AddBody(new {role = role.Description()});
            return request;
        }

        public IRestRequest UpdateCollaboration(string collaborationId, Role role, Status status, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.Collaboration, "{id}", Method.PUT, fields);
            request.AddUrlSegment("id", collaborationId);
            request.AddBody(new {role = role.Description(), status = status.Description()});
            return request;
        }

        public IRestRequest DeleteCollaboration(string collaborationId)
        {
            var request = GetDeleteRequest(ResourceType.Collaboration, collaborationId);
            return request;
        }

        public IRestRequest GetUserEvents(string streamPosition, StreamType streamType, int limit)
        {
            var request = JsonRequest(ResourceType.Event, null, Method.GET);
            request.AddParameter("stream_position", streamPosition);
            request.AddParameter("stream_type", streamType.Description());
            request.AddParameter("limit", limit);
            return request;
        }

        public IRestRequest GetEnterpriseEvents(int offset, int limit, DateTime? createdAfter, DateTime? createdBefore, EnterpriseEventType[] eventTypes)
        {
            var request = JsonRequest(ResourceType.Event, null, Method.GET);
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
                var eventTypeList = string.Join(",", eventTypes.Select(f => f.Description()));
                request.AddParameter("event_type", eventTypeList);
            }
            return request;
        }

        public IRestRequest CreateToken(string emailAddress)
        {
            var request = JsonRequest(ResourceType.Token, null, Method.POST);
            request.AddBody(new {email = emailAddress});
            return request;
        }

        public IRestRequest GetUsers(string filterTerm, int? limit, int? offset, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.User, null, Method.GET, fields);
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

        public IRestRequest GetUser(string id, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.User, "{id}", Method.GET, fields);
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest DeleteUser(string id, bool notify, bool force)
        {
            var request = JsonRequest(ResourceType.User, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddParameter("notify", notify);
            request.AddParameter("force", force);
            return request;
        }

        public IRestRequest MoveFolderToAnotherUser(string currentOwnerId, string folderId, string newOwnerId, bool notify, Field[] fields = null)
        {
            var request = JsonRequest(ResourceType.User, "{userId}/{folderType}/{folderId}", Method.PUT, fields);
            request.AddUrlSegment("userId", currentOwnerId);
            request.AddUrlSegment("folderType", ResourceType.Folder.Description());
            request.AddUrlSegment("folderId", folderId);
            // Notify URL parameter seems to result in a 400..
            // request.AddParameter("notify", notify);
            request.AddBody(new {owned_by = new {id = newOwnerId}});
            return request;
        }

        public IRestRequest GetTicket(string apiKey)
        {
            var restRequest = new RestRequest("1.0/rest");
            restRequest.AddParameter("action", "get_ticket");
            restRequest.AddParameter("api_key", apiKey);
            return restRequest;
        }

        public IRestRequest AuthorizationUrl(string ticket)
        {
            var restRequest = new RestRequest("1.0/auth/{ticket}");
            restRequest.AddUrlSegment("ticket", ticket);
            return restRequest;
        }

        public IRestRequest SwapTicketForToken(string apiKey, string ticket)
        {
            var restRequest = new RestRequest("1.0/rest");
            restRequest.AddParameter("action", "get_auth_token");
            restRequest.AddParameter("api_key", apiKey);
            restRequest.AddParameter("ticket", ticket);
            return restRequest;
        }

        private IRestRequest RawRequest(ResourceType resourceResourceType, string resource, Method method = Method.GET)
        {
            var path = "{version}/{type}" + (string.IsNullOrEmpty(resource) ? string.Empty : string.Format("/{0}", resource));
            var request = new RestRequest(path, method);
            request.AddUrlSegment("version", "2.0");
            request.AddUrlSegment("type", resourceResourceType.Description());
            return request;
        }

        private IRestRequest JsonRequest(ResourceType resourceResourceType, string resource, Method method, Field[] fields = null)
        {
            var request = RawRequest(resourceResourceType, resource, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new AttributableJsonSerializer();
            if (fields != null && fields.Any())
            {
                var fieldList = string.Join(",", fields.Select(f => f.Description()));
                request.AddParameter("fields", fieldList);
            }
            return request;
        }
    }
}