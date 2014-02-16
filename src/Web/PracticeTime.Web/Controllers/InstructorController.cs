using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Helpers;
using PracticeTime.Web.Lib;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        private ISessionRepository sessionRepository;
        private IBadgeRulesEngine rulesEngine;
        private IInstrumentRepository instrumentRepository;
        private IUserHelper userHelper;
        private IInstructorStudentRepository instructorStudentRepository;


        public InstructorController(ISessionRepository sessions,
            IBadgeRulesEngine badgeRulesEngine,
            IInstrumentRepository instuments,
            IUserHelper userHelper,
            IInstructorStudentRepository instructorStudent)
        {
            this.instrumentRepository = instuments;
            this.sessionRepository = sessions;
            this.rulesEngine = badgeRulesEngine;
            this.userHelper = userHelper;
            this.instructorStudentRepository = instructorStudent;
        }

        public ActionResult Index()
        {
            string userId = userHelper.GetUserId(User.Identity.Name);

            InstructorViewModel model = new InstructorViewModel();
            model.Students = instructorStudentRepository.GetAllForInstructor(userId).Select(x => x.Student).ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult GetSessionsForStudent(string studentId)
        {
            List<Session> session = sessionRepository.GetAllForUser(studentId);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(session));
        }
    }
}