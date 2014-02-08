using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeTime.Web.Test
{
    [TestClass]
    public class PracticeTimeWebResolverTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            PracticeTimeWebResolver resolver = PracticeTimeWebResolver.Instance;
            Assert.IsNotNull(resolver);
        }

        [TestMethod]
        public void RestTest()
        {
            Assert.IsNotNull(PracticeTimeWebResolver.Instance.Container);
        }

        [TestCleanup]
        public void Cleanup()
        {
            PracticeTimeWebResolver.Instance.Reset();
        }
    }
}