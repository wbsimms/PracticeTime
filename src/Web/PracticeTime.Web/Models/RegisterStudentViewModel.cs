using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class RegisterStudentViewModel
    {
        private bool isInited = false;
        private List<SelectListItem> students;

        public RegisterStudentViewModel()
        {
        }

        public void Init(List<ApplicationUser> students)
        {
            this.students =
                students.Select(
                    x => new SelectListItem() {Text = x.LastName + ", " + x.FirstName, Value = x.StudentToken}).ToList();
            this.isInited = true;
        }

        public List<SelectListItem> Students {
            get
            {
                if (isInited)
                    return this.students;
                throw new ApplicationException("Class not initalized. Developer must call Init()");
            }
            set
            {
                isInited = true;
                this.students = value;
            }
        }
    }
}