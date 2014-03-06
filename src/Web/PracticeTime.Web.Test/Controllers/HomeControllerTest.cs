using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Repositories;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<ISessionRepository> mockSessionRepository;
        [TestInitialize]
        public void Setup()
        {
            mockSessionRepository = new Mock<ISessionRepository>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(mockSessionRepository.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(mockSessionRepository.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Thanks for wanting to learn more.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(mockSessionRepository.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
