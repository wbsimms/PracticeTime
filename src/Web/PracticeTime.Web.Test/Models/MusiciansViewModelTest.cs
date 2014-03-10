using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class MusiciansViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            MusiciansViewModel model = new MusiciansViewModel() {};
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.PublicUsers);
        }
    }
}
