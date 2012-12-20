using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using BoxApi.V2.Authentication.OAuth2;
using BoxApi.V2.Samples.WebAuthentication.MVC.Models;

namespace BoxApi.V2.Samples.WebAuthentication.MVC.Controllers
{
    public class HomeController : Controller
    {
        private const string JsonFormat =
            @"{{
    ""ClientId"": ""{0}"",
    ""ClientSecret"": ""{1}"",
    ""AccessToken"": ""{2}"",
    ""RefreshToken"": ""{3}"",
    ""TestEmail"": ""box.csharp.sdk@gmail.com"",
    ""CollaboratingUser"": ""186800768""
}}";

        private const string ClientId = "clientId";
        private const string ClientSecret = "clientSecret";
        private const string AccessToken = "accessToken";
        private const string RefreshToken = "refreshToken";

        public ActionResult Index()
        {
            ClearSession();
            return View();
        }

        private void ClearSession()
        {
            Session[ClientId] = null;
            Session[ClientSecret] = null;
            Session[AccessToken] = null;
            Session[RefreshToken] = null;
        }

        public ActionResult InitiateAuthorization(string clientId, string clientSecret)
        {
            Session[ClientId] = clientId;
            Session[ClientSecret] = clientSecret;
            var boxAuthenticator = new TokenProvider(clientId, clientSecret);
            string authorizationUrl = boxAuthenticator.GetAuthorizationUrl();
            return new RedirectResult(authorizationUrl);
        }

        public ActionResult Authorize()
        {
            return IsError() 
                ? Error(Request.QueryString["error"], Request.QueryString["error_description"], Request.QueryString["state"]) 
                : Token(Request.QueryString["code"]);
        }

        private bool IsError()
        {
            return Request.QueryString["error"] != null;
        }

        private ActionResult Token(string arg1)
        {
            try
            {
                var boxAuthenticator = new TokenProvider(Session[ClientId] as string, Session[ClientSecret] as string);
                OAuthToken accessToken = boxAuthenticator.GetAccessToken(arg1);
                Session[AccessToken] = accessToken.AccessToken;
                Session[RefreshToken] = accessToken.RefreshToken;
                return View(accessToken);
            }
            catch (BoxException e)
            {
                return Error(e.Error.Status, e.Message);
            }
            catch (Exception e)
            {
                return Error(e.Message, e.StackTrace);
            }
        }

        private ViewResult Error(string error, string description, string state = null)
        {
            ClearSession();
            return View("Error", new ErrorModel(){Message = error, Description = description ?? "(none)", State = state ?? "(none)"});
        }

        public FileResult Save()
        {
            string result = string.Format(JsonFormat, Session[ClientId] as string, Session[ClientSecret] as string, Session[AccessToken] as string, Session[RefreshToken] as string);
            ClearSession();
            return File(Encoding.UTF8.GetBytes(result), "application/json", "test_info.json");
        }
    }
}