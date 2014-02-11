using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Lib.BadgeRules;

namespace PracticeTime.Web.Lib.Test.BadgeRules
{
    [TestClass]
    public class SongMasterRuleTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>(); });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1 }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object,badgeAwardRepository.Object);
            Assert.IsNotNull(rule);
        }

        [TestMethod]
        public void RuleNoBadgeTest()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>(); });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1 }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            rule.Rule(new Session(),response);
            Assert.IsFalse(response.HasNewBadges);
        }

        [TestMethod]
        public void RuleLevel1Test()
        {
            SongMasterRuleTester(1, 3);
        }

        [TestMethod]
        public void RuleLevel2Test()
        {
            SongMasterRuleTester(2, 4);
        }

        [TestMethod]
        public void RuleLevel3Test()
        {
            SongMasterRuleTester(3, 5);
        }

        [TestMethod]
        public void RuleLevel4Test()
        {
            SongMasterRuleTester(4,6);
        }

        [TestMethod]
        public void RuleLevel5Test()
        {
            SongMasterRuleTester(5,7);
        }

        [TestMethod]
        public void RuleLevel6_9Test()
        {
            SongMasterRuleTester(6, 8);
            SongMasterRuleTester(7, 9);
            SongMasterRuleTester(8, 10);
            SongMasterRuleTester(9, 11);
        }


        public void SongMasterRuleTester(int sessionCount, int badgeIdToReturn)
        {
            int id = badgeIdToReturn;
            var sessionRepository = new Mock<ISessionRepository>();

            List<Session> sessions = new List<Session>();
            for (int i = 0; i < sessionCount; i++)
            {
                sessions.Add(new Session() { Title = "blah"+i, Time = 300 });
            }

            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return sessions;
            });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1, C_BadgeId = id }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            response.Badges = new List<BadgeAward>()
            {
                new BadgeAward() {C_BadgeId = 2},
            };
            rule.Rule(new Session(), response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(id, response.NewBadges.First().C_BadgeId);
        }
    }
}
