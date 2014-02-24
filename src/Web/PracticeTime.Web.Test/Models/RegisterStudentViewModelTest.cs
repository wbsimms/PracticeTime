using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class RegisterStudentViewModelTest
    {

        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod]
        public void ConstructorTest()
        {
            RegisterStudentViewModel model = new RegisterStudentViewModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void InitTest()
        {
            RegisterStudentViewModel model = new RegisterStudentViewModel();
            model.Init(new List<ApplicationUser>() { 
                new ApplicationUser() { FirstName = "blah1", LastName = "blah1-1", StudentToken = "blah1token" },
                new ApplicationUser() { FirstName = "blah2", LastName = "blah2-2", StudentToken = "blah2token" } });
            Assert.IsNotNull(model.RegisteredStudents);
            Assert.AreEqual(2, model.RegisteredStudents.Count);
            Assert.IsTrue(model.RegisteredStudents.Any(x => x.Text == "blah1-1, blah1"));
            Assert.IsTrue(model.RegisteredStudents.Any(x => x.Text == "blah2-2, blah2"));
        }
    }
}
