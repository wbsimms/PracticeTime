using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess;
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
            Assert.AreEqual(PracticeTimeRoles.Student,UserHelper.GetRoleFromId("Student"));
            Assert.AreEqual(PracticeTimeRoles.Instructor, UserHelper.GetRoleFromId("Instructor"));
            try
            {
                Assert.AreEqual(PracticeTimeRoles.Student, UserHelper.GetRoleFromId("99999"));
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Unable to determine role", ex.Message); 
            }

            Assert.AreEqual("Student",PracticeTimeRoles.Student.ToString());
        }

        [TestMethod]
        public void RandomStringTest()
        {
            string randomString = UserHelper.RandomString(10);
            string randomString1 = UserHelper.RandomString(10);
            Assert.IsTrue(randomString.Length == 10);
            Assert.IsTrue(randomString1.Length == 10);
            Assert.AreNotEqual(randomString,randomString1);
        }
    }
}
