using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
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
                    SessionId = 1,Time = 20,Title= "blah", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1
                }, new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah2", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1
                }};
            });

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object,mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            JsonResult retval = (JsonResult)controller.GetSessionsForUserGraph();
            string json = retval.Data as string;
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void GetSessionsForUserGraphTitlesTest()
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
                    SessionId = 1,Time = 20,Title= "blah", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1
                }, new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah2", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1
                }};
            });

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            JsonResult retval = (JsonResult)controller.GetSessionsForUserGraphTitle();
            string json = retval.Data as string;
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void IndexTest()
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
                    SessionId = 1,Time = 20,Title= "blah", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1
                }, new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah2", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1
                }};
            });

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult retval = (ViewResult)controller.Index();
            Assert.IsNotNull(retval.Model);
            Assert.IsNotNull(retval);
        }


    }
}
