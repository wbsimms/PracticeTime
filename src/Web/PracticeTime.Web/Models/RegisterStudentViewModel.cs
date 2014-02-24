using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class RegisterStudentViewModel
    {
        private bool isInited = false;
        private List<SelectListItem> registeredStudents;

        public RegisterStudentViewModel()
        {
        }

        public void Init(List<ApplicationUser> students)
        {
            this.registeredStudents =
                students.Select(
                    x => new SelectListItem() {Text = x.LastName + ", " + x.FirstName, Value = x.Id}).ToList();
            this.isInited = true;
        }

        [DisplayName("Student Token")]
        public string StudentTokenForRegistration { get; set; }

        [DisplayName("Registered Student")]
        public string SelectedRegisteredStudent { get; set; }

        public List<SelectListItem> RegisteredStudents {
            get
            {
                if (isInited)
                    return this.registeredStudents;
                throw new ApplicationException("Class not initalized. Developer must call Init()");
            }
            set
            {
                isInited = true;
                this.registeredStudents = value;
            }
        }
    }
}