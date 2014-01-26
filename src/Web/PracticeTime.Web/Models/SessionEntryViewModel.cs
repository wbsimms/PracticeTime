using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeTime.Web.Models
{
    public class SessionEntryViewModel
    {
        public string UserId { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public int SessionId { get; set; }
    }
}