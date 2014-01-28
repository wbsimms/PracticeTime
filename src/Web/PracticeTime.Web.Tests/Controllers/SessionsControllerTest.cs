using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories.Fakes;

namespace PracticeTime.Web.Tests.Controllers
{
    [TestClass]
    public class SessionsControllerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            StubISessionRepository stub = new PracticeTime.Web.DataAccess.Repositories.Fakes.StubISessionRepository();
            stub.GetAllForUserString = i => { return new List<Session>() {new Session()
            {
                SessionId = 1,Time = 20,Title= "blah"
            }, new Session()
            {
                SessionId = 1,Time = 20,Title= "blah2"
            }};
            };

            SessionsController controller = new SessionsController(stub);
            Assert.IsNotNull(controller);
        }
    }
}
