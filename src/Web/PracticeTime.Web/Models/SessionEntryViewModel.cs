using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace PracticeTime.Web.Models
{
    public class SessionEntryViewModel
    {
        public string UserId { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public string Title { get; set; }
        public int SessionId { get; set; }
        [Required]
        [Display(Name = "Date")]
        public string SessionDate { get; set; }
        public int TimeZoneOffset { get; set; }
        public List<string> SessionTitles { get; set; }
        public string StateMessage { get; set; }
    }
}