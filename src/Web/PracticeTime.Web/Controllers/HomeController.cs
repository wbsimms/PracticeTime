using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    public class HomeController : Controller
    {
        private ISessionRepository sessionRepository;

        public HomeController(ISessionRepository sessions)
        {
            this.sessionRepository = sessions;
        }

        public List<UserData> GetTopUsersThisWeek()
        {
            return sessionRepository.GetTopUsersThisWeek();
        }

        public ActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.TopUsersThisWeek = GetTopUsersThisWeek();
            return View(model);
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