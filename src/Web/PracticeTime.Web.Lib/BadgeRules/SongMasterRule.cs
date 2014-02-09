using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public interface ISongMasterRule : IBadgeRule
    {
    }

    public class SongMasterRule : BadgeRuleBase, ISongMasterRule
    {
        public SongMasterRule(ISessionRepository sessionRepository, IBadgeAwardRepository badgeAwardRepository)
            : base(sessionRepository, badgeAwardRepository)
        {
        }

        public void Rule(Session session, ResponseModel response)
        {
            List<Session> sessions = sessionRepository.GetAllForUser(session.UserId);

            IDictionary<string,int> titleTimeDictionary = new Dictionary<string, int>();
            foreach (Session sessionInDb in sessions)
            {
                if (!titleTimeDictionary.ContainsKey(sessionInDb.Title))
                    titleTimeDictionary.Add(sessionInDb.Title,0);
                titleTimeDictionary[sessionInDb.Title] += sessionInDb.Time;
            }

            int hours5 = 60*5;
            int countOver5 = titleTimeDictionary.Values.Count(x => x >= hours5);

            BadgeAward award = new BadgeAward()
            {
                UserId = session.UserId,
                AwardDate = DateTime.UtcNow,
            };

            bool newbadge = false;

            if (countOver5 == 0) return;
            if (countOver5 == 9 && response.Badges.All(x => x.C_BadgeId != 11))
            {
                newbadge = true;
                award.C_BadgeId = 11;
            }
            ;
            if (countOver5 == 8 && response.Badges.All(x => x.C_BadgeId != 10))
            {
                newbadge = true;
                award.C_BadgeId = 10;
            }
            if (countOver5 == 7 && response.Badges.All(x => x.C_BadgeId != 9))
            {
                newbadge = true;
                award.C_BadgeId = 9;
            }
            if (countOver5 == 6 && response.Badges.All(x => x.C_BadgeId != 8))
            {
                newbadge = true;
                award.C_BadgeId = 8;
            }
            if (countOver5 == 5 && response.Badges.All(x => x.C_BadgeId != 7))
            {
                newbadge = true;
                award.C_BadgeId = 7;
            }
            if (countOver5 == 4 && response.Badges.All(x => x.C_BadgeId != 6))
            {
                newbadge = true;
                award.C_BadgeId = 6;
            }
            if (countOver5 == 3 && response.Badges.All(x => x.C_BadgeId != 5))
            {
                newbadge = true;
                award.C_BadgeId = 5;
            }
            if (countOver5 == 2 && response.Badges.All(x => x.C_BadgeId != 4))
            {
                newbadge = true;
                award.C_BadgeId = 4;
            }
            if (countOver5 == 1 && response.Badges.All(x => x.C_BadgeId != 3))
            {
                newbadge = true;
                award.C_BadgeId = 3;
            }

            if (newbadge)
            {
                BadgeAward badgeAward = badgeAwardRepository.Add(award);
                response.HasNewBadges = true;
                response.NewBadges.Add(badgeAward);
            }
        }
    }
}
