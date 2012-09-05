using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using BoxApi.V2.ServiceReference;
using BoxApi.V2.Statuses;

using File = BoxApi.V2.File;

namespace BoxApi.V2
{
	/// <summary>
	/// Provides methods for using Box.NET SOAP web service
	/// </summary>
	public sealed partial class BoxManager
	{
		private string _serviceUrl = "https://www.box.com/api/2.0";
        private string _authServiceUrl = "http://box.net/api/soap";
        private string _http_Authorization_Header;

		private readonly boxnetService _service;
		private readonly string _apiKey;
		private string _token;

		/// <summary>
		/// Instantiates BoxManager
		/// </summary>
		/// <param name="applicationApiKey">The unique API key which is assigned to application</param>
		/// <param name="serviceUrl">Box.NET SOAP service Url</param>
		/// <param name="proxy">Proxy information</param>
		public BoxManager(string applicationApiKey, IWebProxy proxy) :
			this(applicationApiKey, proxy, null)
		{
		}

		/// <summary>
		/// Instantiates BoxManager
		/// </summary>
		/// <param name="applicationApiKey">The unique API key which is assigned to application</param>
		/// <param name="serviceUrl">Box.NET SOAP service Url</param>
		/// <param name="proxy">Proxy information</param>
		/// <param name="authorizationToken">Valid authorization token</param>
		public BoxManager(
			string applicationApiKey, 
			IWebProxy proxy, 
			string authorizationToken)
		{
			_apiKey = applicationApiKey;
			
			_service = new boxnetService();

            _service.Url = _authServiceUrl;
			_service.Proxy = proxy;

			_token = authorizationToken;

            // Set HTTP auth header
            _http_Authorization_Header = "BoxAuth api_key=" + _apiKey + "&auth_token=" + _token;
		}


		/// <summary>
		/// Gets or sets authentication token required for communication 
		/// between Box.NET service and user's application
		/// </summary>
		public string AuthenticationToken
		{
			get
			{
				return _token;
			}
			set
			{
				_token = value;
			}
		}
        
		#region AuthenticateUser

		/// <summary>
		/// Authenticates user
		/// </summary>
		/// <param name="login">Account login</param>
		/// <param name="password">Account password</param>
		/// <param name="method"></param>
		/// <param name="authenticationToken">Authentication token</param>
		/// <param name="authenticatedUser">Authenticated user information</param>
		/// <returns>Operation result</returns>
		/// <exception cref="NotSupportedException">The exception is thrown every time you call the method</exception>
		[Obsolete("This method is no longer supported by Box.NET API")]
		public AuthenticationStatus AuthenticateUser(
			string login, 
			string password, 
			string method, 
			out string authenticationToken, 
			out User authenticatedUser)
		{
			throw new NotSupportedException("This method is no longer supported by Box.NET API");
		}

		/// <summary>
		/// Authenticates user
		/// </summary>
		/// <param name="login">Account login</param>
		/// <param name="password">Account password</param>
		/// <param name="method"></param>
		/// <returns>Operation result</returns>
		/// <exception cref="NotSupportedException">The exception is thrown every time you call the method</exception>
		[Obsolete("This method is no longer supported by Box.NET API")]
		public AuthenticateUserResponse AuthenticateUser(
			string login,
			string password,
			string method)
		{
			throw new NotSupportedException("This method is no longer supported by Box.NET API");
		}

		/// <summary>
		/// Authenticates user
		/// </summary>
		/// <param name="login">Account login</param>
		/// <param name="password">Account password</param>
		/// <param name="method"></param>
		/// <param name="authenticateUserCompleted">Callback method which will be invoked when operation completes</param>
		/// <exception cref="NotSupportedException">The exception is thrown every time you call the method</exception>
		[Obsolete("This method is no longer supported by Box.NET API")]
		public void AuthenticateUser(
			string login,
			string password,
			string method,
			OperationFinished<AuthenticateUserResponse> authenticateUserCompleted)
		{
			AuthenticateUser(login, password, method, authenticateUserCompleted, null);
		}

