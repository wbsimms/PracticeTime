using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Test.Models
{
    [TestClass]
    public class ApplicationUserTest
    {

        [TestMethod]
        public void ConstructorTest()
        {
            ApplicationUser user = new ApplicationUser() {StudentToken = "blah",FirstName = "Barrett",LastName = "Simms",EmailAddress = "wbsimms@gmail.com"};
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.StudentToken);
            Assert.IsNotNull(user.FirstName);
            Assert.IsNotNull(user.LastName);
            Assert.IsNotNull(user.EmailAddress);
        }
    }

    [TestClass]
    public class ApplicationRoleTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ApplicationRole role = new ApplicationRole("Admin"){Active = true,Id="blah"};
            Assert.IsNotNull(role.Name);
            Assert.IsNotNull(role.Id);
            Assert.IsTrue(role.Active);
        }
    }
}
