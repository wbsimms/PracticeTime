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
            ApplicationUser user = new ApplicationUser();
            user.C_AccountTypeId = 4;
            user.C_AccountType = new C_AccountType();
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.C_AccountType);
            Assert.AreEqual(4,user.C_AccountTypeId);
        }
    }
}
