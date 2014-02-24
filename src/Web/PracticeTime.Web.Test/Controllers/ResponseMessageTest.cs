using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Controllers;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class ResponseMessageTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ResponseMessage message = new ResponseMessage();
            Assert.IsNotNull(message);
        }

        [TestMethod]
        public void HasErrorsTest()
        {
            ResponseMessage message = new ResponseMessage();
            Assert.IsFalse(message.HasErrors);
            
            message.Errors = new List<string>();
            Assert.IsFalse(message.HasErrors);

            message.Errors.Add("blah");
            Assert.IsTrue(message.HasErrors);
            Assert.AreEqual("blah",message.Errors[0]);
        }

        [TestMethod]
        public void HasMessageTest()
        {
            ResponseMessage message = new ResponseMessage();
            Assert.IsFalse(message.HasMessage);

            message.Message = "";
            Assert.IsFalse(message.HasMessage);

            message.Message = "blah";
            Assert.IsTrue(message.HasMessage);
            Assert.AreEqual("blah", message.Message);
        }

    }
}
