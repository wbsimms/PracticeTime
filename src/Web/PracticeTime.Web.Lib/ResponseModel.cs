using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Lib
{
    [Serializable]
    public class ResponseModel
    {
        public List<string> Errors { get; set; }
        public bool HasErrors { get; set; }
        public List<string> Messages { get; set; }
        public bool HasMessages { get; set; }
        public List<BadgeAward> Badges { get; set; }
        public List<BadgeAward> NewBadges { get; set; }
        public bool HasNewBadges { get; set; }

        public ResponseModel()
        {
            this.Badges = new List<BadgeAward>();
            this.NewBadges = new List<BadgeAward>();
            this.Errors = new List<string>();
            this.Messages = new List<string>();
        }
    }


}
