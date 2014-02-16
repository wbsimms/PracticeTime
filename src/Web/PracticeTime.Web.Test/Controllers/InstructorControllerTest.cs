using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    public class InstructorControllerTest
    {
        private Mock<IUserHelper> mockUserHelper = new Mock<IUserHelper>();
        private Mock<IInstrumentRepository> mockInstrumentRepository = new Mock<IInstrumentRepository>();
        private Mock<ISessionRepository> mockSessionRepository = new Mock<ISessionRepository>();
        private Mock<IBadgeRulesEngine> mockBadgeRulesEngine = new Mock<IBadgeRulesEngine>();
        private Mock<IInstructorStudentRepository> mockInstructorStudentRepository = new Mock<IInstructorStudentRepository>();


        [TestInitialize]
        public void Setup()
        {
            mockInstructorStudentRepository.Setup(x => x.GetAllForInstructor(It.IsAny<string>())).Returns(() =>
            {
                return new List<InstructorStudent>()
                {
                    new InstructorStudent(){InstructorId = "teacherid",StudentId = "studentid",Student = new ApplicationUser(){UserName = "student"}},
                    new InstructorStudent(){InstructorId = "teacherid",StudentId = "student2id",Student = new ApplicationUser(){UserName = "student2"}}
                };
            }).Verifiable();

            mockUserHelper.Setup(x => x.GetUserId(It.IsAny<string>())).Returns(() =>
            {
                return "userid";
            }).Verifiable();
            mockSessionRepository.Setup(x => x.GetAllForUser(It.IsAny<string>())).Returns(() =>
            {
                return new List<Session>()
                {
                    new Session()
                    {
                        SessionId = 1,
                        Time = 20,
                        Title = "blah",
                        SessionDateTimeUtc = DateTime.UtcNow,
                        C_InstrumentId = 1,
                        C_Instrument = new C_Instrument() {Name = "Guitar"}
                    },
                    new Session()
                    {
                        SessionId = 1,
                        Time = 20,
                        Title = "blah2",
                        SessionDateTimeUtc = DateTime.UtcNow,
                        C_InstrumentId = 1,
                        C_Instrument = new C_Instrument() {Name = "Guitar"}
                    }
                };
            }).Verifiable();
        }

        [TestMethod]
        public void ConstructorTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockBadgeRulesEngine.Object,
                mockInstrumentRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void IndexTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockBadgeRulesEngine.Object,
                mockInstrumentRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object);
            controller.ControllerContext = new TestControllerContext(){UserName = "teacher"};
            ViewResult result = (ViewResult)controller.Index();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is InstructorViewModel);
            Assert.AreEqual(2,((InstructorViewModel)result.Model).Students.Count);
            mockInstructorStudentRepository.Verify(x => x.GetAllForInstructor(It.IsAny<string>()),Times.Once);
        }

        [TestMethod]
        public void GetSessionsForStudentTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockBadgeRulesEngine.Object,
                mockInstrumentRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object);
            controller.ControllerContext = new TestControllerContext() { UserName = "teacher" };
            JsonResult jsonResult = controller.GetSessionsForStudent("student");
            mockSessionRepository.Verify(x => x.GetAllForUser(It.IsAny<string>()),Times.Once);
            Assert.IsNotNull(jsonResult);
            List<Session> sessions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Session>>(jsonResult.Data.ToString());
            Assert.IsNotNull(sessions);
            Assert.AreEqual(2,sessions.Count);
        }


    }
}
