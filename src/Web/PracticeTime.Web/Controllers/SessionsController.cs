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
            Random rnd = new Random();
            GGraph graph = new GGraph()
            {
                cols = new ColInfo[]
                {
                    new ColInfo() { id="a",label = "First",type="date"},
                    new ColInfo() { id="b",label = "Second",type="number"}
                },
                p = new Dictionary<string, string>(),
                rows = new DataPointSet[]
                {
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(-5).ToShortDateString()}, 
                        new DataPoint() {v = rnd.Next(1,1000)}, 
                    }},
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(-4).ToShortDateString()}, 
                        new DataPoint() {v = rnd.Next(1,1000)}, 
                    }},
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(-3).ToShortDateString()}, 
                        new DataPoint() {v = rnd.Next(1,1000)}, 
                    }},
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(-2).ToShortDateString()}, 
                        new DataPoint() {v = rnd.Next(1,1000)}, 
                    }},
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(-1).ToShortDateString()}, 
                        new DataPoint() {v = rnd.Next(1,1000)}, 
                    }},
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(+0).ToShortDateString()}, 
                        new DataPoint() {v = rnd.Next(1,1000)}, 
                    }},
                }
            };
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(graph));
//            string json = new GGraphSerializer().Serailize(graph);
//            return json;
//            return new JsonResult(){Data = json};
        }
    }
}