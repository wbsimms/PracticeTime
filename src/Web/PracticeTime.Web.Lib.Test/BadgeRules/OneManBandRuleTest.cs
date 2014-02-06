using System;
using System.Collections.Generic;
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
    public class OneManBandRuleTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>(); });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1 }; });

            OneManBandRule rule = new OneManBandRule(sessionRepository.Object,badgeAwardRepository.Object);
            Assert.IsNotNull(rule);
        }

        [TestMethod]
        public void RuleNoBadgeTest()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>()))
                .Returns(() =>
                {
                    return new List<Session>()
                    {
                        new Session(){SessionId = 1,C_InstrumentId = 1},
                        new Session(){SessionId = 2,C_InstrumentId = 2},
                        new Session(){SessionId = 2,C_InstrumentId = 1},
                    };
                });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1 }; }).Verifiable();

            OneManBandRule rule = new OneManBandRule(sessionRepository.Object, badgeAwardRepository.Object);
            ResponseModel response = new ResponseModel();
            rule.Rule(new Session(){SessionId = 4,C_InstrumentId = 2},response);
            Assert.IsFalse(response.HasNewBadges);
            Assert.AreEqual(0,response.NewBadges.Count);
        }

        [TestMethod]
        public void RuleTest()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>()))
                .Returns(() =>
                {
                    return new List<Session>()
                    {
                        new Session(){SessionId = 1,C_InstrumentId = 1},
                        new Session(){SessionId = 2,C_InstrumentId = 2},
                        new Session(){SessionId = 3,C_InstrumentId = 3},
                    };
                });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1 }; }).Verifiable();

            OneManBandRule rule = new OneManBandRule(sessionRepository.Object, badgeAwardRepository.Object);
            ResponseModel response = new ResponseModel();
            rule.Rule(new Session() { SessionId = 4, C_InstrumentId = 2 }, response);
            Assert.IsTrue(response.HasNewBadges);
            Assert.AreEqual(1, response.NewBadges.Count);
        }

        [TestMethod]
        public void RuleHasBadgeTest()
        {
            var sessionRepository = new Mock<ISessionRepository>();
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>()))
                .Returns(() =>
                {
                    return new List<Session>()
                    {
                        new Session(){SessionId = 1,C_InstrumentId = 1},
                        new Session(){SessionId = 2,C_InstrumentId = 2},
                        new Session(){SessionId = 3,C_InstrumentId = 3},
                    };
                });

            var badgeAwardRepository = new Mock<IBadgeAwardRepository>();
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() => { return new BadgeAward() { BadgeAwardId = 1 }; }).Verifiable();

            OneManBandRule rule = new OneManBandRule(sessionRepository.Object, badgeAwardRepository.Object);
            ResponseModel response = new ResponseModel() {Badges = new List<BadgeAward>(){ new BadgeAward(){C_BadgeId = 2}}};
            rule.Rule(new Session() { SessionId = 4, C_InstrumentId = 2 }, response);
            Assert.IsFalse(response.HasNewBadges);
            Assert.AreEqual(0, response.NewBadges.Count);
        }


    }
}
