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
using PracticeTime.Web.Helpers;
using PracticeTime.Web.Lib;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    [Authorize(Roles = "Instructor,Admin")]
    public class InstructorController : Controller
    {
        private ISessionRepository sessionRepository;
        private IUserHelper userHelper;
        private IInstructorStudentRepository instructorStudentRepository;
        private IApplicationUserRepository applicationUserRepository;


        public InstructorController(ISessionRepository sessions,
            IUserHelper userHelper,
            IInstructorStudentRepository instructorStudent,
            IApplicationUserRepository applicationUser)
        {
            this.sessionRepository = sessions;
            this.userHelper = userHelper;
            this.instructorStudentRepository = instructorStudent;
            this.applicationUserRepository = applicationUser;
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

        public ActionResult RegisterStudents(RegisterStudentViewModel model)
        {
            List<InstructorStudent> instructorStudents = instructorStudentRepository.GetAllForInstructor(User.Identity.GetUserId());
            model.Init(new List<ApplicationUser>(instructorStudents.Select(x => x.Student)));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RegisterStudent(RegisterStudentViewModel model)
        {
            ResponseMessage message = new ResponseMessage() {Message = "Student Registered"};
            string id = User.Identity.GetUserId();
            ApplicationUser student = applicationUserRepository.GetUserByToken(model.StudentTokenForRegistration);
            if (student == null)
            {
                message.Errors.Add("Token not found");
                message.Message = "Registration Failed";
            }
            else
            {
                InstructorStudent retval =
                    instructorStudentRepository.Add(new InstructorStudent() {InstructorId = id, StudentId = student.Id});
            }

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(message));
        }
    }
}