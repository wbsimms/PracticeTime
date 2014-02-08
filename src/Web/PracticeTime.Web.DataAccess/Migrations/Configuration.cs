using System.Diagnostics.CodeAnalysis;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    [ExcludeFromCodeCoverage]
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
            context.Badges.AddOrUpdate(b => b.Name,
                new Models.C_Badge { Name = "First Session", Description = "Good job! You entered your first session", ImageUrl = "Images/Badges/FirstSession.png" },
                new Models.C_Badge { Name = "One Man Band", Description = "You're a one man band!", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 1", Description = "You're practiced a song over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 2", Description = "You're practiced two songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 3", Description = "You're practiced three songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 4", Description = "You're practiced four songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 5", Description = "You're practiced five songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 6", Description = "You're practiced six songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 7", Description = "You're practiced seven songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 8", Description = "You're practiced eight songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Song Master: Level 9", Description = "You're practiced nine songs over 5 hours.", ImageUrl = "" },
                new Models.C_Badge { Name = "Stage Ready", Description = "You've mastered 10 songs.", ImageUrl = "" }
                );

            context.Instruments.AddOrUpdate(b => b.Name,
                new C_Instrument { Name = "Guitar - Acoustic",Description = "",IconUrl = ""},
                new C_Instrument { Name = "Guitar - Classical", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Guitar - Electric", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Piano", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Saxaphone", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Clarinet", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Flute", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Oboe", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Drums", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Bass", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Voice", Description = "", IconUrl = "" },
                new C_Instrument { Name = "Violin", Description = "", IconUrl = "" }
                );

            context.SaveChanges();
        }
    }
}
