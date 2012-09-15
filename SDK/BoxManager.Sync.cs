using System.Net;
using BoxApi.V2.SDK.Model;
using RestSharp;

namespace BoxApi.V2.SDK
{
    public partial class BoxManager
    {
        public Folder GetFolder(string id)
        {
            var restRequest = _requestHelper.GetFolder(id);
            return Execute<Folder>(restRequest, HttpStatusCode.OK);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            var restRequest = _requestHelper.CreateFolder(parentId, name);
            return Execute<Folder>(restRequest, HttpStatusCode.Created);
        }

        public void DeleteFolder(string id, bool recursive)
        {
            var restRequest = _requestHelper.DeleteFolder(id, recursive);
            Execute(restRequest, HttpStatusCode.OK);
        }

        public Folder CopyFolder(string folderId, string newParentId, string newName = null)
        {
            RestRequest request = _requestHelper.CopyFolder(folderId, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        private void Execute(RestRequest restRequest, HttpStatusCode expectedStatusCode)
        {
            var restResponse = _restContentClient.Execute(restRequest);
            if (!WasSuccessful(restResponse, expectedStatusCode))
            {
                throw new BoxException(restResponse);
            }
        }

        private T Execute<T>(IRestRequest restRequest, HttpStatusCode expectedStatusCode) where T : class, new()
        {
            var restResponse = _restContentClient.Execute<T>(restRequest);
            if (!WasSuccessful(restResponse, expectedStatusCode))
            {
                throw new BoxException(restResponse);
            }
            return restResponse.Data;
        }

    }
}