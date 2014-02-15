using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class AdminViewModel
    {
        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> Instructors { get; set; }
        [DisplayName("Select Student")]
        public string SelectedStudent { get; set; }
        [DisplayName("Select Instructor")]
        public string SelectedInstructor { get; set; }

        public bool HasErrors { get; set; }
        public string Messages { get; set; }

        public AdminViewModel()
        {
            this.Students = new List<SelectListItem>();
            this.Instructors = new List<SelectListItem>();
        }

        public void Init(List<ApplicationUser> students, List<ApplicationUser> instructors)
        {
            Students.AddRange(students.Select(x => new SelectListItem() { Text = x.UserName, Value = x.Id }));
            Instructors.AddRange(instructors.Select(x => new SelectListItem() { Text = x.UserName, Value = x.Id }));
        }


    }
}