using System.Web.Mvc;
using BoxApi.V2.Model;

namespace BoxApi.V2.Samples.WebAuthentication.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitiateAuthorization(string apiKey)
        {
            var boxAuthenticator = new BoxAuthenticator(apiKey);
            var authorizationUrl = boxAuthenticator.GetAuthorizationUrl();
            return new RedirectResult(authorizationUrl);
        }

        public ActionResult Authorize(string ticket, string auth_token)
        {
            var model = new BoxAuthToken() {AuthToken = auth_token};
            return View(model);
        }
    }
}