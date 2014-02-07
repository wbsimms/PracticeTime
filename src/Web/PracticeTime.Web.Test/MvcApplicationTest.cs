using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeTime.Web.Test
{
    [TestClass]
    public class MvcApplicationTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            MvcApplication app = new MvcApplication();
            Assert.IsNotNull(app);
        }
    }
}
