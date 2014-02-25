using System;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess
{
    public interface IUserHelper
    {
//        string GetUserId(string name);
        string GetStudentToken(string name);
    }

    public class UserHelper : IUserHelper
    {
        static Random random = new Random();
        private static string _passwordArray = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public string GetStudentToken(string name)
        {
            ApplicationUser user = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PracticeTimeContext())).
                FindByName(name);
            return user.StudentToken;
        }

        public static PracticeTimeRoles GetRoleFromId(string id)
        {
            switch (id)
            {
                case "Student":
                    return PracticeTimeRoles.Student;
                case "Instructor":
                    return PracticeTimeRoles.Instructor;
                default:
                    throw new ApplicationException("Unable to determine role");
            }
        }
        public static string RandomString(int length) 
        { 
            StringBuilder password = new StringBuilder(); 
            for (int i = 0; i < length; i++) 
            { 
                int index = (int)Math.Ceiling(random.NextDouble() * _passwordArray.Length-1); 
                password.Append(_passwordArray[index]); 
            } 
            return password.ToString(); }
    }
}