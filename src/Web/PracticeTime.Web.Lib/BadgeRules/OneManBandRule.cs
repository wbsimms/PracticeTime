using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public interface IOneManBandRule : IBadgeRule
    {
    }


    public class OneManBandRule : BadgeRuleBase, IOneManBandRule
    {
        public static int KeyId = 2;
        public OneManBandRule(ISessionRepository sessionRepository, IBadgeAwardRepository badgeAwardRepository)
            : base(sessionRepository, badgeAwardRepository)
        {
        }


        public void Rule(Session session, ResponseModel response)
        {
            List<Session> allSessions = sessionRepository.GetAllForUser(session.UserId);
            if (response.Badges.Any(x => x.C_BadgeId == KeyId))
            {
                return;
            }
            int count = allSessions.Select(x => x.C_InstrumentId).Distinct().Count();
            if (count >= 3)
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
