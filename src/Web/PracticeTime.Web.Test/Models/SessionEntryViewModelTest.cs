using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;
using PracticeTime.Web.Models;

namespace PracticeTime.Web.Test.Models
{
    [TestClass]
    public class SessionEntryViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            SessionEntryViewModel model = new SessionEntryViewModel() {StateMessage = "sdfdf",TimeZoneOffset = 300,Time = 30,Title = "sdf",SessionDate = DateTime.UtcNow.ToShortDateString(),SessionId = 1,SessionTitles = new List<string>(),BadgeAward = new BadgeAward(),SelectedInstrumentId = 1,UserId = "sdfsdf"};
            Assert.IsNotNull(model.StateMessage);
            Assert.IsNotNull(model.Time);
            Assert.IsNotNull(model.TimeZoneOffset);
            Assert.IsNotNull(model.Title);
            Assert.IsNotNull(model.SessionDate);
            Assert.IsNotNull(model.BadgeAwards);
            Assert.IsNotNull(model.Instruments);
            Assert.IsNotNull(model.SessionId);
            Assert.IsNotNull(model.SessionTitles);
            Assert.IsNotNull(model.UserId);
            Assert.IsNotNull(model.SelectedInstrumentId);
            Assert.IsNotNull(model.BadgeAward);
        }
    }
}
