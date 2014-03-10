using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class MusiciansControllerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            MusiciansController controller = new MusiciansController();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void IndexTest()
        {
            MusiciansController controller = new MusiciansController();
            controller.ControllerContext = new TestControllerContext();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            MusiciansViewModel model = result.Model as MusiciansViewModel;
            Assert.IsNotNull(model);
        }
    }
}
