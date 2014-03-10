using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Models
{
    public class MusiciansViewModel
    {
        public MusiciansViewModel()
        {
            this.PublicUsers = new List<ApplicationUser>();
        }

        public List<ApplicationUser> PublicUsers { get; set; }
    }
}