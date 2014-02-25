using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class InstructorControllerTest
    {
        private Mock<IUserHelper> mockUserHelper = new Mock<IUserHelper>();
        private Mock<ISessionRepository> mockSessionRepository = new Mock<ISessionRepository>();
        private Mock<IInstructorStudentRepository> mockInstructorStudentRepository = new Mock<IInstructorStudentRepository>();
        private Mock<IApplicationUserRepository> mockApplicationUserRepository = new Mock<IApplicationUserRepository>();


        [TestInitialize]
        public void Setup()
        {
            mockInstructorStudentRepository.Setup(x => x.GetAllForInstructor(It.IsAny<string>())).Returns(() =>
            {
                return new List<InstructorStudent>()
                {
                    new InstructorStudent(){InstructorId = "teacherid",StudentId = "studentid",Student = new ApplicationUser(){UserName = "student",FirstName = "stu1",LastName = "dent1"}},
                    new InstructorStudent(){InstructorId = "teacherid",StudentId = "student2id",Student = new ApplicationUser(){UserName = "student2",FirstName = "stu2",LastName = "dent2"}}
                };
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

            mockApplicationUserRepository.Setup(x => x.GetUserByToken(It.IsAny<string>()))
                .Returns(new ApplicationUser() {UserName = "student", FirstName = "stu1", LastName = "dent1"}).Verifiable();

            mockInstructorStudentRepository.Setup(x=> x.Add(It.IsAny<InstructorStudent>())).Returns(new InstructorStudent(){InstructorId = "99"}).Verifiable();
        }

        [TestMethod]
        public void ConstructorTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object,
                mockApplicationUserRepository.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void IndexTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object,
                mockApplicationUserRepository.Object);
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
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object,
                mockApplicationUserRepository.Object);
            controller.ControllerContext = new TestControllerContext() { UserName = "teacher" };
            JsonResult jsonResult = controller.GetSessionsForStudent("student");
            mockSessionRepository.Verify(x => x.GetAllForUser(It.IsAny<string>()),Times.Once);
            Assert.IsNotNull(jsonResult);
            List<Session> sessions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Session>>(jsonResult.Data.ToString());
            Assert.IsNotNull(sessions);
            Assert.AreEqual(2,sessions.Count);
        }

        [TestMethod]
        public void RegisterStudentsTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object,
                mockApplicationUserRepository.Object);
            controller.ControllerContext = new TestControllerContext() { UserName = "teacher" };
            ViewResult result = controller.RegisterStudents(new RegisterStudentViewModel()) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is RegisterStudentViewModel);
            RegisterStudentViewModel model = result.Model as RegisterStudentViewModel;
            Assert.IsNotNull(model.RegisteredStudents);
            Assert.AreEqual(2,model.RegisteredStudents.Count);
            Assert.IsTrue(model.RegisteredStudents.Any(x => x.Text == "dent1, stu1"));
            Assert.IsTrue(model.RegisteredStudents.Any(x => x.Text == "dent2, stu2"));
            mockInstructorStudentRepository.Verify(x => x.GetAllForInstructor(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void RegisterStudentTest()
        {
            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object,
                mockApplicationUserRepository.Object);
            controller.ControllerContext = new TestControllerContext() { UserName = "teacher" };
            JsonResult result = controller.RegisterStudent(new RegisterStudentViewModel(){StudentTokenForRegistration = "studentToken"}) as JsonResult;
            Assert.IsNotNull(result);
            ResponseMessage message =
                Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMessage>(result.Data as string);
            Assert.IsNotNull(message);
            Assert.IsFalse(message.HasErrors);
            mockApplicationUserRepository.Verify(x => x.GetUserByToken(It.IsAny<string>()),Times.Once);
            mockInstructorStudentRepository.Verify(x => x.Add(It.IsAny<InstructorStudent>()),Times.Once);
        }

        [TestMethod]
        public void RegisterStudentFailedTest()
        {
            mockInstructorStudentRepository.Setup(x => x.Add(It.IsAny<InstructorStudent>())).Returns(() =>
            {
                return null;
            }).Verifiable();
            mockApplicationUserRepository.Setup(x => x.GetUserByToken(It.IsAny<string>()))
                .Returns(() => { return null; }).Verifiable();


            InstructorController controller = new InstructorController(
                mockSessionRepository.Object,
                mockUserHelper.Object,
                mockInstructorStudentRepository.Object,
                mockApplicationUserRepository.Object);
            controller.ControllerContext = new TestControllerContext() { UserName = "teacher" };
            JsonResult result = controller.RegisterStudent(new RegisterStudentViewModel() { StudentTokenForRegistration = "studentToken" }) as JsonResult;
            Assert.IsNotNull(result);
            ResponseMessage message =
                Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMessage>(result.Data as string);
            Assert.IsNotNull(message);
            Assert.IsTrue(message.HasErrors);
            mockApplicationUserRepository.Verify(x => x.GetUserByToken(It.IsAny<string>()), Times.Once);
        }

    }
}
