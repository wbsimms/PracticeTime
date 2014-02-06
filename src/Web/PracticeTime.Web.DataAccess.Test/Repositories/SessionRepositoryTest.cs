using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.DataAccess.Test.Repositories
{
    [TestClass]
    public class SessionRepositoryTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            SessionRepository repo = new SessionRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void AddTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("wbsimms").Result;

                SessionRepository repo = new SessionRepository();
                Session retval = repo.Add(new Session()
                {
                    C_InstrumentId = 1,
                    SessionDateTimeUtc = DateTime.UtcNow,
                    Time = 300,
                    TimeZoneOffset = 300,
                    Title = "blah",
                    UserId = user.Id
                });
                Assert.IsNotNull(retval);
                Assert.IsTrue(retval.SessionId > 0);
                Assert.IsNotNull(retval.C_Instrument);
                Assert.IsNotNull(retval.C_Instrument.Name);
            }
        }

        [TestMethod]
        public void AddWithExistingSessionIdTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("wbsimms").Result;

                SessionRepository repo = new SessionRepository();
                try
                {
                    Session retval = repo.Add(new Session()
                    {
                        C_InstrumentId = 1,
                        SessionDateTimeUtc = DateTime.UtcNow,
                        Time = 300,
                        TimeZoneOffset = 300,
                        Title = "blah",
                        UserId = user.Id,
                        SessionId = 100
                    });

                }
                catch (ApplicationException ae)
                {
                    Assert.AreEqual("Session.SessionId must be zero", ae.Message);
                }
            }
        }

    }
}
