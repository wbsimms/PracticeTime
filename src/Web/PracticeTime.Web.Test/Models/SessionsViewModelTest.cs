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
            SessionsViewModel model = new SessionsViewModel();
            Assert.IsNotNull(model);
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
