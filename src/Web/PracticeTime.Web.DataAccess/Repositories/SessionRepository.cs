using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PracticeTime.Web.DataAccess.Copiers;
using PracticeTime.Web.DataAccess.Models;
using System.Data.Entity;

namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface ISessionRepository
    {
        Session Add(Session session);
        void Update(Session session);
        void Delete(Session session);
        Session GetById(int sessionId);
        List<Session> GetAll();
        List<Session> GetAllForUser(string userId);
        List<string> GetAllTitles();
        List<string> GetAllTitlesForUser(string userId);
        List<UserData> GetTopUsersThisWeek();
    }

    public class SessionRepository : ISessionRepository
    {
        public Session Add(Session session)
        {
            if (session.SessionId != 0) throw new ApplicationException("Session.SessionId must be zero");
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                Session toSave = new SessionCopier().Copy(session);
                context.Sessions.Add(toSave);
                context.SaveChanges();
                return GetById(toSave.SessionId);
            }
        }

        public void Update(Session session)
        {
            if (session.SessionId == 0) throw new ApplicationException("Session.SessionId must be greather than 0");
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                Session toUpdate = context.Sessions.FirstOrDefault(x => x.SessionId == session.SessionId);
                if (toUpdate == null) throw new ApplicationException(string.Format("SessionId not found: {0}",session.SessionId));
                new SessionCopier().Merge(session,toUpdate);
                context.SaveChanges();
            }
        }

        public void Delete(Session session)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                Session toDelete = context.Sessions.FirstOrDefault(x => x.SessionId == session.SessionId);
                context.Sessions.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public Session GetById(int sessionId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions
                    .Include(x => x.C_Instrument)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.SessionId == sessionId);
            }
        }

        public List<Session> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions.AsNoTracking().ToList();
            }
        }

        public List<Session> GetAllForUser(string userId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions
                    .Include(x => x.C_Instrument)
                    .AsNoTracking()
                    .Where(s => s.UserId == userId)
                    .ToList();
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

        public List<UserData> GetTopUsersThisWeek()
        {
            
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    DateTime lastWeek = DateTime.UtcNow.AddDays(-7);
                    var allFromLastWeek = context.Sessions.AsNoTracking()
                        .Include(x => x.User)
                        .Where(x => x.SessionDateTimeUtc > lastWeek)
                        .GroupBy(x => x.User.UserName)
                        .Select(group => new UserData()
                        {
                            UserName = group.Key,
                            TimeThisWeek = group.Sum(x => x.Time)
                        }).OrderByDescending(x => x.TimeThisWeek).Take(10).ToList();

                    int rank = 0;
                    allFromLastWeek.ForEach(x => x.RankThisWeek = ++rank);
                    return allFromLastWeek;
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                    s.Any();
                }
            }
            return null;
        }
    }

    public class UserData
    {
        public string UserName { get; set; }
        public int TimeThisWeek { get; set; }
        public int RankThisWeek { get; set; }
    }
}
