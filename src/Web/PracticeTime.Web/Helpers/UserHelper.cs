using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Helpers
{
    public class UserHelper
    {
        public static string GetUserId(string name)
        {
            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PracticeTimeContext())).
                FindByNameAsync(name).Result.Id;
        }
    }
}