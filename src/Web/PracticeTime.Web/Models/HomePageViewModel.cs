using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Models
{
    public class HomePageViewModel
    {
        public List<UserData> TopUsersThisWeek { get; set; }
    }

}