using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess
{
    public class PracticeTimeContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Session> Sessions { get; set; }
        public DbSet<BadgeAward> BadgeAwards { get; set; }
        public DbSet<C_Badge> Badges { get; set; }
        public DbSet<C_Instrument> Instruments { get; set; }
        public DbSet<C_AccountType> AccountTypes { get; set; }
        public DbSet<InstructorStudent> InstructorStudents { get; set; }

        public PracticeTimeContext() : base("DefaultConnection")
        {
        }
    }
}
