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
    public interface IBadgeAwardRepository
    {
        BadgeAward Add(BadgeAward award);
        void Update(BadgeAward badgeAward);
        void Delete(BadgeAward badgeAward);
        BadgeAward GetById(int badgeAwardId);
        List<BadgeAward> GetAll();
        List<BadgeAward> GetAllForUser(string userId);
        List<string> GetAllTitles();
        List<string> GetAllTitlesForUser(string userId);
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
                return GetById(toSave.BadgeId);
            }
        }

        public void Update(BadgeAward badgeAward)
        {
            if (badgeAward.BadgeAwardId == 0) throw new ApplicationException("Id must be greather than 0");
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                BadgeAward toUpdate = context.BadgeAwards.FirstOrDefault(x => x.BadgeAwardId == badgeAward.BadgeAwardId);
                if (toUpdate == null) throw new ApplicationException(string.Format("SessionId not found: {0}", badgeAward.BadgeAwardId));
                new BadgeAwardCopier().Merge(badgeAward, toUpdate);
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
                return context.BadgeAwards.AsNoTracking().FirstOrDefault(x => x.BadgeAwardId == badgeAwardId);
            }
        }

        public List<BadgeAward> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.BadgeAwards.AsNoTracking().ToList();
            }
        }

        public List<BadgeAward> GetAllForUser(string userId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.BadgeAwards.AsNoTracking().Where(s => s.UserId== userId).ToList();
            }
        }

        public List<string> GetAllTitles()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions.AsNoTracking()
                    .Select((x) => x.Title )
                    .Distinct()
                    .ToList();
            }
        }


        public List<string> GetAllTitlesForUser(string userId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions.AsNoTracking()
                    .Where(s => s.UserId == userId)
                    .Select((x) => x.Title)
                    .Distinct()
                    .ToList();
            }
        }


    }
}
