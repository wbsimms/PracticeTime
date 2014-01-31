using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeTime.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Thanks for wanting to learn more.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Thanks for wanting to get in touch.";

            return View();
        }
    }
}