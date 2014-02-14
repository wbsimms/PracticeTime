using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.Helpers;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Controllers
{
    public class AdminController : Controller
    {
        private IUserHelper userHelper;

        public AdminController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            AdminViewModel model = new AdminViewModel();
//            model.Init();

            return View();
        }

        public ActionResult Associate(AdminViewModel model)
        {
            string selectedInstructor = model.Instructors.First(x => x.Selected).Value;
            string selectedStudent = model.Students.First(x => x.Selected).Value;
            return View();
        }
    }
}