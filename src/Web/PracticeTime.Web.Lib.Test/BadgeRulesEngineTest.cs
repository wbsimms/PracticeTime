using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeTime.Web.Lib.Test
{
    [TestClass]
    public class BadgeRulesEngineTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            IBadgeRulesEngine engine = PracticeTimeLibResolver.Instance.Container.Resolve<IBadgeRulesEngine>();
            Assert.IsNotNull(engine);
            Assert.IsTrue(engine is BadgeRulesEngine);
        }
    }
}
