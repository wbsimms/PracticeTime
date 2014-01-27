using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private ISessionRepository sessions;

        public SessionsController(ISessionRepository sessionRepository)
        {
            this.sessions = sessionRepository;
        }

        //
        // GET: /Sessions/
        public ActionResult Index()
        {
            ApplicationUser user = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PracticeTimeContext())).FindByNameAsync
                (User.Identity.Name).Result;
            string id = user.Id;

            SessionsViewModel vm = new SessionsViewModel();
            vm.AllSessions = new List<Session>();
            vm.AllSessions.Add(new Session() {Time = 200, Title = "foo"});
            vm.AllSessions.Add(new Session() { Time = 400, Title = "bar" });
            return View(vm);
        }

        public ActionResult Add(SessionEntryViewModel sessionEntry)
        {
            sessionEntry.SessionTitles = new List<string>(){"Mel Bay","FingerStyle"};
            return View("Add",sessionEntry);
        }
    }
}