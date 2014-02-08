using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeTime.Web.DataAccess.Test
{
    [TestClass]
    public class PracticeTimeDataAccessResolverTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            PracticeTimeDataAccessResolver instance = PracticeTimeDataAccessResolver.Instance;
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void ContainerTest()
        {
            PracticeTimeDataAccessResolver instance = PracticeTimeDataAccessResolver.Instance;
            Assert.IsNotNull(instance.Container);
        }

    }
}
