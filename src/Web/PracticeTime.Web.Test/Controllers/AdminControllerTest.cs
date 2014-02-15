﻿using System;
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
                    new ApplicationUser(){C_AccountTypeId = 2, UserName = "teacher1"},
                    new ApplicationUser(){C_AccountTypeId = 2, UserName = "teacher2"}
                };
            }).Verifiable();
            mockApplicationUserRepository.Setup(x => x.GetAllStudents()).Returns(() =>
            {
                return new List<ApplicationUser>()
                {
                    new ApplicationUser(){C_AccountTypeId = 1, UserName = "student1"},
                    new ApplicationUser(){C_AccountTypeId = 1, UserName = "student2"}
                };
            }).Verifiable();

            mockInstructorStudentRepository = new Mock<IInstructorStudentRepository>();
            mockInstructorStudentRepository.Setup(x => x.Add(It.IsAny<InstructorStudent>())).Returns(() => { return new InstructorStudent(){InstructorStudentId = 4}; }).Verifiable();
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
            ViewResult result = controller.Index() as ViewResult;
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
    }
}