		/// <summary>
		/// Authenticates user
		/// </summary>
		/// <param name="login">Account login</param>
		/// <param name="password">Account password</param>
		/// <param name="method"></param>
		/// <param name="authenticateUserCompleted">Callback method which will be invoked when operation completes</param>
		/// <param name="userState">A user-defined object containing state information. 
		/// This object is passed to the <paramref name="authenticateUserCompleted"/> delegate as a part of response when the operation is completed</param>
		/// <exception cref="NotSupportedException">The exception is thrown every time you call the method</exception>
		[Obsolete("This method is no longer supported by Box.NET API")]
		public void AuthenticateUser(
			string login,
			string password,
			string method,
			OperationFinished<AuthenticateUserResponse> authenticateUserCompleted,
			object userState)
		{
			throw new NotSupportedException("This method is no longer supported by Box.NET API");
		}

		#endregion

		#region GetAuthenticationToken

		/// <summary>
		/// Gets authentication token required for communication between Box.NET service and user's application.
		/// Method has to be called after the user has authorized themself on Box.NET site
		/// </summary>
		/// <param name="authenticationTicket">Athentication ticket</param>
		/// <param name="authenticationToken">Authentication token</param>
		/// <param name="authenticatedUser">Authenticated user account information</param>
		/// <returns>Operation result</returns>
		public GetAuthenticationTokenStatus GetAuthenticationToken(
			string authenticationTicket, 
			out string authenticationToken, 
			out User authenticatedUser)
		{
			SOAPUser user;

			string result = _service.get_auth_token(_apiKey, authenticationTicket, out authenticationToken, out user);

			authenticatedUser = user == null ? null : new User(user);
			_token = authenticationToken;

			return StatusMessageParser.ParseGetAuthenticationTokenStatus(result);
		}

		/// <summary>
		/// Gets authentication token required for communication between Box.NET service and user's application.
		/// Method has to be called after the user has authorized themself on Box.NET site
		/// </summary>
		/// <param name="authenticationTicket">Athentication ticket</param>
		/// <param name="getAuthenticationTokenCompleted">Call back method which will be invoked when operation completes</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="getAuthenticationTokenCompleted"/> is null</exception>
		public void GetAuthenticationToken(
			string authenticationTicket, 
			OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted)
		{
			GetAuthenticationToken(authenticationTicket, getAuthenticationTokenCompleted, null);
		}

		/// <summary>
		/// Gets authentication token required for communication between Box.NET service and user's application.
		/// Method has to be called after the user has authorized themself on Box.NET site
		/// </summary>
		/// <param name="authenticationTicket">Athentication ticket</param>
		/// <param name="getAuthenticationTokenCompleted">Callback method which will be invoked when operation completes</param>
		/// <param name="userState">A user-defined object containing state information. 
		/// This object is passed to the <paramref name="getAuthenticationTokenCompleted"/> delegate as a part of response when the operation is completed</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="getAuthenticationTokenCompleted"/> is null</exception>
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
			object[] state = (object[]) e.UserState;
			Exception error = null;

			OperationFinished<GetAuthenticationTokenResponse> getAuthenticationTokenCompleted =
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

			GetAuthenticationTokenResponse response = new GetAuthenticationTokenResponse
			                                          	{
			                                          		Status = status,
			                                          		UserState = state[1],
			                                          		Error = error,
															AuthenticationToken = string.Empty
			                                          	};

			if (response.Status == GetAuthenticationTokenStatus.Successful)
			{
				User authenticatedUser = new User(e.user);
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
		/// Gets ticket which is used to generate an authentication page 
		/// for the user to login
		/// </summary>
		/// <param name="authenticationTicket">Authentication ticket</param>
		/// <returns>Operation status</returns>
		public GetTicketStatus GetTicket(out string authenticationTicket)
		{
			string result = _service.get_ticket(_apiKey, out authenticationTicket);

			return StatusMessageParser.ParseGetTicketStatus(result);
		}

		/// <summary>
		/// Gets ticket which is used to generate an authentication page 
		/// for the user to login
		/// </summary>
		/// <param name="getAuthenticationTicketCompleted">Call back method which will be invoked when operation completes</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="getAuthenticationTicketCompleted"/> is <c>null</c></exception>
		public void GetTicket(OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted)
		{
			GetTicket(getAuthenticationTicketCompleted, null);
		}

		/// <summary>
		/// Gets ticket which is used to generate an authentication page 
		/// for the user to login
		/// </summary>
		/// <param name="getAuthenticationTicketCompleted">Call back method which will be invoked when operation completes</param>
		/// <param name="userState">A user-defined object containing state information. 
		/// This object is passed to the <paramref name="getAuthenticationTicketCompleted"/> delegate as a part of response when the operation is completed</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="getAuthenticationTicketCompleted"/> is <c>null</c></exception>
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
			object[] data = (object[]) e.UserState;
			OperationFinished<GetTicketResponse> getAuthenticationTicketCompleted = (OperationFinished<GetTicketResponse>)data[0];
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
				GetTicketStatus status = StatusMessageParser.ParseGetTicketStatus(e.Result);

				Exception error = status == GetTicketStatus.Unknown
				                  	?
				                  		new Exception(e.Result)
				                  	:
				                  		null;

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

        public void GetFolderInfo(int folder_id)
        {
            string actionString = "/folders/";
            string url = _serviceUrl + actionString + folder_id.ToString();

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFolderInfo));
                    JSONFolderInfo folderInfo = (JSONFolderInfo)ser.ReadObject(stream);

