using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Sched
{
    public class Logger
    {
        private static Logger instance;
        private readonly string _logFolderPath;
        private readonly string _logFullFileName;

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();

                return instance;
            }
        }
                      
        private Logger()
        {
            _logFolderPath = Settings.Instance.LogFolderPath;
            _logFullFileName = String.Format(@"{0}\{1}.txt", _logFolderPath, DateTime.Now.ToString("yyyyMMdd"));
            EnsureLoggingFolder();
            EnsureLoggingFile();
        }

        public void Reload()
        {
            instance = null;
        }

        private void EnsureLoggingFolder()
        {
            
            if (!Directory.Exists(_logFolderPath))
                Directory.CreateDirectory(_logFolderPath);
        }

        private void EnsureLoggingFile()
        {
            if (!File.Exists(_logFullFileName))
                File.Create(_logFullFileName);
        }

        private string Log(string logType, string message)
        {
            string fullLog = String.Format("{0} - [{1}] - {2}", DateTime.Now.ToLongTimeString(), logType, message);
            using (StreamWriter sw = File.AppendText(_logFullFileName))
            {
                sw.WriteLine(fullLog);
            }
            return fullLog;
        }

        private void Log(string logType, string message, Action<string> updateUI)
        {
            string fullLog = Log(logType, message);
            updateUI(fullLog);
        }


        public void Info(string message, Action<string> updateUI)
        {
            Log("INFO", message, updateUI);
        }

        public void Info(string message)
        {
            Log("INFO", message);
        }

        public void Warn(string message, Action<string> updateUI)
        {
            Log("WARN", message, updateUI);
        }

        public void Warn(string message)
        {
            Log("WARN", message);
        }

        public void Error(string message, Action<string> updateUI)
        {
            Log("ERROR", message, updateUI);
        }

        public void Error(string message)
        {
            Log("ERROR", message);
        }


    }
}
