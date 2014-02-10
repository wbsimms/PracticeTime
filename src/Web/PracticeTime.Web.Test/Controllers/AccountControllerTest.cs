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

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private Mock<IAccountTypeRepository> accountTypesMock;

        [TestInitialize]
        public void Setup()
        {
            accountTypesMock = new Mock<IAccountTypeRepository>();
            accountTypesMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<C_AccountType>
                {
                    new C_AccountType() {Name = "blah", Active = true},
                    new C_AccountType() {Name = "blah2", Active = true}
                };
            });

        }

        [TestMethod]
        public void ConstructorTest()
        {
            AccountController controller = new AccountController(accountTypesMock.Object);
            controller.ControllerContext = new TestControllerContext();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void LoginTest()
        {
            AccountController controller = new AccountController(accountTypesMock.Object);
            controller.ControllerContext = new TestControllerContext();
            ViewResult view = (ViewResult)controller.Login("http://cnn.com");
            Assert.AreEqual("http://cnn.com",view.ViewBag.ReturnUrl);
            Assert.IsNotNull(view);
        }

    }
}
