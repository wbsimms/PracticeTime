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
        private ISessionRepository sessionRepository;
        private IBadgeRulesEngine rulesEngine;
        private IInstrumentRepository instrumentRepository;
        private IUserHelper userHelper;

        public SessionsController(ISessionRepository sessions,
            IBadgeRulesEngine badgeRulesEngine,
            IInstrumentRepository instuments,
            IUserHelper userHelper)
        {
            this.instrumentRepository = instuments;
            this.sessionRepository = sessions;
            this.rulesEngine = badgeRulesEngine;
            this.userHelper = userHelper;
        }

        //
        // GET: /Sessions/
        public ActionResult Index()
        {
            string userId = userHelper.GetUserId(User.Identity.Name);
            SessionsViewModel vm = new SessionsViewModel();
            vm.AllSessions = sessionRepository.GetAllForUser(userId);
            return View(vm);
        }

        public ActionResult Add(SessionEntryViewModel sessionEntry)
        {
            List<C_Instrument> instruments = instrumentRepository.GetAll();
            string userId = userHelper.GetUserId(User.Identity.Name);
            if (ModelState.IsValid)
            {
                Session session = new Session()
                {
                    SessionDateTimeUtc = Convert.ToDateTime(sessionEntry.SessionDate),
                    Time = sessionEntry.Time,
                    TimeZoneOffset = sessionEntry.TimeZoneOffset,
                    Title = sessionEntry.Title,
                    UserId = userId,
                    C_InstrumentId = sessionEntry.SelectedInstrumentId
                };

                Session savedSession = sessionRepository.Add(session);
                sessionEntry.StateMessage = "Session Saved";
                ResponseModel response = rulesEngine.RunRules(savedSession);
                if (response.HasNewBadges)
                {
                    sessionEntry.BadgeAward = response.NewBadges.FirstOrDefault();
                }
                sessionEntry.BadgeAwards = response.Badges;
            }

            List<string> userTitles = sessionRepository.GetAllTitlesForUser(userId);
            sessionEntry.SessionTitles = userTitles;
            sessionEntry.Instruments = new SelectList(instruments,"C_InstrumentId","Name");

            return View("Add",sessionEntry);
        }

        [HttpPost]
        public ActionResult GetSessionsForUserGraph()
        {
            string userId = userHelper.GetUserId(User.Identity.Name);
            List<Session> sessionList = sessionRepository.GetAllForUser(userId);
            GGraph graph = new GGraph()
            {
                cols = new ColInfo[]
                {
                    new ColInfo() {id = "a", label = "Date", type = "date"},
                    new ColInfo() {id = "b", label = "Minutes", type = "number"}
                },
                p = new Dictionary<string, string>()
            };

            IDictionary<DateTime,int> sessionDateTimes = new Dictionary<DateTime, int>();
            foreach (Session s in sessionList)
            {
                if (!sessionDateTimes.ContainsKey(s.SessionDateTimeUtc.Date))
                {
                    sessionDateTimes.Add(s.SessionDateTimeUtc.Date,0);
                }
                sessionDateTimes[s.SessionDateTimeUtc.Date] += s.Time;
            }

            List<DataPointSet> dpsetList = new List<DataPointSet>(sessionList.Count);
            foreach (DateTime d in sessionDateTimes.Keys.OrderBy(x => x.Date))
            {
                DataPointSet dps = new DataPointSet() {
                    c = new DataPoint[]
                    {
                        new DataPoint() {v = d.ToShortDateString()}, 
                        new DataPoint() {v = sessionDateTimes[d]} 
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
            string userId = userHelper.GetUserId(User.Identity.Name);
            List<Session> sessionList = sessionRepository.GetAllForUser(userId);
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