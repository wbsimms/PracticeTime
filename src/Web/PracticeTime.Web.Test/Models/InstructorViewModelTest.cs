using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class InstructorViewModelTest
    {

        [TestMethod]
        public void ConstructorTest()
        {
            InstructorViewModel model = new InstructorViewModel();
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void StudentsListItemsTest()
        {
            InstructorViewModel model = new InstructorViewModel();
            model.Students = new List<ApplicationUser>()
            {
                new ApplicationUser(){UserName = "blah",Id ="blahId"},
                new ApplicationUser(){UserName = "blah1",Id="blah2Id"}
            };
            Assert.IsNotNull(model.StudentsListItems);
            Assert.AreEqual(2,model.StudentsListItems.Count);
        }

    }
}
