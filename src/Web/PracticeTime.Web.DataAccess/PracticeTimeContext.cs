using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess
{
    public class PracticeTimeContext : DbContext
    {

        public DbSet<Session> Sessions { get; set; }

        public PracticeTimeContext() : base("DefaultConnection")
        {
        }
    }
}
