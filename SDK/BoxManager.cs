using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using BoxApi.V2.SDK;
using BoxApi.V2.SDK.Model;
using BoxApi.V2.ServiceReference;
using BoxApi.V2.Statuses;
using RestSharp;
using RestSharp.Deserializers;
using FileInfo = BoxApi.V2.SDK.Model.FileInfo;

namespace BoxApi.V2
{
    /// <summary>
    ///   Provides methods for using Box.NET SOAP web service
    /// </summary>
    public sealed class BoxManager
    {
        private const string ApiVersion2 = "2.0";
        private string _serviceUrl = "https://www.box.com/api/";
        private string _http_Authorization_Header;

        private readonly boxnetService _service;
        private readonly string _apiKey;
        private string _ticket;
        private string _authorizationToken;
        private readonly RestClient _restContentClient;
        private readonly RestClient _restAuthorizationClient;
        private readonly RequestHelper _requestHelper;
        private Entity _rootFolder;
        private IWebProxy _proxy;

        /// <summary>
        ///   Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The unique API key which is assigned to application </param>
        /// <param name="proxy"> Proxy information </param>
        public BoxManager(string applicationApiKey, IWebProxy proxy) :
            this(applicationApiKey, proxy, null)
        {
        }

        /// <summary>
        ///   Instantiates BoxManager
        /// </summary>
        /// <param name="applicationApiKey"> The unique API key which is assigned to application </param>
        /// <param name="proxy"> Proxy information </param>
        /// <param name="authorizationToken"> Valid authorization token </param>
        public BoxManager(
            string applicationApiKey,
            IWebProxy proxy,
            string authorizationToken)
        {
            _apiKey = applicationApiKey;
            _authorizationToken = authorizationToken;
            _proxy = proxy;

            _restAuthorizationClient = new RestClient {Proxy = proxy};
            _restAuthorizationClient.ClearHandlers();
            _restAuthorizationClient.AddHandler("*", new XmlDeserializer());

            _restContentClient = new RestClient(_serviceUrl) {Proxy = proxy, Authenticator = new BoxAuthenticator(applicationApiKey, authorizationToken)};
            _restContentClient.ClearHandlers();
            _restContentClient.AddHandler("*", new JsonDeserializer());

             _requestHelper = new RequestHelper("1.0", "2.0");
        }

        #region V2_Authentication

        public string GetTicket()
        {
            return _ticket ?? (_ticket = BoxXmlAuthRequest("https://www.box.com/api/1.0/rest?action=get_ticket&api_key=" + _apiKey, "ticket"));
        }

        public string GetAuthorizationUrl()
        {
            return "https://www.box.com/api/1.0/auth/" + GetTicket();
        }

        public string GetAuthorizationToken()
        {
            return _authorizationToken ?? (_authorizationToken =
                BoxXmlAuthRequest("https://www.box.com/api/1.0/rest?action=get_auth_token&api_key=" + _apiKey + "&ticket=" + GetTicket(), "auth_token"));
        }

        public string GetAppAuthTokenForUser(string email)
        {
            if (_authorizationToken == null)
            {
                var restRequest = new RestRequest("2.0/tokens", Method.POST) { RequestFormat = DataFormat.Json };
                restRequest.AddHeader("Authorization", "BoxAuth api_key=" + _apiKey);
                restRequest.AddBody(new { email });
                var client = new RestClient(_serviceUrl) { Proxy = _proxy };
                client.ClearHandlers();
                client.AddHandler("*", new JsonDeserializer());
                var restResponse = client.Execute<Token>(restRequest);
                if (!WasSuccessful(restResponse, HttpStatusCode.Created))
                {
                    throw new BoxException(restResponse);
                }
                _authorizationToken = restResponse.Data.BoxToken;
                _rootFolder = restResponse.Data.RootFolder;
            }
            return _authorizationToken;
        }

        private string BoxXmlAuthRequest(string url, string elementName)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            using (var response = req.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                // Console application output  
                var doc = new XmlDocument();
                string resp = reader.ReadToEnd();
                doc.LoadXml(resp);
                var node = doc.GetElementsByTagName(elementName).Item(0);
                return node == null ? null : node.InnerText;
            }
        }

        #endregion

        #region V2_Folders

        public Folder GetFolder(string id)
        {
            var restRequest = _requestHelper.GetFolder(id);
            return Execute<Folder>(restRequest, HttpStatusCode.OK);
        }

