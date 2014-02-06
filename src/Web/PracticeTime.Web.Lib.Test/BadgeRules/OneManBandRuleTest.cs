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
    }
}
