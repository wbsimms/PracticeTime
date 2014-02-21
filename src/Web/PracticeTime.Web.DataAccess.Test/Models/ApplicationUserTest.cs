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
            user.C_AccountTypeId = 4;
            user.C_AccountType = new C_AccountType();
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.C_AccountType);
            Assert.AreEqual(4,user.C_AccountTypeId);
            Assert.IsNotNull(user.StudentToken);
            Assert.IsNotNull(user.FirstName);
            Assert.IsNotNull(user.LastName);
            Assert.IsNotNull(user.EmailAddress);
        }
    }
}