        public void GetFolderAsync(string id, Action<Folder> onSuccess, Action onFailure)
        {
            var restRequest = _requestHelper.GetFolder(id);
            ExecuteAsync(restRequest, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public Folder CreateFolder(string parentId, string name)
        {
            var restRequest = _requestHelper.CreateFolder(parentId, name);
            return Execute<Folder>(restRequest, HttpStatusCode.Created);
        }

        public void CreateFolderAsync(string parentId, string name, Action<Folder> onSuccess, Action onFailure)
        {
            var restRequest = _requestHelper.CreateFolder(parentId, name);
            ExecuteAsync(restRequest, onSuccess, onFailure, HttpStatusCode.Created);
        }

        public void DeleteFolder(string id, bool recursive)
        {
            var restRequest = _requestHelper.DeleteFolder(id, recursive);
            Execute(restRequest, HttpStatusCode.OK);
        }

        public void DeleteFolderAsync(string id, bool recursive, Action onSuccess, Action onFailure)
        {
            var restRequest = _requestHelper.DeleteFolder(id, recursive);
            ExecuteAsync(restRequest, onSuccess, onFailure, HttpStatusCode.OK);
        }

        public Folder CopyFolder(string folderId, string newParentId, string newName = null)
        {
            RestRequest request = _requestHelper.CopyFolder(folderId, newParentId, newName);
            return Execute<Folder>(request, HttpStatusCode.Created);
        }

        public void CopyFolderAsync(string folderId, string newParentId, Action<Folder> onSuccess, Action onFailure, string newName = null)
        {
            RestRequest request = _requestHelper.CopyFolder(folderId, newParentId, newName);
            ExecuteAsync(request, onSuccess, onFailure, HttpStatusCode.Created);
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

        private void ExecuteAsync<T>(RestRequest restRequest, Action<T> onSuccess, Action onFailure, HttpStatusCode expectedStatusCode) where T : class, new()
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("onSuccess can not be null");
            }

            _restContentClient.ExecuteAsync<T>(restRequest, response =>
                {
                    if (WasSuccessful(response, expectedStatusCode))
                    {
                        onSuccess(response.Data);
                    }
                    else if (onFailure != null)
                    {
                        onFailure();
                    }
                });
        }

        private void ExecuteAsync(RestRequest restRequest, Action onSuccess, Action onFailure, HttpStatusCode expectedStatusCode)
        {
            if (onSuccess == null)
            {
                throw new ArgumentException("callback can not be null");
            }

            _restContentClient.ExecuteAsync(restRequest, response =>
                {
                    if (WasSuccessful(response, expectedStatusCode))
                    {
                        onSuccess();
                    }
                    else if (onFailure != null)
                    {
                        onFailure();
                    }
                });
        }

        private static bool WasSuccessful(IRestResponse restResponse, HttpStatusCode expectedStatusCode)
        {
            return restResponse != null && restResponse.StatusCode.Equals(expectedStatusCode);
        }

        public void UpdateFolder(int folder_id, string new_name)
        {
            var actionString = "/folders/";
            var url = _serviceUrl + actionString + folder_id.ToString();

            var requestBody = "{ \"name\" : \"" +
                              new_name +
                              "\" }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (Folder));
                    var folder = (Folder) ser.ReadObject(stream);

                    Console.WriteLine(folder.ToString());
                }
            }
        }

        #endregion

        #region V2_Files

        public void GetFileInfo(int file_id, int version)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString();
            if (version > 0)
            {
                url += "?version=" + version.ToString();
            }

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (FileInfo));
                    var fileInfo = (FileInfo) ser.ReadObject(stream);

                    Console.WriteLine("FILE INFO - ");
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        public int CopyFile(int file_id, int parent_id)
        {
            var new_file_id = 0;

            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString() + "/copy";

            var requestBody = "{ \"parent_folder\" : { \"id\": \"" +
                              parent_id.ToString() +
                              "\" } }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (FileInfo));
                    var folderInfo = (FileInfo) ser.ReadObject(stream);
                    new_file_id = folderInfo.id;
                    Console.WriteLine(folderInfo.ToString());
                }
            }

            return new_file_id;
        }

        public void RenameFile(int file_id, string new_name)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString();

            var requestBody = "{ \"name\" : \"" +
                              new_name +
                              "\" }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (FileInfo));
                    var fileInfo = (FileInfo) ser.ReadObject(stream);
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        public void UpdateDescription(int file_id, string new_description)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString();

            var requestBody = "{ \"name\" : { \"description\": \"" +
                              new_description +
                              "\" } }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (FileInfo));
                    var fileInfo = (FileInfo) ser.ReadObject(stream);
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        public void DeleteFile(int file_id, int version)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString();

            if (version > 0)
            {
                url += "/versions/" + version.ToString();
            }

            using (var stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        public void GetFileData(int file_id, int version)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString();
            if (version > 0)
            {
                url += "/versions/" + version.ToString();
            }
            else
            {
                url += "/data";
            }

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    var sr = new StreamReader(stream);

                    Console.WriteLine("FILE DATA - ");
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
        }

        public int CreateFile(string data)
        {
            var new_file_id = 0;

            var actionString = "/files/data";
            var url = _serviceUrl + actionString;

            var requestData = Encoding.ASCII.GetBytes(data);

            using (var stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (FileInfo));
                    var fileInfo = (FileInfo) ser.ReadObject(stream);
                    new_file_id = fileInfo.id;
                    Console.WriteLine(fileInfo.ToString());
                }
            }

            return new_file_id;
        }

        public void UploadFile(int file_id, string data)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString() + "/data";

            var requestData = Encoding.ASCII.GetBytes(data);

            using (var stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (FileInfo));
                    var fileInfo = (FileInfo) ser.ReadObject(stream);
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        #region V2_Comments

        #endregion

        public void PostComment(int file_id, string comment)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString() + "/comments";

            var requestBody = "{ \"comment\" : { \"message\": \"" +
                              comment +
                              "\" } }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
            }
        }

        public void GetFileComments(int file_id)
        {
            var actionString = "/files/";
            var url = _serviceUrl + actionString + file_id.ToString() + "/comments";

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (Comment[]));
                    var comments = (Comment[]) ser.ReadObject(stream);

                    foreach (Comment comment in comments)
                    {
                        Console.WriteLine(comment.ToString());
                    }
                }
            }
        }

        public void GetComment(int comment_id)
        {
            var actionString = "/comments/";
            var url = _serviceUrl + actionString + comment_id.ToString();

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (Comment));
                    var comment = (Comment) ser.ReadObject(stream);

                    if (comment != null)
                    {
                        Console.WriteLine(comment.ToString());
                    }
                }
            }
        }

        public void DeleteComment(int comment_id)
        {
            var actionString = "/comments/";
            var url = _serviceUrl + actionString + comment_id.ToString();

            using (var stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        public void UpdateComment(int comment_id, string new_message)
        {
            var actionString = "/comments/";
            var url = _serviceUrl + actionString + comment_id.ToString();

            var requestBody = "{ \"message\" : \"" +
                              new_message +
                              "\" }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
            }
        }

        #region V2 Discussions API

        #endregion

        public void GetDiscussions(int folder_id)
        {
            var actionString = "/folders/";
            var url = _serviceUrl + actionString + folder_id.ToString() + "/discussions";

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (Entity[]));
                    var discussions = (Entity[]) ser.ReadObject(stream);

                    foreach (Entity disc in discussions)
                    {
                        Console.WriteLine(disc.ToString());
                    }
                }
            }
        }

        public void DeleteDiscussion(int disc_id)
        {
            var actionString = "/discussions/";
            var url = _serviceUrl + actionString + disc_id.ToString();

            using (var stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        public void GetDiscussionComments(int disc_id)
        {
            var actionString = "/discussions/";
            var url = _serviceUrl + actionString + disc_id.ToString() + "/comments";

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (Comment[]));
                    var comments = (Comment[]) ser.ReadObject(stream);

                    foreach (Comment comment in comments)
                    {
                        Console.WriteLine(comment.ToString());
                    }
                }
            }
        }

        public void PostCommentInDiscussion(int disc_id, string comment)
        {
            var actionString = "/discussions/";
            var url = _serviceUrl + actionString + disc_id.ToString() + "/comments";

            var requestBody = "{ \"comment\" : { \"message\": \"" +
                              comment +
                              "\" } }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
            }
        }

        public void UpdateDiscussionName(int disc_id, string new_name)
        {
            var actionString = "/discussions/";
            var url = _serviceUrl + actionString + disc_id.ToString();

            var requestBody = "{ \"name\" : \"" +
                              new_name +
                              "\" }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
            }
        }

        #region V2 Events API

        #endregion

        public void GetEvents()
        {
            var actionString = "/events/";
            var url = _serviceUrl + actionString;

            using (var stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                // Parse event info from stream
            }
        }


        #endregion
    }

    public class BoxException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; }

        public BoxException(IRestResponse restResponse)
        {
            StatusCode = restResponse.StatusCode;
            Message = restResponse.StatusDescription;
        }
    }
}