using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.DataAccess.Repositories.Fakes;
using PracticeTime.Web.Lib;
using PracticeTime.Web.Lib.BadgeRules;

namespace PracticeTime.Web.Lib.Test.BadgeRules
{
    [TestClass]
    public class FirstSessionRuleTest
    {

        [TestMethod]
        public void ConstructorTest()
        {
            StubISessionRepository sessionRepository = new StubISessionRepository();
            sessionRepository.GetAllForUserString = s => { return new List<Session>(); };

            StubIBadgeAwardRepository badgeAwardRepository = new StubIBadgeAwardRepository();
            badgeAwardRepository.AddBadgeAward = award => { return new BadgeAward() {BadgeAwardId = 1};  };

            FirstSessionRule firstSessionRule = new FirstSessionRule(sessionRepository,badgeAwardRepository);
            Assert.IsNotNull(firstSessionRule);
        }

        [TestMethod]
        public void RuleTest()
        {
            StubISessionRepository sessionRepository = new StubISessionRepository();
            sessionRepository.GetAllForUserString = s =>
            {
                return new List<Session>() {new Session(){SessionId = 1}};
            };

            StubIBadgeAwardRepository badgeAwardRepository = new StubIBadgeAwardRepository();
            badgeAwardRepository.AddBadgeAward = award =>
            {
                return new BadgeAward() { BadgeAwardId = 1, C_BadgeId = 1};
            };

            FirstSessionRule firstSessionRule = new FirstSessionRule(sessionRepository, badgeAwardRepository);
            Assert.IsNotNull(firstSessionRule);
            Session testSession = new Session() {SessionId = 1};
            ResponseModel responseModel = new ResponseModel();
            firstSessionRule.Rule(testSession,responseModel);
            Assert.IsTrue(responseModel.HasNewBadges);
            Assert.IsTrue(responseModel.NewBadges.FirstOrDefault(x =>x.C_BadgeId == 1) != null);
            Assert.IsFalse(responseModel.Badges.Any());
        }

    }
}
