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

        public RestRequest GetFolder(string id)
        {
            var request = JsonRequest("folders/{id}");
            request.AddUrlSegment("id", id);
            return request;
        }

        public RestRequest GetFolderItems(string id)
        {
            var request = JsonRequest("folders/{id}/items");
            request.AddUrlSegment("id", id);
            return request;
        }

        public RestRequest CreateFolder(string parentId, string name)
        {
            var request = JsonRequest("folders/{parentId}", Method.POST);
            request.AddUrlSegment("parentId", parentId);
            request.AddBody(new {name});
            return request;
        }

        public RestRequest DeleteFolder(string id, bool recursive)
        {
            var request = JsonRequest("folders/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.AddParameter("recursive", recursive.ToString().ToLower());
            return request;
        }

        public RestRequest CopyFolder(string id, string newParentId, string name)
        {
            var request = JsonRequest("folders/{id}/copy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddBody(new {parent = new {id = newParentId}, name});
            return request;
        }

        public RestRequest ShareFolderLink(string id, SharedLink sharedLink)
        {
            var request = JsonRequest("folders/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new {shared_link = sharedLink});
            return request;
        }

        public RestRequest MoveFolder(string id, string newParentId)
        {
            var request = JsonRequest("folders/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new {parent = new {id = newParentId}});
            return request;
        }

        public RestRequest RenameFolder(string id, string newName)
        {
            var request = JsonRequest("folders/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(new {name = newName});
            return request;
        }


        private RestRequest JsonRequest(string resource, Method method = Method.GET)
        {
            var jsonRequest = new RestRequest("{version}/" + resource, method)
                {
                    RequestFormat = DataFormat.Json, 
                    JsonSerializer = new AttributableJsonSerializer()
                };
            jsonRequest.AddUrlSegment("version", _contentApiVersion);
            return jsonRequest;
        }
    }
}