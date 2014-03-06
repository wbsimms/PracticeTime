using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class SessionsViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            SessionsViewModel model = new SessionsViewModel(){StudentToken = "blah", Badges = new List<BadgeAward>()};
            Assert.IsNotNull(model);
            Assert.AreEqual("blah",model.StudentToken);
            Assert.IsNotNull(model.Badges);
        }

        [TestMethod]
        public void AllSessionsTest()
        {
            SessionsViewModel model = new SessionsViewModel();
            List<Session> allSessions = model.AllSessions;
            Assert.IsNotNull(allSessions);
        }

    }
}
