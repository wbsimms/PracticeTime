using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PracticeTime.Web.DataAccess.PracticeTimeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PracticeTime.Web.DataAccess.PracticeTimeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Badges.AddOrUpdate(b => b.C_BadgeId,
                new Models.C_Badge { C_BadgeId = 1, Name = "First Session", Description = "Good job! You entered your first session", ImageUrl = "Images/Badges/FirstSession.png" },
                new Models.C_Badge { C_BadgeId = 2, Name = "One Man Band", Description = "You're a one man band!", ImageUrl = "" },
                new Models.C_Badge { C_BadgeId = 3, Name = "Song Master", Description = "You're practiced this song over 5 hours.", ImageUrl = "" }
                );

            context.Instruments.AddOrUpdate(b => b.C_InstrumentId,
                new C_Instrument { C_InstrumentId = 1, Name = "Guitar - Acoustic",Description = "",IconUrl = ""},
                new C_Instrument { C_InstrumentId = 2, Name = "Guitar - Classical", Description = "", IconUrl = "" },
                new C_Instrument { C_InstrumentId = 3, Name = "Guitar - Electric", Description = "", IconUrl = "" },
                new C_Instrument { C_InstrumentId = 4, Name = "Piano", Description = "", IconUrl = "" }
                );

            context.SaveChanges();
        }
    }
}
