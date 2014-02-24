using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.DataAccess.Test.Repositories
{
    [TestClass]
    public class ApplicationUserRepositoryTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ApplicationUserRepository repo = new ApplicationUserRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void GetAllStudentsTest()
        {
            ApplicationUserRepository repo = new ApplicationUserRepository();
            List<ApplicationUser> users = repo.GetAllStudents();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.All(x => x.Roles.Any(r => r.Role.Name == "Student")));
        }

        [TestMethod]
        public void GetAllInstructorsTest()
        {
            ApplicationUserRepository repo = new ApplicationUserRepository();
            List<ApplicationUser> users = repo.GetAllInstructors();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.All(x => x.Roles.Any(r => r.Role.Name == "Instructor")));
        }

    }
}
