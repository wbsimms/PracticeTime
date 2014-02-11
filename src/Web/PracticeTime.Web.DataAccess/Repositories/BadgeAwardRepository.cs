using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Copiers;
using PracticeTime.Web.DataAccess.Models;
using System.Data.Entity;


namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IBadgeAwardRepository
    {
        BadgeAward Add(BadgeAward award);
        void Update(BadgeAward badgeAward);
        void Delete(BadgeAward badgeAward);
        BadgeAward GetById(int badgeAwardId);
        List<BadgeAward> GetAll();
        List<BadgeAward> GetAllForUser(string userId);
    }

    public class BadgeAwardRepository : IBadgeAwardRepository
    {
        public BadgeAward Add(BadgeAward award)
        {
            if (award.BadgeAwardId != 0) throw new ApplicationException("BadgeAward.BadgeAwardId must be zero");
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                BadgeAward toSave = new BadgeAwardCopier().Copy(award);
                context.BadgeAwards.Add(toSave);
                context.SaveChanges();
                return GetById(toSave.BadgeAwardId);
            }
        }

        public void Update(BadgeAward badgeAward)
        {
            if (badgeAward.BadgeAwardId == 0) throw new ApplicationException("Id must be greather than 0");
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                BadgeAward toUpdate = context.BadgeAwards.FirstOrDefault(x => x.BadgeAwardId == badgeAward.BadgeAwardId);
                if (toUpdate == null) throw new ApplicationException(string.Format("BadgeAward not found: {0}", badgeAward.BadgeAwardId));
                new BadgeAwardCopier().Merge(badgeAward, toUpdate);
                context.SaveChanges();
            }
        }

        public void Delete(BadgeAward badgeAward)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                BadgeAward toDelete = context.BadgeAwards.FirstOrDefault(x => x.BadgeAwardId == badgeAward.BadgeAwardId);
                context.BadgeAwards.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public BadgeAward GetById(int badgeAwardId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.BadgeAwards
                    .Include(x => x.C_Badge)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.BadgeAwardId == badgeAwardId);
            }
        }

        public List<BadgeAward> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.BadgeAwards
                    .Include(x => x.C_Badge)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<BadgeAward> GetAllForUser(string userId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.BadgeAwards.AsNoTracking()
                    .Include(x => x.C_Badge)
                    .Where(s => s.UserId == userId).ToList();
            }
        }
    }
}
