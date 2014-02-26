using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class RegisterStudentViewModel
    {
        private List<SelectListItem> registeredStudents;

        public RegisterStudentViewModel()
        {
            ResponseMessage = new ResponseMessage();
        }

        [DisplayName("Student Token")]
        [Required]
        public string StudentTokenForRegistration { get; set; }

        [DisplayName("Registered Student")]
        public string SelectedRegisteredStudent { get; set; }

        public List<SelectListItem> RegisteredStudents {
            get
            {
                return this.registeredStudents;
            }
            set
            {
                this.registeredStudents = value;
            }
        }

        public ResponseMessage ResponseMessage { get; set; }
    }
}