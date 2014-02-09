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
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>(){new Session(){Title = "blah", Time = 300}}; });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1,C_BadgeId = 3}; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            rule.Rule(new Session(), response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(3,response.NewBadges.First().C_BadgeId);
        }

        [TestMethod]
        public void RuleLevel2Test()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>()
            {
                new Session() { Title = "blah", Time = 300 },
                new Session() { Title = "blah2", Time = 300 }
            }; });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1, C_BadgeId = 4 }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            response.Badges = new List<BadgeAward>()
            {
                new BadgeAward() {C_BadgeId = 3},
                new BadgeAward() {C_BadgeId = 2},
            };
            rule.Rule(new Session(), response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(4, response.NewBadges.First().C_BadgeId);
        }

        [TestMethod]
        public void RuleLevel3Test()
        {
            int id = 5;
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>()
            {
                new Session() { Title = "blah", Time = 300 },
                new Session() { Title = "blah2", Time = 300 },
                new Session() { Title = "blah3", Time = 300 }
            };
            });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1, C_BadgeId = id }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            response.Badges = new List<BadgeAward>()
            {
                new BadgeAward() {C_BadgeId = 3},
                new BadgeAward() {C_BadgeId = 2},
            };
            rule.Rule(new Session(), response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(id, response.NewBadges.First().C_BadgeId);
        }

        [TestMethod]
        public void RuleLevel4Test()
        {
            int id = 6;
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>()
            {
                new Session() { Title = "blah", Time = 300 },
                new Session() { Title = "blah2", Time = 300 },
                new Session() { Title = "blah4", Time = 300 },
                new Session() { Title = "blah3", Time = 300 }
            };
            });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1, C_BadgeId = id }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            response.Badges = new List<BadgeAward>()
            {
                new BadgeAward() {C_BadgeId = 3},
                new BadgeAward() {C_BadgeId = 2},
            };
            rule.Rule(new Session(), response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(id, response.NewBadges.First().C_BadgeId);
        }

        [TestMethod]
        public void RuleLevel5Test()
        {
            int id = 7;
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>()
            {
                new Session() { Title = "blah", Time = 300 },
                new Session() { Title = "blah2", Time = 300 },
                new Session() { Title = "blah4", Time = 300 },
                new Session() { Title = "blah5", Time = 300 },
                new Session() { Title = "blah3", Time = 300 }
            };
            });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1, C_BadgeId = id }; });

            SongMasterRule rule = new SongMasterRule(sessionRepository.Object, badgeAwardRepository.Object);

            ResponseModel response = new ResponseModel();
            response.Badges = new List<BadgeAward>()
            {
                new BadgeAward() {C_BadgeId = 3},
                new BadgeAward() {C_BadgeId = 2},
            };
            rule.Rule(new Session(), response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(id, response.NewBadges.First().C_BadgeId);
        }

    }
}
