using System;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Common.Models;
using PracticeTime.DataAccessLayer.Fakes;

namespace PracticeTime.DataAccessLayer.Test
{
    [TestClass]
    public class EventRecordRepositoryTest
    {
        [TestMethod]
        public void GetAllEventRecordsTest()
        {
            using (ShimsContext.Create())
            {
                ShimEventRecordRepository.AllInstances.GetAllEventRecords = blah =>
                {
                    return new List<EventRecord>() {new EventRecord() {EventName = "blah1", Id = 1, Time = 300}};
                };

                Assert.IsNotNull(new EventRecordRepository().GetAllEventRecords());
            }
        }
    }
}
