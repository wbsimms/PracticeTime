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
    public class InstructorControllerTest
    {
        Mock<IUserHelper> mockUserHelper = new Mock<IUserHelper>();
        Mock<IInstrumentRepository> mockInstrumentRepository = new Mock<IInstrumentRepository>();
        Mock<ISessionRepository> mockSessionRepository = new Mock<ISessionRepository>();
        Mock<IBadgeRulesEngine> mockBadgeRulesEngine = new Mock<IBadgeRulesEngine>();


        [TestInitialize]
        public void Setup()
        {
            mockUserHelper.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(() =>
            {
                return "userid";
            });
            mockSessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>() {new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1,C_Instrument = new C_Instrument(){Name="Guitar"}
                }, new Session()
                {
                    SessionId = 1,Time = 20,Title= "blah2", SessionDateTimeUtc = DateTime.UtcNow, C_InstrumentId = 1,C_Instrument = new C_Instrument(){Name="Guitar"}
                }};
            });
        }

        [TestMethod]
        public void ConstructorTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockBadgeRulesEngine.Object,
                mockInstrumentRepository.Object,
                mockUserHelper.Object);
            Assert.IsNotNull(controller);
        }
    }
}
