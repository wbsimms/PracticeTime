using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetAllStudents();
        List<ApplicationUser> GetAllInstructors();
        ApplicationUser GetUserByToken(string token);
        List<ApplicationUser> GetAppPublicProfiles();
        List<ApplicationUser> GetAll();
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public List<ApplicationUser> GetAllStudents()
        {
            return GetByRole(PracticeTimeRoles.Student);
        }

        public List<ApplicationUser> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                RoleManager<ApplicationRole> roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                List<ApplicationUser> users = context.Users.ToList();
                return users;
            }
        }


        private List<ApplicationUser> GetByRole(PracticeTimeRoles role)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                RoleManager<ApplicationRole> roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
             
                var users1 = roleManager.FindByName(role.ToString()).Users.Select(x => x.UserId);
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                List<ApplicationUser> users = context.Users.Where(x => users1.Contains(x.Id)).ToList();
                return users;
            }
        }

        public List<ApplicationUser> GetAllInstructors()
        {
            return GetByRole(PracticeTimeRoles.Instructor);
        }

        public ApplicationUser GetUserByToken(string token)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Users.AsNoTracking()
                    .FirstOrDefault(x => x.StudentToken == token);
            }
        }

        public List<ApplicationUser> GetAppPublicProfiles()
        {
            var instructors = GetByRole(PracticeTimeRoles.Instructor);
            var students = GetByRole(PracticeTimeRoles.Student).Where(x => x.StudentPublicProfile).ToList();
            instructors.AddRange(students);
            return instructors;
        }
    }
}
