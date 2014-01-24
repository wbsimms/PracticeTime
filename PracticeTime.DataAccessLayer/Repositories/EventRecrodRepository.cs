using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Common.Interfaces;
using PracticeTime.Common.Models;

namespace PracticeTime.DataAccessLayer.Repositories
{
    public class EventRecordRepository : IEventRecordRepository
    {
        public void SaveEventRecord(EventRecord eventRecord)
        {
        }

        public List<EventRecord> GetAllEventRecords()
        {
            return null;
        }

        public EventRecord GetEventRecord(int id)
        {
            return null;
        }
    }
}
