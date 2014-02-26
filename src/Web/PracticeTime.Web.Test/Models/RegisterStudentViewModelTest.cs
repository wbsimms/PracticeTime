using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
        public void AddRegisteredStudentsTest()
        {
            RegisterStudentViewModel model = new RegisterStudentViewModel();
            model.RegisteredStudents = new List<SelectListItem>() { 
                new SelectListItem() { Text = "blah1-1, blah1", Value = "blah1token" },
                new SelectListItem() { Text = "blah2-2, blah2", Value = "blah2token" } };
            Assert.IsNotNull(model.RegisteredStudents);
            Assert.AreEqual(2, model.RegisteredStudents.Count);
            Assert.IsTrue(model.RegisteredStudents.Any(x => x.Text == "blah1-1, blah1"));
            Assert.IsTrue(model.RegisteredStudents.Any(x => x.Text == "blah2-2, blah2"));
        }
    }
}
