using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Lib.BadgeRules;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public interface IRepertoireRule : IBadgeRule
    {
    }

    public class RepertoireRule : BadgeRuleBase, IRepertoireRule
    {
        public static int KeyId = 13;

        public RepertoireRule(ISessionRepository sessionRepository, IBadgeAwardRepository badgeAwardRepository)
            : base(sessionRepository, badgeAwardRepository)
        {
        }

        public void Rule(Session session, ResponseModel response)
        {
            if (response.Badges.Any(x => x.C_BadgeId == KeyId)) return;
            List<Session> sessions = sessionRepository.GetAllForUser(session.UserId);
            if (sessions.Select(x => x.Title).Distinct().Count() >= 10)
            {
                BadgeAward award = new BadgeAward()
                {
                    UserId = session.UserId,
                    AwardDate = DateTime.UtcNow,
                    C_BadgeId = KeyId
                };
                BadgeAward badgeAward = badgeAwardRepository.Add(award);
                response.HasNewBadges = true;
                response.NewBadges.Add(badgeAward);
            }
        }
    }
}
