using System.Web.Mvc;

namespace PublishSubscribe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Publishers()
        {
            return View();
        }

        public ActionResult Subscribers()
        {
            return View();
        }

        public ActionResult Messages()
        {
            return View();
        }
    }
}