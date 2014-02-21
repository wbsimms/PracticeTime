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
    public enum Roles
    {
        Student,
        Instructor,
        Admin
    }
    
    public interface IUserHelper
    {
        string GetUserId(string name);
    }

    public class UserHelper : IUserHelper
    {
        public string GetUserId(string name)
        {
            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PracticeTimeContext())).
                FindByNameAsync(name).Result.Id;
        }

        public static Roles GetRoleFromId(string id)
        {
            switch (id)
            {
                case "1":
                    return Roles.Student;
                case "2":
                    return Roles.Instructor;
                default:
                    throw new ApplicationException("Unable to determine role");
            }
        }

    }
}