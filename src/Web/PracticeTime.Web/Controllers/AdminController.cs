using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Helpers;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    public class AdminController : Controller
    {
        private IUserHelper userHelper;
        private IApplicationUserRepository applicationUserRepository;
        private IInstructorStudentRepository instructorStudentRepository;


        public AdminController(IUserHelper userHelper,IApplicationUserRepository applicationUser,
            IInstructorStudentRepository instructorStudent)
        {
            this.userHelper = userHelper;
            this.applicationUserRepository = applicationUser;
            this.instructorStudentRepository = instructorStudent;
        }

        public ActionResult Index(AdminViewModel model)
        {
            model.Init(applicationUserRepository.GetAllStudents(), applicationUserRepository.GetAllInstructors());
            return View(model);
        }


        public ActionResult Associate(AdminViewModel model)
        {
            InstructorStudent toAdd = new InstructorStudent(){InstructorId = model.SelectedInstructor,StudentId = model.SelectedStudent};
            InstructorStudent saved = instructorStudentRepository.Add(toAdd);
            model.Init(applicationUserRepository.GetAllStudents(), applicationUserRepository.GetAllInstructors());
            model.Messages = "Saved";
            model.HasErrors = false;
            if (saved == null)
            {
                model.Messages = "Already exists";
                model.HasErrors = false;
            }
            return View("Index",model);
        }

        [HttpPost]
        public JsonResult DeleteAssociation(string studentId, string instructorId)
        {
            InstructorStudent toDelete = new InstructorStudent(){InstructorId = instructorId, StudentId = studentId};
            instructorStudentRepository.Delete(toDelete);
            return GetInstructorStudents(instructorId);

        }

        [HttpPost]
        public JsonResult GetInstructorStudents(string instructorId)
        {
            List<InstructorStudent> students = instructorStudentRepository.GetAllForInstructor(instructorId);
            var retval = students.Select(x => new {StudentName = x.Student.UserName,StudentId = x.Student.Id});
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(retval));
        }
    }
}