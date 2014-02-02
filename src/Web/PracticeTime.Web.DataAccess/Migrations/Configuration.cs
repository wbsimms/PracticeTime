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
            context.Badges.AddOrUpdate(b => b.C_BadgeId,
                new Models.C_Badge { C_BadgeId = 1, Name = "First Session", Description = "Good job! You entered your first session", ImageUrl = "Images/Badges/FirstSession.png" },
                new Models.C_Badge { C_BadgeId = 2, Name = "One Man Band", Description = "You're a one man band!", ImageUrl = "" },
                new Models.C_Badge { C_BadgeId = 3, Name = "Song Master", Description = "You're practiced this song over 5 hours.", ImageUrl = "" }
                );

            context.SaveChanges();
        }
    }
}
