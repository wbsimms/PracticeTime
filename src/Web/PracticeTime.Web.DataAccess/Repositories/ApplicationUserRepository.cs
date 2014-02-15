using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetAllStudents();
        List<ApplicationUser> GetAllInstructors();
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public List<ApplicationUser> GetAllStudents()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                List<ApplicationUser> users = context.Users
                    .Include(x => x.C_AccountType)
                    .Where(x => x.C_AccountTypeId == 1)
                    .AsNoTracking().ToList();
                return users;
            }
        }

        public List<ApplicationUser> GetAllInstructors()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                List<ApplicationUser> users = context.Users
                    .Include(x => x.C_AccountType)
                    .Where(x => x.C_AccountTypeId == 2)
                    .AsNoTracking().ToList();
                return users;
            }
        }

    }
}
