using System;
using System.Collections;
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
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private Mock<IUserHelper> mockUserHelper;
        private Mock<IApplicationUserRepository> mockApplicationUserRepository;
        private Mock<IInstructorStudentRepository> mockInstructorStudentRepository;

        [TestInitialize]
        public void Setup()
        {
            mockUserHelper = new Mock<IUserHelper>();
            mockApplicationUserRepository = new Mock<IApplicationUserRepository>();
            mockApplicationUserRepository.Setup(x => x.GetAllInstructors()).Returns(() =>
            {
                return new List<ApplicationUser>()
                {
                    new ApplicationUser(){UserName = "teacher1"},
                    new ApplicationUser(){UserName = "teacher2"}
                };
            }).Verifiable();
            mockApplicationUserRepository.Setup(x => x.GetAllStudents()).Returns(() =>
            {
                return new List<ApplicationUser>()
                {
                    new ApplicationUser(){UserName = "student1"},
                    new ApplicationUser(){UserName = "student2"}
                };
            }).Verifiable();

            mockInstructorStudentRepository = new Mock<IInstructorStudentRepository>();
            mockInstructorStudentRepository.Setup(x => x.Add(It.IsAny<InstructorStudent>())).Returns(() => { return new InstructorStudent(){InstructorStudentId = 4}; }).Verifiable();
            mockInstructorStudentRepository.Setup(x => x.GetAllForInstructor(It.IsAny<string>()))
                .Returns(() => { return new List<InstructorStudent>()
                {
                    new InstructorStudent() {Student = new ApplicationUser(){UserName = "student1",Id = "student1Id",FirstName = "stu",LastName = "dent1"},Instructor = new ApplicationUser()},
                    new InstructorStudent() {Student = new ApplicationUser(){UserName = "student2",Id = "student2Id",FirstName = "stu",LastName = "dent2"},Instructor = new ApplicationUser()}
                };
                }).Verifiable();
            mockInstructorStudentRepository.Setup(x => x.Delete(It.IsAny<InstructorStudent>())).Callback(() => { }).Verifiable();
        }

        [TestMethod]
        public void ConstructorTest()
        {
            AdminController controller = new AdminController(mockUserHelper.Object,
                mockApplicationUserRepository.Object,
                mockInstructorStudentRepository.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void IndexTest()
        {
            AdminController controller = new AdminController(mockUserHelper.Object,
                mockApplicationUserRepository.Object,
                mockInstructorStudentRepository.Object);
            ViewResult result = controller.Index(new AdminViewModel()) as ViewResult;
            Assert.IsTrue(result.Model is AdminViewModel);
            Assert.IsTrue(((AdminViewModel)result.Model).Instructors.Count == 2);
            Assert.IsTrue(((AdminViewModel)result.Model).Students.Count == 2);
            mockApplicationUserRepository.VerifyAll();
       }

        [TestMethod]
        public void AssociateTest()
        {
            AdminController controller = new AdminController(mockUserHelper.Object,
                mockApplicationUserRepository.Object,
                mockInstructorStudentRepository.Object);
            ViewResult result = (ViewResult)controller.Associate(new AdminViewModel() {SelectedInstructor = "werwe", SelectedStudent = "yuiyui"});
            mockInstructorStudentRepository.Verify(x => x.Add(It.IsAny<InstructorStudent>()),Times.Once);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(result.Model is AdminViewModel);
            AdminViewModel model = result.Model as AdminViewModel;
            Assert.AreEqual("Saved",model.Messages);
            Assert.IsFalse(model.HasErrors);
        }

        [TestMethod]
        public void AssociateAlreadySavedTest()
        {
            mockInstructorStudentRepository.Setup(x => x.Add(It.IsAny<InstructorStudent>())).Returns(() => null);
            AdminController controller = new AdminController(mockUserHelper.Object,
                mockApplicationUserRepository.Object,
                mockInstructorStudentRepository.Object);
            ViewResult result = (ViewResult)controller.Associate(new AdminViewModel() { SelectedInstructor = "werwe", SelectedStudent = "yuiyui" });
            mockInstructorStudentRepository.Verify(x => x.Add(It.IsAny<InstructorStudent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(result.Model is AdminViewModel);
            AdminViewModel model = result.Model as AdminViewModel;
            Assert.AreEqual("Already exists", model.Messages);
            Assert.IsFalse(model.HasErrors);
        }

        [TestMethod]
        public void GetInstructorStudentsTest()
        {
            AdminController controller = new AdminController(mockUserHelper.Object,
                mockApplicationUserRepository.Object,
                mockInstructorStudentRepository.Object);
            JsonResult result = controller.GetInstructorStudents("blah");
            Assert.IsNotNull(result);
            var retval = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Students>>(result.Data.ToString());
            Assert.IsNotNull(retval);
            Assert.AreEqual(2,retval.Count);
            Assert.IsTrue(retval.Any(x => x.StudentName == "student1"));
            Assert.IsTrue(retval.Any(x => x.StudentName == "student2"));
            Assert.IsTrue(retval.Any(x => x.StudentId == "student2Id"));
            Assert.IsTrue(retval.Any(x => x.StudentId == "student1Id"));
            Assert.IsTrue(retval.Any(x => x.FirstName == "stu"));
            Assert.IsTrue(retval.Any(x => x.LastName == "dent1"));
        }

        [TestMethod]
        public void DeleteAssociationTest()
        {
            AdminController controller = new AdminController(mockUserHelper.Object,
                mockApplicationUserRepository.Object,
                mockInstructorStudentRepository.Object);
            JsonResult result = controller.DeleteAssociation("student1Id","blah");
            Assert.IsNotNull(result);
            var retval = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Students>>(result.Data.ToString());
            Assert.IsNotNull(retval);
            mockInstructorStudentRepository.Verify(x => x.Delete(It.IsAny<InstructorStudent>()),Times.Once);
            mockInstructorStudentRepository.Verify(x => x.GetAllForInstructor(It.IsAny<string>()), Times.Once);

        }

        class Students
        {
            public string StudentName { get; set; }
            public string StudentId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
