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
            string userId = helper.GetUserId("student");
            Assert.IsNotNull(userId);
            Assert.IsTrue(!string.IsNullOrEmpty(userId));
        }

        [TestMethod]
        public void GetRoleFromIdTest()
        {
            Assert.AreEqual(Roles.Student,UserHelper.GetRoleFromId("1"));
            Assert.AreEqual(Roles.Instructor, UserHelper.GetRoleFromId("2"));
            try
            {
                Assert.AreEqual(Roles.Student, UserHelper.GetRoleFromId("99999"));
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Unable to determine role", ex.Message); 
            }

            Assert.AreEqual("Student",Roles.Student.ToString());
        }
    }
}
