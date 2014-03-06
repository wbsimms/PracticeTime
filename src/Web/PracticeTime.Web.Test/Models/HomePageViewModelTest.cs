using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Repositories;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class HomePageViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            HomePageViewModel model = new HomePageViewModel(){TopUsersThisWeek = new List<UserData>()};
            Assert.IsNotNull(model.TopUsersThisWeek);

        }
    }
}
