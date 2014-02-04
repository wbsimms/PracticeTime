using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Lib.BadgeRules
{
    public class BadgeRuleBase
    {
        protected ISessionRepository sessionRepository;
        protected IBadgeAwardRepository badgeAwardRepository;
        public BadgeRuleBase(ISessionRepository sessionRepository, IBadgeAwardRepository badgeAwardRepository)
        {
            this.sessionRepository = sessionRepository;
            this.badgeAwardRepository = badgeAwardRepository;
        }
    }
}
