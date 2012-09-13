using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using BoxApi.V2.SDK;
using BoxApi.V2.SDK.Model;
using BoxApi.V2.ServiceReference;
using BoxApi.V2.Statuses;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
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
        private string _token;
        private readonly RestClient _restContentClient;
        private readonly RestClient _restAuthorizationClient;

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
            _restAuthorizationClient = new RestClient {Proxy = proxy};
            _restAuthorizationClient.ClearHandlers();
            _restAuthorizationClient.AddHandler("*", new XmlDeserializer());

            _restContentClient = new RestClient(_serviceUrl) {Proxy = proxy, Authenticator = new BoxAuthenticator(applicationApiKey, authorizationToken)};
            _restContentClient.ClearHandlers();
            _restContentClient.AddHandler("*", new JsonDeserializer());
        }

        #region GetAuthenticationToken

        /// <summary>
        ///   Gets authentication token required for communication between Box.NET service and user's application.
        ///   Method has to be called after the user has authorized themself on Box.NET site
        /// </summary>
        /// <param name="authenticationTicket"> Athentication ticket </param>
        /// <param name="authenticationToken"> Authentication token </param>
        /// <param name="authenticatedUser"> Authenticated user account information </param>
        /// <returns> Operation result </returns>
        public GetAuthenticationTokenStatus GetAuthenticationToken(
            string authenticationTicket,
            out string authenticationToken,
            out User authenticatedUser)
        {
            SOAPUser user;

            var result = _service.get_auth_token(_apiKey, authenticationTicket, out authenticationToken, out user);

            authenticatedUser = user == null ? null : new User(user);
            _token = authenticationToken;

            return StatusMessageParser.ParseGetAuthenticationTokenStatus(result);
        }

        /// <summary>
        ///   Gets authentication token required for communication between Box.NET service and user's application.
        ///   Method has to be called after the user has authorized themself on Box.NET site
        /// </summary>
        /// <param name="authenticationTicket"> Athentication ticket </param>
        /// <param name="getAuthenticationTokenCompleted"> Call back method which will be invoked when operation completes </param>
        /// <exception cref="ArgumentNullException">Thrown if
        ///   <paramref name="getAuthenticationTokenCompleted" />
        ///   is null</exception>
        public void GetAuthenticationToken(
            string authenticationTicket,
            OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted)
        {
            GetAuthenticationToken(authenticationTicket, getAuthenticationTokenCompleted, null);
        }

        /// <summary>
        ///   Gets authentication token required for communication between Box.NET service and user's application.
        ///   Method has to be called after the user has authorized themself on Box.NET site
        /// </summary>
        /// <param name="authenticationTicket"> Athentication ticket </param>
        /// <param name="getAuthenticationTokenCompleted"> Callback method which will be invoked when operation completes </param>
        /// <param name="userState"> A user-defined object containing state information. This object is passed to the <paramref
        ///    name="getAuthenticationTokenCompleted" /> delegate as a part of response when the operation is completed </param>
        /// <exception cref="ArgumentNullException">Thrown if
        ///   <paramref name="getAuthenticationTokenCompleted" />
        ///   is null</exception>
        public void GetAuthenticationToken(
            string authenticationTicket,
            OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted,
            object userState)
        {
            // ThrowIfParameterIsNull(getAuthenticationTokenCompleted, "getAuthenticationTokenCompleted");

            _service.get_auth_tokenCompleted += GetAuthenticationTokenFinished;

            object[] state = {getAuthenticationTokenCompleted, userState};

            _service.get_auth_tokenAsync(_apiKey, authenticationTicket, state);
        }

        private void GetAuthenticationTokenFinished(object sender, get_auth_tokenCompletedEventArgs e)
        {
            var state = (object[]) e.UserState;
            Exception error = null;

            var getAuthenticationTokenCompleted =
                (OperationFinished<GetAuthenticationTokenResponse>) state[0];

            GetAuthenticationTokenStatus status;

            if (e.Error == null)
            {
                status = StatusMessageParser.ParseGetAuthenticationTokenStatus(e.Result);

                if (status == GetAuthenticationTokenStatus.Unknown)
                {
                    error = new Exception(e.Result);
                }
            }
            else
            {
                status = GetAuthenticationTokenStatus.Failed;
                error = e.Error;
            }

            var response = new GetAuthenticationTokenResponse
                {
                    Status = status,
                    UserState = state[1],
                    Error = error,
                    AuthenticationToken = string.Empty
                };

            if (response.Status == GetAuthenticationTokenStatus.Successful)
            {
                var authenticatedUser = new User(e.user);
                response.AuthenticatedUser = authenticatedUser;
                response.AuthenticationToken = e.auth_token;
                _token = e.auth_token;

                // Set HTTP auth header
                _http_Authorization_Header = "BoxAuth api_key=" + _apiKey + "&auth_token=" + _token;
            }

            getAuthenticationTokenCompleted(response);
        }

        #endregion

        #region GetTicket

        /// <summary>
        ///   Gets ticket which is used to generate an authentication page 
        ///   for the user to login
        /// </summary>
        /// <param name="authenticationTicket"> Authentication ticket </param>
        /// <returns> Operation status </returns>
        public GetTicketStatus GetTicket(out string authenticationTicket)
        {
            var result = _service.get_ticket(_apiKey, out authenticationTicket);

            return StatusMessageParser.ParseGetTicketStatus(result);
        }

        /// <summary>
        ///   Gets ticket which is used to generate an authentication page 
        ///   for the user to login
        /// </summary>
        /// <param name="getAuthenticationTicketCompleted"> Call back method which will be invoked when operation completes </param>
        /// <exception cref="ArgumentException">Thrown if
        ///   <paramref name="getAuthenticationTicketCompleted" />
        ///   is
        ///   <c>null</c>
        /// </exception>
        public void GetTicket(OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted)
        {
            GetTicket(getAuthenticationTicketCompleted, null);
        }

        /// <summary>
        ///   Gets ticket which is used to generate an authentication page 
        ///   for the user to login
        /// </summary>
        /// <param name="getAuthenticationTicketCompleted"> Call back method which will be invoked when operation completes </param>
        /// <param name="userState"> A user-defined object containing state information. This object is passed to the <paramref
        ///    name="getAuthenticationTicketCompleted" /> delegate as a part of response when the operation is completed </param>
        /// <exception cref="ArgumentException">Thrown if
        ///   <paramref name="getAuthenticationTicketCompleted" />
        ///   is
        ///   <c>null</c>
        /// </exception>
        public void GetTicket(
            OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted,
            object userState)
        {
            // ThrowIfParameterIsNull(getAuthenticationTicketCompleted, "getAuthenticationTicketCompleted");

            object[] data = {getAuthenticationTicketCompleted, userState};

            _service.get_ticketCompleted += GetTicketFinished;

            _service.get_ticketAsync(_apiKey, data);
        }


        private void GetTicketFinished(object sender, get_ticketCompletedEventArgs e)
        {
            var data = (object[]) e.UserState;
            var getAuthenticationTicketCompleted = (OperationFinished<GetTicketResponse>) data[0];
            GetTicketResponse response;

            if (e.Error != null)
            {
                response = new GetTicketResponse
                    {
                        Status = GetTicketStatus.Failed,
                        UserState = data[1],
                        Error = e.Error
                    };
            }
            else
            {
                var status = StatusMessageParser.ParseGetTicketStatus(e.Result);

                var error = status == GetTicketStatus.Unknown
                                ? new Exception(e.Result)
                                : null;

                response = new GetTicketResponse
                    {
                        Status = status,
                        Ticket = e.ticket,
                        UserState = data[1],
                        Error = error
                    };
            }

            getAuthenticationTicketCompleted(response);
        }

        #endregion

        #region V2_Folders

        public Folder GetFolder(string id)
        {
            var restRequest = GetFolderRequest(id);
            var restResponse = _restContentClient.Execute<Folder>(restRequest);
            return restResponse.Data;
        }

        public void GetFolderAsync(string id, Action<Folder> callback)
        {
            var restRequest = GetFolderRequest(id);
            _restContentClient.ExecuteAsync<Folder>(restRequest, response => callback(response.Data));
        }

        private RestRequest GetFolderRequest(string id)
        {
            var restRequest = new RestRequest("{version}/folders/{id}");
            restRequest.AddUrlSegment("version", ApiVersion2);
            restRequest.AddUrlSegment("id", id);
            return restRequest;
        }

        public Folder CreateFolder(string parentId, string name)
        {
            var restRequest = CreateFolderRequest(parentId, name);
            var restResponse = _restContentClient.Execute<Folder>(restRequest);
            return restResponse.Data;
        }

        public void CreateFolderAsync(string parentId, string name, Action<Folder> callback)
        {
            var restRequest = CreateFolderRequest(parentId, name);
            _restContentClient.ExecuteAsync<Folder>(restRequest, response => callback(response.Data));
        }

        private RestRequest CreateFolderRequest(string parentId, string name)
        {
            var restRequest = new RestRequest("{version}/folders/{parentId}", Method.POST){RequestFormat = DataFormat.Json};
            restRequest.AddUrlSegment("version", ApiVersion2);
            restRequest.AddUrlSegment("parentId", parentId);
            restRequest.AddBody(new { name });
            return restRequest;
        }

        private class NameBody
        {
            public NameBody(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

        public int CreateFolder(int parent_folder_id, string name)
        {
            var new_folder_id = 0;

            var actionString = "/folders/";
            var url = _serviceUrl + actionString + parent_folder_id.ToString();

            var requestBody = "{ \"name\" : \"" +
                              name +
                              "\" }";
            var requestData = Encoding.ASCII.GetBytes(requestBody);

            using (var stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    var ser = new DataContractJsonSerializer(typeof (Folder));
                    var folder = (Folder) ser.ReadObject(stream);
                    new_folder_id = int.Parse(folder.Id);

                    Console.WriteLine(folder.ToString());
                }
            }

            return new_folder_id;
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

        public void DeleteFolder(int folder_id)
        {
            var actionString = "/folders/";
            var url = _serviceUrl + actionString + folder_id.ToString();

            using (var stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
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

        #endregion

        #region V2_Comments

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

        #endregion

        #region V2 Discussions API

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

        #endregion

        #region V2 Events API

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
}