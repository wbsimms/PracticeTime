using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Copiers
{
    public class BadgeAwardCopier
    {
        public BadgeAward Copy(BadgeAward from)
        {
            if (from == null) return null;
            BadgeAward to = new BadgeAward();
            to.BadgeAwardId = from.BadgeAwardId;
            Merge(from,to);
            return to;
        }

        public void Merge(BadgeAward from, BadgeAward to)
        {
            if (from == null) return;
            to.AwardDate = from.AwardDate;
            to.UserId = from.UserId;
            to.C_BadgeId = from.C_BadgeId;
        }
    }
}
