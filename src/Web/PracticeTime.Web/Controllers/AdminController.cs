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

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            AdminViewModel model = new AdminViewModel();
            model.Init(applicationUserRepository.GetAllStudents(),applicationUserRepository.GetAllInstructors());
            return View(model);
        }

        public ActionResult Associate(AdminViewModel model)
        {
            InstructorStudent toAdd = new InstructorStudent(){InstructorId = model.SelectedInstructor,StudentId = model.SelectedStudent};
            instructorStudentRepository.Add(toAdd);
            return View();
        }
    }
}