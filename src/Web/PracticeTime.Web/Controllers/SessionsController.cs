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
            SessionsViewModel vm = new SessionsViewModel();
            vm.AllSessions = new List<Session>();
            vm.AllSessions.Add(new Session() {Time = 200, Title = "foo"});
            vm.AllSessions.Add(new Session() { Time = 400, Title = "bar" });
            return View(vm);
        }
	}
}