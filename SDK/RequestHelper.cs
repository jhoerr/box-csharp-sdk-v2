using System.IO;
using BoxApi.V2.SDK.Model;
using BoxApi.V2.SDK.Serialization;
using RestSharp;

namespace BoxApi.V2
{
    public class RequestHelper
    {
        private readonly string _authorizationApiVersion;
        private readonly string _contentApiVersion;

        public RequestHelper(string authorizationApiVersion, string contentApiVersion)
        {
            _authorizationApiVersion = authorizationApiVersion;
            _contentApiVersion = contentApiVersion;
        }

        public RestRequest Get(Type resourceType, string id)
        {

            var request = JsonRequest(resourceType, "{id}");
            request.AddUrlSegment("id", id);
            return request;
        }

        public RestRequest GetItems(string id)
        {
            var request = JsonRequest(Type.Folder, "{id}/items");
            request.AddUrlSegment("id", id);
            return request;
        }

        public RestRequest Create(Type resourceType, string parentId, string name)
        {
            var request = JsonRequest(resourceType, "{parentId}", Method.POST);
            request.AddUrlSegment("parentId", parentId);
            request.AddBody(new {name});
            return request;
        }

        public RestRequest Delete(Type resourceType, string id, bool recursive)
        {
            var request = JsonRequest(resourceType, "{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddParameter("recursive", recursive.ToString().ToLower());
            return request;
        }

        public RestRequest Copy(Type resourceType, string id, string newParentId, string name)
        {
            var request = JsonRequest(resourceType, "{id}/copy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new {parent = new {id = newParentId}, name});
            return request;
        }

        public RestRequest ShareLink(Type resourceType, string id, SharedLink sharedLink)
        {
            var request = JsonRequest(resourceType, "{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new {shared_link = sharedLink});
            return request;
        }

        public RestRequest Move(Type resourceType, string id, string newParentId)
        {
            var request = JsonRequest(resourceType, "{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new {parent = new {id = newParentId}});
            return request;
        }

        public RestRequest Rename(Type resourceType, string id, string newName)
        {
            var request = JsonRequest(resourceType, "{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new {name = newName});
            return request;
        }


        private RestRequest JsonRequest(Type resourceType, string resource = null, Method method = Method.GET)
        {
            string path = Path.Combine("{version}/{type}", resource ?? string.Empty);
            var jsonRequest = new RestRequest(path, method)
                {
                    RequestFormat = DataFormat.Json, 
                    JsonSerializer = new AttributableJsonSerializer()
                };
            jsonRequest.AddUrlSegment("version", _contentApiVersion);
            jsonRequest.AddUrlSegment("type", resourceType.Description());
            return jsonRequest;
        }
    }
}