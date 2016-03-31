using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convergys.Assist.Test
{
    [TestClass]
    public class LoggingTest
    {
        public LoggingTest()
        {
        }


        [TestMethod]
        public void LoggingSettings_IsTrue()
        {
            var logsettings = ConfigurationManager.AppSettings[typeof(Log4NetManager).FullName + ".LoggingEnabled"];
            bool result = false;
            Boolean.TryParse(logsettings, out result);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestLogInfo()
        {
            Log4NetManager.Instance.Info(this.GetType(), "Test Log Info");
            var exist = File.Exists(string.Format("{0}\\logs\\{1}.log",Environment.CurrentDirectory,DateTime.Now.ToString("yyyyMMdd")));
            Assert.AreEqual(exist, true);
        }

        [TestMethod]
        public void TestLogError()
        {
            Log4NetManager.Instance.Error(this.GetType(), "Test Log Error");
            var exist = File.Exists(string.Format("{0}\\logs\\{1}.log", Environment.CurrentDirectory, DateTime.Now.ToString("yyyyMMdd")));
            Assert.AreEqual(exist, true);
        }


    }
}
