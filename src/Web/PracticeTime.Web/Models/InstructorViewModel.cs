using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class InstructorViewModel
    {
        public List<ApplicationUser> Students { get; set; }

        [DisplayName("Students")]
        public string SelectedStudent { get; set; }     

        public List<SelectListItem> StudentsListItems {
            get
            {
                List<SelectListItem> List = new List<SelectListItem>();
                List.AddRange(Students.Select(x => new SelectListItem(){Text = x.UserName,Value = x.Id}));
                return List;
            }
        }
    }
}