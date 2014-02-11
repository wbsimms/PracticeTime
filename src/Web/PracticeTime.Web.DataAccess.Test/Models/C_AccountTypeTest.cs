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
    public class C_AccountTypeTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            C_AccountType accountType = new C_AccountType(){Active = false};
            Assert.IsNotNull(accountType);
            Assert.IsFalse(accountType.Active);
        }
    }
}
