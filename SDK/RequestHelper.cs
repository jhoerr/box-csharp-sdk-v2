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
            var restRequest = new RestRequest("{version}/folders/{id}");
            restRequest.AddUrlSegment("version", _contentApiVersion);
            restRequest.AddUrlSegment("id", id);
            return restRequest;
        }

        public RestRequest CreateFolder(string parentId, string name)
        {
            var restRequest = new RestRequest("{version}/folders/{parentId}", Method.POST) { RequestFormat = DataFormat.Json };
            restRequest.AddUrlSegment("version", _contentApiVersion);
            restRequest.AddUrlSegment("parentId", parentId);
            restRequest.AddBody(new { name });
            return restRequest;
        }

        public RestRequest DeleteFolder(string id, bool recursive)
        {
            var restRequest = new RestRequest("{version}/folders/{id}", Method.DELETE);
            restRequest.AddUrlSegment("version", _contentApiVersion);
            restRequest.AddUrlSegment("id", id);
            restRequest.AddParameter("recursive", recursive.ToString().ToLower());
            return restRequest;
        }

        public RestRequest CopyFolder(string id, string newParentId, string name)
        {
            var restRequest = new RestRequest("{version}/folders/{id}/copy", Method.POST) { RequestFormat = DataFormat.Json };
            restRequest.AddUrlSegment("version", _contentApiVersion);
            restRequest.AddUrlSegment("id", id);
            restRequest.AddBody(new { parent = new { id = newParentId }, name});
            return restRequest;

        }
    }
}