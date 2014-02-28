using System.Data.Entity.Migrations.Model;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                new Models.C_Badge { Name = "Stage Ready", Description = "You've mastered 10 songs.", ImageUrl = "" },
                new Models.C_Badge { Name = "Repetoire", Description = "You have a good repertioire.", ImageUrl = "" }
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

            RoleManager<ApplicationRole> roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new PracticeTimeContext()));
            if (!roleManager.CreateAsync(new ApplicationRole(PracticeTimeRoles.Admin.ToString())).Result.Succeeded)
                throw new ApplicationException("Unable to create role");
            if (!roleManager.CreateAsync(new ApplicationRole(PracticeTimeRoles.Student.ToString())).Result.Succeeded)
                throw new ApplicationException("Unable to create role");

            if (!roleManager.CreateAsync(new ApplicationRole(PracticeTimeRoles.Instructor.ToString())).Result.Succeeded)
                throw new ApplicationException("Unable to create role");


            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PracticeTimeContext()));
            if (!userManager.CreateAsync(new ApplicationUser() { UserName = "student",LastName = "dent1",FirstName = "stu1",StudentToken = UserHelper.RandomString(10)}, "student").Result.Succeeded)
                throw new Exception("Unable to Add user");
            if (!userManager.AddToRoleAsync(userManager.FindByNameAsync("student").Result.Id, PracticeTimeRoles.Student.ToString()).Result.Succeeded)
                throw new ApplicationException("Unable to add role");

            if (!userManager.CreateAsync(new ApplicationUser() { UserName = "teacher" }, "teacher").Result.Succeeded)
                throw new Exception("Unable to Add user");
            if (!userManager.AddToRoleAsync(userManager.FindByNameAsync("teacher").Result.Id, PracticeTimeRoles.Instructor.ToString()).Result.Succeeded)
                throw new ApplicationException("Unable to add role");

            if (!userManager.CreateAsync(new ApplicationUser() { UserName = "student2", LastName = "dent2", FirstName = "stu2", StudentToken = UserHelper.RandomString(10) }, "student2").Result.Succeeded)
                throw new Exception("Unable to Add user");
            if (!userManager.AddToRoleAsync(userManager.FindByNameAsync("student2").Result.Id, PracticeTimeRoles.Student.ToString()).Result.Succeeded)
                throw new ApplicationException("Unable to add role");

            IdentityResult result =
                userManager.CreateAsync(new ApplicationUser() {UserName = "admin"}, "admin1234").Result;

            if (!result.Succeeded)
                throw new Exception("Unable to Add user"+string.Join("\r\n",result.Errors));
            if (!userManager.AddToRoleAsync(userManager.FindByNameAsync("Admin").Result.Id, PracticeTimeRoles.Admin.ToString()).Result.Succeeded)
                throw new ApplicationException("Unable to add role");


            string studentId = userManager.FindByNameAsync("student").Result.Id;
            string student2Id = userManager.FindByNameAsync("student2").Result.Id;
            string teacherId = userManager.FindByNameAsync("teacher").Result.Id;


            context.InstructorStudents.AddOrUpdate(new InstructorStudent {InstructorId = teacherId,StudentId = studentId},
                new InstructorStudent { InstructorId = teacherId, StudentId = student2Id }
                );
        }
    }
}
