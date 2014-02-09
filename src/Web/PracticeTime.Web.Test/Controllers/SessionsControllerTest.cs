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
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class SessionsControllerTest
    {
        Mock<IUserHelper> mockUserHelper = new Mock<IUserHelper>();
        Mock<IInstrumentRepository> stubIInstrumentRepository = new Mock<IInstrumentRepository>();
        Mock<ISessionRepository> stub = new Mock<ISessionRepository>();
        Mock<IBadgeRulesEngine> mockBadgeRulesEngine = new Mock<IBadgeRulesEngine>();


        [TestInitialize]
        public void Setup()
        {
            mockUserHelper.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(() =>
            {
                return "userid";
            });
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
        }

        [TestMethod]
        public void ConstructorTest()
        {

            SessionsController controller = new SessionsController(stub.Object,mockBadgeRulesEngine.Object,stubIInstrumentRepository.Object,mockUserHelper.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void GetSessionsForUserGraphTest()
        {

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object,mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            JsonResult retval = (JsonResult)controller.GetSessionsForUserGraph();
            string json = retval.Data as string;
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void GetSessionsForUserGraphTitlesTest()
        {
            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            JsonResult retval = (JsonResult)controller.GetSessionsForUserGraphTitle();
            string json = retval.Data as string;
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void IndexTest()
        {
            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult retval = (ViewResult)controller.Index();
            Assert.IsNotNull(retval.Model);
            Assert.IsNotNull(retval);
        }

        [TestMethod]
        public void AddTest()
        {
            stub.Setup(x => x.Add(It.IsAny<Session>())).Returns(() => { return new Session(); });
            stubIInstrumentRepository.Setup(x => x.GetAll()).Returns(() => { return new List<C_Instrument>(); });
            mockBadgeRulesEngine.Setup(x => x.RunRules(It.IsAny<Session>()))
                .Returns(() => { return new ResponseModel(); });

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult result = (ViewResult)controller.Add(new SessionEntryViewModel() { SelectedInstrumentId = 1, Title = "blah", TimeZoneOffset = 300, Time = 25 });
            SessionEntryViewModel model = (SessionEntryViewModel)result.Model;
            Assert.AreEqual("Session Saved",model.StateMessage);
        }

        [TestMethod]
        public void AddNewBadgeTest()
        {
            stub.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>();
            });
            stub.Setup(x => x.Add(It.IsAny<Session>())).Returns(() => { return new Session(); });

            stubIInstrumentRepository.Setup(x => x.GetAll()).Returns(() => { return new List<C_Instrument>(); });

            mockBadgeRulesEngine.Setup(x => x.RunRules(It.IsAny<Session>()))
                .Returns(() => { return new ResponseModel() {HasNewBadges = true, NewBadges = new List<BadgeAward>()
                {
                    new BadgeAward(){C_BadgeId = 1}
                }}; });

            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult result = (ViewResult)controller.Add(new SessionEntryViewModel() { SelectedInstrumentId = 1, Title = "blah", TimeZoneOffset = 300, Time = 25 });
            SessionEntryViewModel model = (SessionEntryViewModel)result.Model;
            Assert.AreEqual("Session Saved", model.StateMessage);
            Assert.IsNotNull(model.BadgeAward);
        }

        [TestMethod]
        public void IndexViewBagTest()
        {
            SessionsController controller = new SessionsController(stub.Object, mockBadgeRulesEngine.Object, stubIInstrumentRepository.Object, mockUserHelper.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult view = (ViewResult)controller.Index();
            Assert.IsNotNull(view);
            Assert.AreEqual(null, view.ViewBag.ReturnUrl);
        }
   }
}
