﻿using System;
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

namespace PracticeTime.Web.Lib.Test
{
    [TestClass]
    public class BadgeRulesEngineTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            IBadgeRulesEngine engine = PracticeTimeLibResolver.Instance.Container.Resolve<IBadgeRulesEngine>();
            Assert.IsNotNull(engine);
            Assert.IsTrue(engine is BadgeRulesEngine);
        }

        [TestMethod]
        public void RunRulesTest()
        {
            var mockBadgeRepository = new Mock<IBadgeAwardRepository>();
            mockBadgeRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<BadgeAward>(); }).Verifiable();

            var mockFirstSession = new Mock<IFirstSessionRule>();
            var mockOneManBand = new Mock<IOneManBandRule>();
            var mockSongMaster = new Mock<ISongMasterRule>();
            var mockRepertoire = new Mock<IRepertoireRule>();
            mockFirstSession.Setup(x => x.Rule(It.IsAny<Session>(), It.IsAny<ResponseModel>())).Verifiable();
            mockOneManBand.Setup(x => x.Rule(It.IsAny<Session>(), It.IsAny<ResponseModel>())).Verifiable();
            mockSongMaster.Setup(x => x.Rule(It.IsAny<Session>(), It.IsAny<ResponseModel>())).Verifiable();
            mockRepertoire.Setup(x => x.Rule(It.IsAny<Session>(), It.IsAny<ResponseModel>())).Verifiable();
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IFirstSessionRule), mockFirstSession.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IOneManBandRule), mockOneManBand.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IBadgeAwardRepository), mockBadgeRepository.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(ISongMasterRule), mockSongMaster.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IRepertoireRule), mockRepertoire.Object);
            PracticeTimeLibResolver.Instance.Container.Resolve<IBadgeRulesEngine>()
                .RunRules(new Session() {SessionId = 1, C_InstrumentId = 1});
            mockFirstSession.VerifyAll();
            mockOneManBand.VerifyAll();
            mockBadgeRepository.VerifyAll();
            mockSongMaster.VerifyAll();
        }

        [TestMethod]
        public void RunRulesFirstTimeBadgeTest()
        {
            var mockBadgeRepository = new Mock<IBadgeAwardRepository>();
            mockBadgeRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() => { return new List<BadgeAward>(); }).Verifiable();

            var mockFirstSession = new Mock<IFirstSessionRule>();
            var mockOneManBand = new Mock<IOneManBandRule>();
            mockFirstSession.Setup(x => x.Rule(It.IsAny<Session>(), It.IsAny<ResponseModel>())).Callback((Session session, ResponseModel response) =>
            {
                response.HasNewBadges = true;
            }).Verifiable();
            mockOneManBand.Setup(x => x.Rule(It.IsAny<Session>(), It.IsAny<ResponseModel>())).Verifiable();
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IFirstSessionRule), mockFirstSession.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IOneManBandRule), mockOneManBand.Object);
            PracticeTimeLibResolver.Instance.Container.RegisterInstance(typeof(IBadgeAwardRepository), mockBadgeRepository.Object);
            PracticeTimeLibResolver.Instance.Container.Resolve<IBadgeRulesEngine>()
                .RunRules(new Session() { SessionId = 1, C_InstrumentId = 1 });
            mockFirstSession.VerifyAll();
            mockOneManBand.Verify(blah => blah.Rule(It.IsAny<Session>(),It.IsAny<ResponseModel>()),Times.Never);
            mockBadgeRepository.VerifyAll();
        }


        [TestCleanup]
        public void Cleanup()
        {
            PracticeTimeLibResolver.Instance.Reset();
        }
    }
}
