using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Lib.BadgeRules;

namespace PracticeTime.Web.Lib.Test.BadgeRules
{
    [TestClass]
    public class RepertoireRuleTest
    {
        private Mock<ISessionRepository> sessionRepository = new Mock<ISessionRepository>();
        private Mock<IBadgeAwardRepository> badgeAwardRepository = new Mock<IBadgeAwardRepository>();

        [TestInitialize]
        public void Initalize()
        {
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>(); });
            badgeAwardRepository.Setup(x => x.Add(It.IsAny<BadgeAward>())).Returns(() =>
            {
                return new BadgeAward() {C_BadgeId = RepertoireRule.KeyId};
            }).Verifiable();

            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(ISessionRepository), sessionRepository.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IBadgeAwardRepository), badgeAwardRepository.Object);
        }

        [TestMethod]
        public void ConstrutorTest()
        {
            RepertoireRule rule = new RepertoireRule(sessionRepository.Object,badgeAwardRepository.Object);
            Assert.IsNotNull(rule);

            IRepertoireRule iRule = PracticeTimeLibResolver.Instance.Container.Resolve<IRepertoireRule>();
            Assert.IsNotNull(iRule);
        }

        [TestMethod]
        public void RuleDoesntApplyTest()
        {
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<Session>()
            {
                new Session(){Title = "1"},
                new Session(){Title = "2"},
                new Session(){Title = "3"},
                new Session(){Title = "4"},
                new Session(){Title = "5"},
                new Session(){Title = "6"},
                new Session(){Title = "7"},
                new Session(){Title = "8"},
                new Session(){Title = "9"},
            };
            });
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof (ISessionRepository),
                sessionRepository.Object);
            IRepertoireRule rule = PracticeTimeLibResolver.Instance.Container.Resolve<IRepertoireRule>();
            ResponseModel model = new ResponseModel();
            rule.Rule(new Session(), model);
            Assert.IsFalse(model.HasNewBadges);
        }

        [TestMethod]
        public void RuleAppliesTest()
        {
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>()
            {
                new Session(){Title = "1"},
                new Session(){Title = "2"},
                new Session(){Title = "3"},
                new Session(){Title = "4"},
                new Session(){Title = "5"},
                new Session(){Title = "6"},
                new Session(){Title = "7"},
                new Session(){Title = "8"},
                new Session(){Title = "9"},
                new Session(){Title = "10"},
            };
            });
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(ISessionRepository),
                sessionRepository.Object);
            IRepertoireRule rule = PracticeTimeLibResolver.Instance.Container.Resolve<IRepertoireRule>();
            ResponseModel model = new ResponseModel();
            rule.Rule(new Session(), model);
            Assert.IsTrue(model.HasNewBadges);
            Assert.IsTrue(model.NewBadges.Count == 1);
            Assert.AreEqual(RepertoireRule.KeyId, model.NewBadges.First().C_BadgeId);
            Assert.AreEqual(0, model.Badges.Count);
        }

        [TestMethod]
        public void RuleHasBadgeTest()
        {
            sessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>()
                {
                    new Session(){Title = "1"},
                    new Session(){Title = "2"},
                    new Session(){Title = "3"},
                    new Session(){Title = "4"},
                    new Session(){Title = "5"},
                    new Session(){Title = "6"},
                    new Session(){Title = "7"},
                    new Session(){Title = "8"},
                    new Session(){Title = "9"},
                    new Session(){Title = "10"},
                };
            });

            badgeAwardRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<BadgeAward>()
                {
                    new BadgeAward() {C_BadgeId = RepertoireRule.KeyId }
                };
            }).Verifiable();

            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(ISessionRepository),
                sessionRepository.Object);
            IRepertoireRule rule = PracticeTimeLibResolver.Instance.Container.Resolve<IRepertoireRule>();
            ResponseModel model = new ResponseModel(){Badges = badgeAwardRepository.Object.GetAllForUser("")};
            rule.Rule(new Session(), model);
            Assert.IsFalse(model.HasNewBadges);
            Assert.IsTrue(model.NewBadges.Count == 0);
            Assert.AreEqual(1, model.Badges.Count);
            sessionRepository.Verify(x => x.GetAllForUser(It.IsAny<string>()),Times.Never);
        }

 
    }
}
