using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Lib.Test
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Logger logger = Logger.Instance;
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void LogWriterTest()
        {
            Logger logger = Logger.Instance;
            try
            {
                logger.Writer.Write("Testing", LogCategories.General.ToString(),
                    5, 1, TraceEventType.Information,"Some Title");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
