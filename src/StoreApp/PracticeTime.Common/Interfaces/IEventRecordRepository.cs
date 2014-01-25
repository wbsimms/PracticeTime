using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Common.Models;

namespace PracticeTime.Common.Interfaces
{
    public interface IEventRecordRepository
    {
        void SaveEventRecord(EventRecord eventRecord);
        List<EventRecord> GetAllEventRecords();
        EventRecord GetEventRecord(int id);
    }
}
