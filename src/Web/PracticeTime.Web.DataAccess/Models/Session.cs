using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTime.Web.DataAccess.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}
