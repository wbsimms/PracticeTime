using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
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

            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
            ApplicationUser user = store.FindByNameAsync("wbsimms").Result;
            if (user != null)
            {
                string id = store.FindByNameAsync("wbsimms").Result.Id;

                Session testSession = new Session() { Time = 300, Title = "Mel Bay Guitar 1", UserId = id, SessionDateTimeUtc = DateTime.UtcNow, TimeZoneOffset = 300 };
                context.Sessions.Add(testSession);
                context.SaveChanges();
                context.Dispose();
                Assert.IsTrue(testSession.SessionId > 0);
            }
        }
    }
}
