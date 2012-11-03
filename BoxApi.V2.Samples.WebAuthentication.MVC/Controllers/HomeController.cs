using System.Text;
using System.Web.Mvc;
using BoxApi.V2.Model;

namespace BoxApi.V2.Samples.WebAuthentication.MVC.Controllers
{
    public class HomeController : Controller
    {
        private const string JsonFormat =
            @"{{
    ""AppKey"": ""{0}"",
    ""AuthKey"": ""{1}"",
    ""TestEmail"": ""box.csharp.sdk@gmail.com"",
    ""CollaboratingUser"": ""186800768""
}}";

        public ActionResult Index()
        {
            ClearSession();
            return View();
        }

        private void ClearSession()
        {
            Session["ApiKey"] = null;
            Session["Token"] = null;
        }

        public ActionResult InitiateAuthorization(string apiKey)
        {
            Session["ApiKey"] = apiKey;
            var boxAuthenticator = new BoxAuthenticator(apiKey);
            var authorizationUrl = boxAuthenticator.GetAuthorizationUrl();
            return new RedirectResult(authorizationUrl);
        }

        public ActionResult Authorize(string ticket, string auth_token)
        {
            Session["Token"] = auth_token;
            var model = new BoxAuthToken {AuthToken = auth_token};
            return View(model);
        }

        public FileResult Save()
        {
            var apiKey = Session["ApiKey"] as string;
            var token = Session["Token"] as string;
            var result = string.Format(JsonFormat, apiKey, token);
            return File(Encoding.UTF8.GetBytes(result), "application/json", "test_info.json");
        }
    }
}