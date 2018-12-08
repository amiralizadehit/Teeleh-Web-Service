using System.Web.Mvc;
using Teeleh.Models.CustomValidation.Website;

namespace Teeleh.WApi.Controllers
{
    public class HomeController : Controller
    {
        [SessionTimeout]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
