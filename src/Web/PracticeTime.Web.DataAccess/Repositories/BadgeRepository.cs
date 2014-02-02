using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Copiers;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IBadgeRepository
    {
        C_Badge GetById(int badgeId);
        List<C_Badge> GetAll();
    }

    public class BadgeRepository : IBadgeRepository
    {
        public C_Badge GetById(int badgeId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Badges.AsNoTracking().FirstOrDefault(x => x.C_BadgeId == badgeId);
            }
        }

        public List<C_Badge> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Badges.AsNoTracking().ToList();
            }
        }
    }
}
