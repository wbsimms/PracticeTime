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
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public List<ApplicationUser> GetAllStudents()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                string roleName = PracticeTimeRoles.Student.ToString();
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                List<ApplicationUser> users = context.Users
                    .Include(x => x.Roles).Include(x => x.Roles.Select(y => y.Role))
                    .Where(x => x.Roles.Any(y => y.Role.Name == roleName))
                    .AsNoTracking().ToList();
                return users;
            }
        }

        public List<ApplicationUser> GetAllInstructors()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                string roleName = PracticeTimeRoles.Instructor.ToString();
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                List<ApplicationUser> users = context.Users
                    .Include(x => x.Roles).Include(x => x.Roles.Select(y => y.Role))
                    .Where(x => x.Roles.Any(y => y.Role.Name == roleName))
                    .AsNoTracking().ToList();
                return users;
            }
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
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                string roleName = PracticeTimeRoles.Instructor.ToString();
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                List<ApplicationUser> users = context.Users
                    .Include(x => x.Roles).Include(x => x.Roles.Select(y => y.Role))
                    .Where(x => x.Roles.Any(y => y.Role.Name == roleName) || x.StudentPublicProfile)
                    .AsNoTracking().ToList();
                return users;
            }

        }
    }
}
