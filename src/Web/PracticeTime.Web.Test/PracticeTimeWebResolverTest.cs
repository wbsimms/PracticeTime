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

        [TestCleanup]
        public void Cleanup()
        {
            PracticeTimeWebResolver.Instance.Reset();
        }
    }
}
