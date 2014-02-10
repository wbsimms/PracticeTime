using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Copiers;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IAccountTypeRepository
    {
        C_AccountType GetById(int Id);
        List<C_AccountType> GetAll();
    }

    public class AccountTypeRepository : IAccountTypeRepository
    {
        public C_AccountType GetById(int Id)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.AccountTypes.AsNoTracking().FirstOrDefault(x => x.C_AccountTypeId == Id);
            }
        }

        public List<C_AccountType> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.AccountTypes.Where(x => x.Active).AsNoTracking().ToList();
            }
        }
    }
}
