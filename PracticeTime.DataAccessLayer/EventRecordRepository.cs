using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Common.IDataAccessLayer;
using PracticeTime.Common.Models;

namespace PracticeTime.DataAccessLayer
{
    public class EventRecordRepository : IEventRecordRepository
    {
        public void SaveEventRecord(EventRecord eventRecord)
        {
            throw new NotImplementedException();
        }

        public List<EventRecord> GetAllEventRecords()
        {
            throw new NotImplementedException();
        }

        public EventRecord GetEventRecord(int id)
        {
            throw new NotImplementedException();
        }
    }
}
