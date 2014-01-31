using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Helpers;
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
            string userId = UserHelper.GetUserId(User.Identity.Name);
            SessionsViewModel vm = new SessionsViewModel();
            vm.AllSessions = sessions.GetAllForUser(userId);
            return View(vm);
        }

        public ActionResult Add(SessionEntryViewModel sessionEntry)
        {
            string userId = UserHelper.GetUserId(User.Identity.Name);
            if (ModelState.IsValid)
            {
                sessions.Add(new Session()
                {
                    SessionDateTimeUtc = Convert.ToDateTime(sessionEntry.SessionDate),
                    Time = sessionEntry.Time,
                    TimeZoneOffset = sessionEntry.TimeZoneOffset,
                    Title = sessionEntry.Title,
                    UserId = userId
                });
                sessionEntry.StateMessage = "Session Saved";
            }

            List<string> userTitles = sessions.GetAllTitlesForUser(userId);
            sessionEntry.SessionTitles = userTitles;

            return View("Add",sessionEntry);
        }

        [HttpPost]
        public ActionResult GetSessionsForUser()
        {
            string userId = UserHelper.GetUserId(User.Identity.Name);
            List<Session> sessionList = sessions.GetAllForUser(userId);
            GGraph graph = new GGraph()
            {
                cols = new ColInfo[]
                {
                    new ColInfo() {id = "a", label = "Date", type = "date"},
                    new ColInfo() {id = "b", label = "Minutes", type = "number"}
                },
                p = new Dictionary<string, string>()
            };
            List<DataPointSet> dpsetList = new List<DataPointSet>(sessionList.Count);
            foreach (Session s in sessionList.OrderBy(x => x.SessionDateTimeUtc))
            {
                DataPointSet dps = new DataPointSet() {
                    c = new DataPoint[]
                    {
                        new DataPoint() {v = s.SessionDateTimeUtc.ToShortDateString()}, 
                        new DataPoint() {v = s.Time} 
                    }
                };
                dpsetList.Add(dps);
            }
            graph.rows = dpsetList.ToArray();

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(graph));
//            string json = new GGraphSerializer().Serailize(graph);
//            return json;
//            return new JsonResult(){Data = json};
        }
    }
}