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
        private IFirstSessionRule firstSessionRule;
        private IBadgeAwardRepository badgeAwardRepository;

        public BadgeRulesEngine(IFirstSessionRule firstSessionRule, IBadgeAwardRepository badgeAwardRepository)
        {
            this.firstSessionRule = firstSessionRule;
            this.badgeAwardRepository = badgeAwardRepository;
        }

        public ResponseModel RunRules(Session session)
        {
            ResponseModel response = new ResponseModel();
            response.Badges.AddRange(badgeAwardRepository.GetAllForUser(session.UserId));
            firstSessionRule.Rule(session, response);
            return response;
        }
    }
}