                    Console.WriteLine(folderInfo.ToString());
                }
            }
        }

        public int CreateFolder(int parent_folder_id, string name)
        {
            int new_folder_id = 0;

            string actionString = "/folders/";
            string url = _serviceUrl + actionString + parent_folder_id.ToString();

            string requestBody = "{ \"name\" : \"" +
                                    name +
                                    "\" }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFolderInfo));
                    JSONFolderInfo folderInfo = (JSONFolderInfo)ser.ReadObject(stream);
                    new_folder_id = folderInfo.id;

                    Console.WriteLine(folderInfo.ToString());
                }
            }

            return new_folder_id;
        }

        public void UpdateFolder(int folder_id, string new_name)
        {
            string actionString = "/folders/";
            string url = _serviceUrl + actionString + folder_id.ToString();

            string requestBody = "{ \"name\" : \"" +
                                    new_name +
                                    "\" }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFolderInfo));
                    JSONFolderInfo folderInfo = (JSONFolderInfo)ser.ReadObject(stream);

                    Console.WriteLine(folderInfo.ToString());
                }
            }
        }

        public void DeleteFolder(int folder_id)
        {
            string actionString = "/folders/";
            string url = _serviceUrl + actionString + folder_id.ToString();

            using (Stream stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        #endregion

        #region V2_Files

        public void GetFileInfo(int file_id, int version)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString();
            if (version > 0)
            {
                url += "?version=" + version.ToString();
            }

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFileInfo));
                    JSONFileInfo fileInfo = (JSONFileInfo)ser.ReadObject(stream);

                    Console.WriteLine("FILE INFO - ");
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        public int CopyFile(int file_id, int parent_id)
        {
            int new_file_id = 0;

            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString() + "/copy";

            string requestBody = "{ \"parent_folder\" : { \"id\": \"" +
                                    parent_id.ToString() +
                                    "\" } }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFileInfo));
                    JSONFileInfo folderInfo = (JSONFileInfo)ser.ReadObject(stream);
                    new_file_id = folderInfo.id;
                    Console.WriteLine(folderInfo.ToString());
                }
            }

            return new_file_id;
        }

        public void RenameFile(int file_id, string new_name)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString();

            string requestBody = "{ \"name\" : \"" +
                                    new_name +
                                    "\" }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFileInfo));
                    JSONFileInfo fileInfo = (JSONFileInfo)ser.ReadObject(stream);
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        public void UpdateDescription(int file_id, string new_description)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString();

            string requestBody = "{ \"name\" : { \"description\": \"" +
                                    new_description +
                                    "\" } }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFileInfo));
                    JSONFileInfo fileInfo = (JSONFileInfo)ser.ReadObject(stream);
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        public void DeleteFile(int file_id, int version)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString();

            if (version > 0)
            {
                url += "/versions/" + version.ToString();
            }

            using (Stream stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        public void GetFileData(int file_id, int version)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString();
            if (version > 0)
            {
                url += "/versions/" + version.ToString();
            }
            else
            {
                url += "/data";
            }

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    StreamReader sr = new StreamReader(stream);

                    Console.WriteLine("FILE DATA - ");
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
        }

        public int CreateFile(string data)
        {
            int new_file_id = 0;

            string actionString = "/files/data";
            string url = _serviceUrl + actionString;

            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(data.ToString());

            using (Stream stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFileInfo));
                    JSONFileInfo fileInfo = (JSONFileInfo)ser.ReadObject(stream);
                    new_file_id = fileInfo.id;
                    Console.WriteLine(fileInfo.ToString());
                }
            }

            return new_file_id;
        }

        public void UploadFile(int file_id, string data)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString() + "/data";

            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(data.ToString());

            using (Stream stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONFileInfo));
                    JSONFileInfo fileInfo = (JSONFileInfo)ser.ReadObject(stream);
                    Console.WriteLine(fileInfo.ToString());
                }
            }
        }

        #endregion

        #region V2_Comments

        public void PostComment(int file_id, string comment)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString() + "/comments";

            string requestBody = "{ \"comment\" : { \"message\": \"" +
                                    comment +
                                    "\" } }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
            }

        }

        public void GetFileComments(int file_id)
        {
            string actionString = "/files/";
            string url = _serviceUrl + actionString + file_id.ToString() + "/comments";

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONComment[]));
                    JSONComment[] comments = (JSONComment[])ser.ReadObject(stream);

                    foreach (JSONComment comment in comments)
                    {
                        Console.WriteLine(comment.ToString());
                    }
                }
            }
        }

        public void GetComment(int comment_id)
        {
            string actionString = "/comments/";
            string url = _serviceUrl + actionString + comment_id.ToString();

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONComment));
                    JSONComment comment = (JSONComment)ser.ReadObject(stream);

                    if (comment != null)
                    {
                        Console.WriteLine(comment.ToString());
                    }
                }
            }
        }

        public void DeleteComment(int comment_id)
        {
            string actionString = "/comments/";
            string url = _serviceUrl + actionString + comment_id.ToString();

            using (Stream stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        public void UpdateComment(int comment_id, string new_message)
        {
            string actionString = "/comments/";
            string url = _serviceUrl + actionString + comment_id.ToString();

            string requestBody = "{ \"message\" : \"" +
                                    new_message +
                                    "\" }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
            }
        }

        #endregion

        #region V2 Discussions API

        public void GetDiscussions(int folder_id)
        {
            string actionString = "/folders/";
            string url = _serviceUrl + actionString + folder_id.ToString() + "/discussions";

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONItem[]));
                    JSONItem[] discussions = (JSONItem[])ser.ReadObject(stream);

                    foreach (JSONItem disc in discussions)
                    {
                        Console.WriteLine(disc.ToString());
                    }
                }
            }
        }

        public void DeleteDiscussion(int disc_id)
        {
            string actionString = "/discussions/";
            string url = _serviceUrl + actionString + disc_id.ToString();

            using (Stream stream = BoxWebRequest.ExecuteDELETE(url, _http_Authorization_Header))
            {
            }
        }

        public void GetDiscussionComments(int disc_id)
        {
            string actionString = "/discussions/";
            string url = _serviceUrl + actionString + disc_id.ToString() + "/comments";

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                if (stream != null)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JSONComment[]));
                    JSONComment[] comments = (JSONComment[])ser.ReadObject(stream);

                    foreach (JSONComment comment in comments)
                    {
                        Console.WriteLine(comment.ToString());
                    }
                }
            }
        }

        public void PostCommentInDiscussion(int disc_id, string comment)
        {
            string actionString = "/discussions/";
            string url = _serviceUrl + actionString + disc_id.ToString() + "/comments";

            string requestBody = "{ \"comment\" : { \"message\": \"" +
                                    comment +
                                    "\" } }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePOST(url, _http_Authorization_Header, requestData))
            {
            }

        }

        public void UpdateDiscussionName(int disc_id, string new_name)
        {
            string actionString = "/discussions/";
            string url = _serviceUrl + actionString + disc_id.ToString();

            string requestBody = "{ \"name\" : \"" +
                                    new_name +
                                    "\" }";
            byte[] requestData = System.Text.Encoding.ASCII.GetBytes(requestBody.ToString());

            using (Stream stream = BoxWebRequest.ExecutePUT(url, _http_Authorization_Header, requestData))
            {
            }
        }
        #endregion

        #region V2 Events API

        public void GetEvents()
        {
            string actionString = "/events/";
            string url = _serviceUrl + actionString;

            using (Stream stream = BoxWebRequest.ExecuteGET(url, _http_Authorization_Header))
            {
                // Parse event info from stream
            }
        }

        #endregion
    }
    
 }

