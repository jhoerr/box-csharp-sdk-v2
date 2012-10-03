using System.Collections.Generic;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Serialization;
using RestSharp;

namespace BoxApi.V2
{
    public class RequestHelper
    {
        public IRestRequest Get(ResourceType resourceResourceType, string id)
        {
            var request = JsonRequest(resourceResourceType, "{id}");
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest GetItems(string id, Field[] fields)
        {
            var request = JsonRequest(ResourceType.Folder, "{id}/items");
            request.AddUrlSegment("id", id);
            if (fields != null && fields.Any())
            {
                var fieldList = string.Join(",", fields.Select(f => f.Description()));
                request.AddParameter("fields", fieldList);
            }
            return request;
        }

        public IRestRequest GetDiscussions(string folderId)
        {
            var request = JsonRequest(ResourceType.Folder, "{id}/discussions");
            request.AddUrlSegment("id", folderId);
            return request;
        }

        public IRestRequest CreateFolder(string parentId, string name)
        {
            var request = JsonRequest(ResourceType.Folder, null, Method.POST);
            request.AddBody(new {name, parent = new {id = parentId}});
            return request;
        }

        public IRestRequest CreateFile(string parentId, string name, byte[] content)
        {
            var request = JsonRequest(ResourceType.File, "data", Method.POST);
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

        public IRestRequest Copy(ResourceType resourceResourceType, string id, string newParentId, string name)
        {
            var request = JsonRequest(resourceResourceType, "{id}/copy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new {parent = new {id = newParentId}, name});
            return request;
        }

        public IRestRequest Update(ResourceType resourceResourceType, string id, string parentId = null, string name = null, string description = null, SharedLink sharedLink = null,
                                   string message = null)
        {
            var request = JsonRequest(resourceResourceType, "{id}", Method.PUT);
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
            var request = RawRequest(ResourceType.File, "{id}/data");
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest Write(string id, string name, string etag, byte[] content)
        {
            var request = JsonRequest(ResourceType.File, "{id}/data", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddHeader("If-Match", etag ?? string.Empty);
            request.AddFile("filename", content, name);

            return request;
        }

        public IRestRequest CreateComment(ResourceType resourceType, string id, string message)
        {
            var request = JsonRequest(resourceType, "{id}/comments", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new {message});
            return request;
        }

        public IRestRequest GetComments(ResourceType resourceResourceType, string id)
        {
            var request = JsonRequest(resourceResourceType, "{id}/comments");
            request.AddUrlSegment("id", id);
            return request;
        }

        public IRestRequest CreateDiscussion(string parentId, string name, string description)
        {
            var request = JsonRequest(ResourceType.Discussion, null, Method.POST);
            request.AddBody(new { parent = new { id = parentId }, name, description });
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

        private IRestRequest JsonRequest(ResourceType resourceResourceType, string resource = null, Method method = Method.GET)
        {
            var request = RawRequest(resourceResourceType, resource, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new AttributableJsonSerializer();
            return request;
        }
    }
}