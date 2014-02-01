using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public class FirstSessionRule : IBadgeRule
    {
        private ISessionRepository sessionRepository;

        public FirstSessionRule(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public void Rule(Session session, ResponseModel response)
        {
            List<Session> sessions = sessionRepository.GetAllForUser(session.UserId);
            if (sessions.Count == 1 &&
                sessions.FirstOrDefault(x =>x.SessionId == session.SessionId) == null)
            {
                // add badge and assign to user
            }
        }
    }
}
