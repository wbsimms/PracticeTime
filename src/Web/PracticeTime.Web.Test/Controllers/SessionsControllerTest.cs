using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Helpers;
using PracticeTime.Web.Lib;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class SessionsControllerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Mock<IUserHelper> mockUserHelper = new Mock<IUserHelper>();
            mockUserHelper.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(() =>
            {
                return "userid";
            });
            Mock<IInstrumentRepository> stubIInstrumentRepository = new Mock<IInstrumentRepository>();
            Mock<ISessionRepository> stub = new Mock<ISessionRepository>();
            Mock<IBadgeRulesEngine> mockBadgeRulesEngine = new Mock<IBadgeRulesEngine>();
            stub.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>() {new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah"
                }, new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah2"
                }};
            });

            SessionsController controller = new SessionsController(stub.Object,mockBadgeRulesEngine.Object,stubIInstrumentRepository.Object,mockUserHelper.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void GetSessionsForUserGraphTest()
        {
            Mock<IUserHelper> mockUserHelper = new Mock<IUserHelper>();
            mockUserHelper.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(() =>
            {
                return "userid";
            });
            Mock<IInstrumentRepository> stubIInstrumentRepository = new Mock<IInstrumentRepository>();
            Mock<ISessionRepository> stub = new Mock<ISessionRepository>();
            Mock<IBadgeRulesEngine> mockBadgeRulesEngine = new Mock<IBadgeRulesEngine>();
            stub.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>() {new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah"
                }, new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah2"
                }};
            });

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object,mockUserHelper.Object);
            Assert.IsNotNull(controller);
        }

    }
}
