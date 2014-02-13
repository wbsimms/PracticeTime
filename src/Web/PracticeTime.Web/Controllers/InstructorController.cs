using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Helpers;
using PracticeTime.Web.Lib;

namespace PracticeTime.Web.Controllers
{
    public class InstructorController : Controller
    {
        private ISessionRepository sessionRepository;
        private IBadgeRulesEngine rulesEngine;
        private IInstrumentRepository instrumentRepository;
        private IUserHelper userHelper;


        public InstructorController(ISessionRepository sessions,
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
        // GET: /Instructor/
        public ActionResult Index()
        {
            return View();
        }
	}
}