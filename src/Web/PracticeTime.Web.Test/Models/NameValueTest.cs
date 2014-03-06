using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class NameValueTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            NameValue nameValue = new NameValue(){Name="foo",Value = "bar"};
            Assert.IsNotNull(nameValue.Name);
            Assert.IsNotNull(nameValue.Value);
        }
    }
}
