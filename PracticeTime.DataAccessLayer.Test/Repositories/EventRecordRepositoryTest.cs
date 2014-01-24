using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PracticeTime.DataAccessLayer.Repositories;

namespace PracticeTime.DataAccessLayer.Test.Repositories
{
    [TestClass]
    public class EventRecordRepositoryTest
    {
        [TestMethod]
        public void GetAllEventRecordsTest()
        {
            Assert.IsNotNull(new EventRecordRepository().GetAllEventRecords());
        }
    }
}