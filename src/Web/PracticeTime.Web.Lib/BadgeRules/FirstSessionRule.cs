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
        private IBadgeAwardRepository badgeAwardRepository;

        public FirstSessionRule(ISessionRepository sessionRepository,IBadgeAwardRepository badgeAwardRepository)
        {
            this.sessionRepository = sessionRepository;
            this.badgeAwardRepository = badgeAwardRepository;
        }

        public void Rule(Session session, ResponseModel response)
        {
            List<Session> sessions = sessionRepository.GetAllForUser(session.UserId);
            if (sessions.Count == 1 &&
                sessions.Any(x =>x.SessionId == session.SessionId))
            {
                BadgeAward award = new BadgeAward()
                {
                    UserId = session.UserId,
                    AwardDate = DateTime.UtcNow,
                    BadgeId = 1
                };
                badgeAwardRepository.Add(award);
                response.HasNewBadges = true;
                response.Badges.Add(award);
                response.NewBadges.Add(award);
            }

        }
    }
}
