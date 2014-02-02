using System;
using System.Collections.Concurrent;
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
using PracticeTime.Web.Lib;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private ISessionRepository sessions;
        private IBadgeRulesEngine rulesEngine;

        public SessionsController(ISessionRepository sessionRepository, IBadgeRulesEngine badgeRulesEngine)
        {
            this.sessions = sessionRepository;
            this.rulesEngine = badgeRulesEngine;
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
                Session session = new Session()
                {
                    SessionDateTimeUtc = Convert.ToDateTime(sessionEntry.SessionDate),
                    Time = sessionEntry.Time,
                    TimeZoneOffset = sessionEntry.TimeZoneOffset,
                    Title = sessionEntry.Title,
                    UserId = userId
                };

                Session savedSession = sessions.Add(session);
                sessionEntry.StateMessage = "Session Saved";
                ResponseModel response = rulesEngine.RunRules(savedSession);
                if (response.HasNewBadges)
                {
                    sessionEntry.BadgeAward = response.NewBadges.FirstOrDefault();
                }
                sessionEntry.BadgeAwards = response.Badges;
            }

            List<string> userTitles = sessions.GetAllTitlesForUser(userId);
            sessionEntry.SessionTitles = userTitles;

            return View("Add",sessionEntry);
        }

        [HttpPost]
        public ActionResult GetSessionsForUserGraph()
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

            return Json(new GGraphSerializer().Serailize(graph));
        }

        [HttpPost]
        public ActionResult GetSessionsForUserGraphTitle()
        {
            string userId = UserHelper.GetUserId(User.Identity.Name);
            List<Session> sessionList = sessions.GetAllForUser(userId);
            GGraph graph = new GGraph()
            {
                cols = new ColInfo[]
                {
                    new ColInfo() {id = "a", label = "Title", type = "string"},
                    new ColInfo() {id = "b", label = "Minutes", type = "number"}
                },
                p = new Dictionary<string, string>()
            };
            List<DataPointSet> dpsetList = new List<DataPointSet>(sessionList.Count);

            IDictionary<string,int> aggregateSession = new Dictionary<string, int>();
            foreach (Session s in sessionList)
            {
                if (!aggregateSession.ContainsKey(s.Title))
                    aggregateSession.Add(s.Title,0);
                aggregateSession[s.Title] += s.Time;
            }

            foreach (string s in aggregateSession.Keys)
            {
                DataPointSet dps = new DataPointSet()
                {
                    c = new DataPoint[]
                    {
                        new DataPoint() {v = s}, 
                        new DataPoint() {v = aggregateSession[s]} 
                    }
                };
                dpsetList.Add(dps);
            }
            graph.rows = dpsetList.ToArray();
            return Json(new GGraphSerializer().Serailize(graph));
        }

    }
}