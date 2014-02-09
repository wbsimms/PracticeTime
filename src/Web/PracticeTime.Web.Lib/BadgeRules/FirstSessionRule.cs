using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public interface IFirstSessionRule : IBadgeRule
    {
    }

    public class FirstSessionRule : BadgeRuleBase, IFirstSessionRule
    {
        public FirstSessionRule(ISessionRepository sessionRepository, IBadgeAwardRepository badgeAwardRepository) : base(sessionRepository, badgeAwardRepository)
        {
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
                    C_BadgeId = 1
                };
                BadgeAward badgeAward = badgeAwardRepository.Add(award);
                response.HasNewBadges = true;
                response.NewBadges.Add(badgeAward);
            }
        }
    }
}
