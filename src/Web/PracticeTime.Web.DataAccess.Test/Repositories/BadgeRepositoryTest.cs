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
    public class BadgeRepositoryTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            BadgeRepository repo = new BadgeRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void GetAllTest()
        {
            BadgeRepository repo = new BadgeRepository();
            List<C_Badge> badges = repo.GetAll();
            Assert.IsTrue(badges.Count > 0);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            BadgeRepository repo = new BadgeRepository();
            C_Badge badge = repo.GetById(1);
            Assert.IsNotNull(badge);
            Assert.AreEqual("First Session", badge.Name);
            Assert.IsNotNull(badge.Name);
            Assert.IsNotNull(badge.Description);
            Assert.IsNotNull(badge.ImageUrl);
            Assert.IsNotNull(badge.C_BadgeId);
        }

    }
}
