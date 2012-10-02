using System.Collections.Generic;
using System.Linq;
using BoxApi.V2.Model;
using BoxApi.V2.Serialization;
using RestSharp;
using RestSharp.Deserializers;

namespace BoxApi.V2
{
    public class RequestHelper
    {
        public const string JsonMimeType = "application/json";

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

        public IRestRequest CreateFolder(string parentId, string name)
        {
            var request = JsonRequest(ResourceType.Folder, "{parentId}", Method.POST);
            request.AddUrlSegment("parentId", parentId);
            request.AddBody(new {name});
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

        public IRestRequest Update(ResourceType resourceResourceType, string id, string parentId = null, string name = null, string description = null, SharedLink sharedLink = null, string message = null)
        {
            var request = JsonRequest(resourceResourceType, "{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            var body = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(parentId))
            {
                body.Add("parent", new{id = parentId});
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

        public IRestRequest Write(string id, string name, byte[] content)
        {
            var request = JsonRequest(ResourceType.File, "{id}/data", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddFile("filename", content, name);
            return request;
        }

        public IRestRequest AddComment(string id, string message)
        {
            var request = JsonRequest(ResourceType.File, "{id}/comments", Method.POST);
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
            string path = "{version}/{type}" + (string.IsNullOrEmpty(resource) ? string.Empty : string.Format("/{0}", resource));
            var request = new RestRequest(path, method);
            request.AddUrlSegment("version", "2.0");
            request.AddUrlSegment("type", resourceResourceType.Description());
            return request;
        }

        public bool WasSuccessful(IRestResponse restResponse, out Error error)
        {
            error = null;
            bool success = true;
            if (restResponse == null)
            {
                success = false;
            }

            else if (restResponse.ContentType.Equals(JsonMimeType) && restResponse.Content.Contains(@"""type"":""error"""))
            {
                success = false;
                var jsonDeserializer = new JsonDeserializer();
                error = jsonDeserializer.Deserialize<Error>(restResponse);
                if (error.Type == null)
                {
                    var errorCollection = jsonDeserializer.Deserialize<ErrorCollection>(restResponse);
                    if (!string.IsNullOrEmpty(errorCollection.TotalCount))
                    {
                        error = errorCollection.Entries.First();
                    }
                }
            }
            return success;
 
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