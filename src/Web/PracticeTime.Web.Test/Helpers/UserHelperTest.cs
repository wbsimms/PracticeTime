using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Helpers;

namespace PracticeTime.Web.Test.Helpers
{
    [TestClass]
    public class UserHelperTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            UserHelper helper = new UserHelper();
            Assert.IsNotNull(helper);
        }

        [TestMethod]
        public void GetUserIdTest()
        {
            UserHelper helper = new UserHelper();
            string userId = helper.GetUserId("wbsimms");
            Assert.IsNotNull(userId);
            Assert.IsTrue(!string.IsNullOrEmpty(userId));

        }
    }
}
