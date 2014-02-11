using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Test.Models
{
    [TestClass]
    public class BadgeAwardTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            BadgeAward badgeAward = new BadgeAward(){User = new ApplicationUser()};
            Assert.IsNotNull(badgeAward);
            Assert.IsNotNull(badgeAward.User);
        }
    }
}
