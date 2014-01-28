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
    public interface ISessionRepository
    {
        void Add(Session session);
        void Update(Session session);
        void Delete(Session session);
        Session GetById(int sessionId);
        List<Session> GetAll();
        List<Session> GetAllForUser(string userId);
        List<string> GetAllTitles();
        List<string> GetAllTitlesForUser(string userId);
    }

    public class SessionRepository : ISessionRepository
    {
        public void Add(Session session)
        {
            if (session.SessionId != 0) throw new ApplicationException("Session.SessionId must be zero");
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                Session toSave = new SessionCopier().Copy(session);
                context.Sessions.Add(toSave);
                context.SaveChanges();
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
                return context.Sessions.AsNoTracking().FirstOrDefault(x => x.SessionId == sessionId);
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
                return context.Sessions.AsNoTracking().Where(s => s.UserId == userId).ToList();
            }
        }

        public List<string> GetAllTitles()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions.AsNoTracking().Select((x) => x.Title ).ToList();
            }
        }


        public List<string> GetAllTitlesForUser(string userId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.Sessions.AsNoTracking().Where(s => s.UserId == userId).Select((x) => x.Title).ToList();
            }
        }


    }
}
