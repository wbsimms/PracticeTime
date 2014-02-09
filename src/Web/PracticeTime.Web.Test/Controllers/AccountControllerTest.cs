using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeTime.Web.Controllers;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            AccountController controller = new AccountController();
            controller.ControllerContext = new TestControllerContext();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void LoginTest()
        {
            AccountController controller = new AccountController();
            controller.ControllerContext = new TestControllerContext();
            ViewResult view = (ViewResult)controller.Login("http://cnn.com");
            Assert.AreEqual("http://cnn.com",view.ViewBag.ReturnUrl);
            Assert.IsNotNull(view);
        }

    }
}
