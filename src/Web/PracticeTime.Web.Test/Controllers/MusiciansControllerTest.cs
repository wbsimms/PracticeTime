using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class MusiciansControllerTest
    {
        private Mock<IApplicationUserRepository> applicationUserRepository;

        [TestInitialize]
        public void Init()
        {
            applicationUserRepository = new Mock<IApplicationUserRepository>();
            applicationUserRepository.Setup(x => x.GetAppPublicProfiles()).Returns(() =>
            {
                return new List<ApplicationUser>()
                {
                    new ApplicationUser(){UserName = "foo",StudentPublicProfile = true,StudentToken = "fooToken"},
                    new ApplicationUser(){UserName = "bar",StudentPublicProfile = true,StudentToken = "barToken"},
                };
            }).Verifiable();
        }


        [TestMethod]
        public void ConstructorTest()
        {
            MusiciansController controller = new MusiciansController(applicationUserRepository.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void IndexTest()
        {
            MusiciansController controller = new MusiciansController(applicationUserRepository.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            MusiciansViewModel model = result.Model as MusiciansViewModel;
            Assert.IsNotNull(model);
        }
    }
}
