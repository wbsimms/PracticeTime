using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PracticeTime.Web.DataAccess.Models
{
    public class BadgeAward
    {
        public int BadgeAwardId { get; set; }
        public C_Badge Badge { get; set; }
        public int BadgeId { get; set; }
        public DateTime AwardDate { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
