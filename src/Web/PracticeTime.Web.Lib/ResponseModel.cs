using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTime.Web.Lib
{
    public class ResponseModel
    {
        public List<string> Errors { get; set; }
        public bool HasErrors { get; set; }
        public List<string> Messages { get; set; }
        public bool HasMessages { get; set; }

        public List<Badge> Badges { get; set; }
        public List<Badge> NewBadges { get; set; }
        public bool HasNewBadges { get; set; }
    }


}
