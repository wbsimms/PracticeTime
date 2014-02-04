using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Lib.BadgeRules;

namespace PracticeTime.Web.Lib
{
    public interface IBadgeRulesEngine
    {
        ResponseModel RunRules(Session session);
    }

    public class BadgeRulesEngine : IBadgeRulesEngine
    {
        private IBadgeAwardRepository badgeAwardRepository;
        List<IBadgeRule> rulesToCheck = new List<IBadgeRule>(); 

        public BadgeRulesEngine(
            IFirstSessionRule firstSessionRule,
            IOneManBandRule oneManBandRule,
            IBadgeAwardRepository badgeAwardRepository)
        {
            this.badgeAwardRepository = badgeAwardRepository;
            rulesToCheck.Add(firstSessionRule);
            rulesToCheck.Add(oneManBandRule);
        }

        public ResponseModel RunRules(Session session)
        {
            ResponseModel response = new ResponseModel();
            response.Badges.AddRange(badgeAwardRepository.GetAllForUser(session.UserId));
            foreach (IBadgeRule rule in rulesToCheck)
            {
                rule.Rule(session,response);
                if (response.HasNewBadges) break;
            }
            return response;
        }
    }
}