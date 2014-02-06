using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PracticeTime.Web.DataAccess.Models
{
    [Serializable]
    public class BadgeAward
    {
        public int BadgeAwardId { get; set; }
        public C_Badge C_Badge { get; set; }
        public int C_BadgeId { get; set; }
        public DateTime AwardDate { get; set; }
        [XmlIgnore]
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}
