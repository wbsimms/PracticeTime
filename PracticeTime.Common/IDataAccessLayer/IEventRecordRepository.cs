using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Common.Models;

namespace PracticeTime.Common.IDataAccessLayer
{
    public interface IEventRecordRepository
    {
        void SaveEventRecord(EventRecord eventRecord);
        List<EventRecord> GetAllEventRecords();
        EventRecord GetEventRecord(int id);

        EventRecord GetOneEventRecord { get;}

    }
}
