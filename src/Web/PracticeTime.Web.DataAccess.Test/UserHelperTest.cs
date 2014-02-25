using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeTime.Web.DataAccess.Test
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
        public void GetStudentTokenTest()
        {
            UserHelper helper = new UserHelper();
            string token = helper.GetStudentToken("student");
            Assert.IsTrue(!string.IsNullOrEmpty(token));
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
