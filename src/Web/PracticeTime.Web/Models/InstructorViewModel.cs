using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class InstructorViewModel
    {
        public List<ApplicationUser> Students { get; set; }
    }
}