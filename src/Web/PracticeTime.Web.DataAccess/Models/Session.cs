using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PracticeTime.Web.DataAccess.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public IdentityUser User { get; set; }
        public DateTime SessionDateTimeUtc { get; set; }
        public int TimeZoneOffset { get; set; }
        public string UserId { get; set; }
        public int C_InstrumentId { get; set; }
        public C_Instrument C_Instrument { get; set; }

    }
}
