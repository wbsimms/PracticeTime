﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Controllers;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.DataAccess.Repositories.Fakes;
using PracticeTime.Web.Lib;
using PracticeTime.Web.Lib.Fakes;

namespace PracticeTime.Web.Test.Controllers
{
    [TestClass]
    public class SessionsControllerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            StubIInstrumentRepository stubIInstrumentRepository = new StubIInstrumentRepository();
            StubISessionRepository stub = new PracticeTime.Web.DataAccess.Repositories.Fakes.StubISessionRepository();
            StubIBadgeRulesEngine stubIBadgeRulesEngine = new StubIBadgeRulesEngine(); 
            stub.GetAllForUserString = i =>
            {
                return new List<Session>() {new Session()
            {
                SessionId = 1,Time = 20,Title= "blah"
            }, new Session()
            {
                SessionId = 1,Time = 20,Title= "blah2"
            }};
            };

            SessionsController controller = new SessionsController(stub,stubIBadgeRulesEngine,stubIInstrumentRepository);
            Assert.IsNotNull(controller);
        }
    }
}