using System.Web.Mvc;

namespace wrappify.Web.Controllers
{
    public class WrappifyController :Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}