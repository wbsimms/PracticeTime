using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Database;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Logging.Lib
{

    public enum LogCategories
    {
        General
    }

    public class Logger
    {
        private static Logger instance = new Logger();
        private LogWriter writer;

        public static Logger Instance
        {
            get { return instance; }
        }

        private Logger()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; 
            TextFormatter formatter = new TextFormatter();
            var databaseTraceListener =
              new FormattedDatabaseTraceListener(new SqlDatabase(connectionString)
              , "WriteLog", "AddCategory",formatter);

            var config = new LoggingConfiguration();
            config.AddLogSource(LogCategories.General.ToString(), SourceLevels.All, true).AddTraceListener(databaseTraceListener);
            writer = new LogWriter(config);
           writer.Write("Starting...",LogCategories.General.ToString(),9,0,TraceEventType.Verbose,"Log Starting");
        }

        public LogWriter Writer
        {
            get { return this.writer; }
        }
    }
}
