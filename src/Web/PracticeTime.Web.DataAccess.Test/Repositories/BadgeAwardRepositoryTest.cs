﻿using System;
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
    public class BadgeAwardRepositoryTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            BadgeAwardRepository repo = new BadgeAwardRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void AddTest()
        {
            using (var scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("student").Result;

                BadgeAwardRepository repo = new BadgeAwardRepository();
                BadgeAward award =
                    repo.Add(new BadgeAward() {AwardDate = DateTime.UtcNow, C_BadgeId = 1, UserId = user.Id});
                Assert.IsTrue(award.BadgeAwardId > 0);
                Assert.IsNotNull(award.C_Badge);
                Assert.IsNotNull(award.C_Badge.Name);
                scope.Dispose();
            }
        }

        [TestMethod]
        public void AddExistingIdTest()
        {
            using (var scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("student").Result;

                BadgeAwardRepository repo = new BadgeAwardRepository();
                try
                {
                    BadgeAward award =
                        repo.Add(new BadgeAward()
                        {
                            BadgeAwardId = 4,
                            AwardDate = DateTime.UtcNow,
                            C_BadgeId = 1,
                            UserId = user.Id
                        });
                }
                catch (ApplicationException ae)
                {
                    Assert.AreEqual("BadgeAward.BadgeAwardId must be zero",ae.Message);
                }
                scope.Dispose();
            }
        }

        [TestMethod]
        public void UpdateNoIdTest()
        {
            using (var scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("student").Result;

                BadgeAwardRepository repo = new BadgeAwardRepository();
                try
                {
                        repo.Update(new BadgeAward()
                        {
                            BadgeAwardId = 0,
                            AwardDate = DateTime.UtcNow,
                            C_BadgeId = 1,
                            UserId = user.Id
                        });
                }
                catch (ApplicationException ae)
                {
                    Assert.AreEqual("Id must be greather than 0", ae.Message);
                }
                scope.Dispose();
            }
        }

        [TestMethod]
        public void UpdateNoBadgeIdTest()
        {
            using (var scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("student").Result;

                BadgeAwardRepository repo = new BadgeAwardRepository();
                try
                {
                    repo.Update(new BadgeAward()
                    {
                        BadgeAwardId = 999999,
                        AwardDate = DateTime.UtcNow,
                        C_BadgeId = 1,
                        UserId = user.Id
                    });
                }
                catch (ApplicationException ae)
                {
                    Assert.AreEqual("BadgeAward not found: 999999", ae.Message);
                }
                scope.Dispose();
            }
        }


        [TestMethod]
        public void UpdateAndDeleteTest()
        {
            using (var scope = new TransactionScope())
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new PracticeTimeContext());
                ApplicationUser user = store.FindByNameAsync("student").Result;

                BadgeAwardRepository repo = new BadgeAwardRepository();
                BadgeAward award =
                    repo.Add(new BadgeAward() { AwardDate = DateTime.UtcNow, C_BadgeId = 1, UserId = user.Id });
                BadgeAward award2 =
                    repo.Add(new BadgeAward() { AwardDate = DateTime.UtcNow, C_BadgeId = 1, UserId = user.Id });

                Assert.IsTrue(award.BadgeAwardId > 0);
                Assert.IsNotNull(award.C_Badge);
                Assert.IsNotNull(award.C_Badge.Name);

                List<BadgeAward> all = repo.GetAll();
                Assert.IsNotNull(all);
                Assert.IsTrue(all.Count > 1);

                List<BadgeAward> allUsersAwards = repo.GetAllForUser(user.Id);
                Assert.IsNotNull(allUsersAwards);
                Assert.IsTrue(allUsersAwards.Count > 1);

                award.AwardDate = DateTime.UtcNow.AddDays(-1);
                repo.Update(award);

                BadgeAward fromDb = repo.GetById(award.BadgeAwardId);
                Assert.IsNotNull(fromDb.ToString());
                Assert.AreEqual(award.AwardDate.ToLongDateString(),fromDb.AwardDate.ToLongDateString());

                repo.Delete(fromDb);
                BadgeAward deleted = repo.GetById(award.BadgeAwardId);
                Assert.IsNull(deleted);
            }
        }
    }
}
