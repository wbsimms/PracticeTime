using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
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

        [TestMethod]
        public void UpdateDeleteTest()
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
                Assert.IsTrue(retval.SessionId > 0);
                retval.Title = "updated";
                repo.Update(retval);

                Session fromDb = repo.GetById(retval.SessionId);
                Assert.AreEqual(retval.Title,fromDb.Title);
                repo.Delete(fromDb);
                Session shouldBeNull = repo.GetById(retval.SessionId);
                Assert.IsNull(shouldBeNull);
            }
        }

        [TestMethod]
        public void GetAllTest()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("wbsimms").Result;

                SessionRepository repo = new SessionRepository();
                Session retval1 = repo.Add(new Session()
                {
                    C_InstrumentId = 1,
                    SessionDateTimeUtc = DateTime.UtcNow,
                    Time = 300,
                    TimeZoneOffset = 300,
                    Title = "blah",
                    UserId = user.Id
                });
                Session retval2 = repo.Add(new Session()
                {
                    C_InstrumentId = 1,
                    SessionDateTimeUtc = DateTime.UtcNow,
                    Time = 300,
                    TimeZoneOffset = 300,
                    Title = "blah",
                    UserId = user.Id
                });
                
                List<Session> sessions = repo.GetAll();
                Assert.IsTrue(sessions.Count > 0);

                sessions = repo.GetAllForUser(user.Id);
                Assert.IsNotNull(sessions);
                List<string> titles = repo.GetAllTitlesForUser(user.Id);
                List<string> alltitles = repo.GetAllTitles();
                Assert.IsNotNull(alltitles);

                Assert.IsNotNull(titles);
                Assert.IsTrue(titles.Count == 1);
            }
        }



    }
}
