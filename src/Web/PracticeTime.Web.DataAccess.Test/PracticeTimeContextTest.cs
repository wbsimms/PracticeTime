using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Test
{
    [TestClass]
    public class PracticeTimeContextTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            PracticeTimeContext context = new PracticeTimeContext();
            Assert.IsNotNull(context);

            Session testSession = new Session() {Time = 300, Title = "Mel Bay Guitar 1", UserId = 2};
            context.Sessions.Add(testSession);
            context.SaveChanges();
            context.Dispose();
            Assert.IsTrue(testSession.SessionId > 0);
        }
    }
}
