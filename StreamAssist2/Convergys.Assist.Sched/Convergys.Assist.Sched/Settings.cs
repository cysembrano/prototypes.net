using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Sched
{
    public class Settings
    {
        public string LogFolderPath { get; set; }
        public string SAConnection { get; set; }
        public string RTAConnection { get; set; }

        private static Settings instance;

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    instance = new Settings();

                return instance;
            }
        }

        private Settings()
        {
            LogFolderPath = Properties.Settings.Default.LogFolderPath;
            SAConnection = Properties.Settings.Default.SAConnection;
            RTAConnection = Properties.Settings.Default.RTAConnection;

        }
        public void Save()
        {
            Properties.Settings.Default.LogFolderPath = LogFolderPath;
            Properties.Settings.Default.SAConnection = SAConnection;
            Properties.Settings.Default.RTAConnection = RTAConnection;
            Properties.Settings.Default.Save();
        }

        
           
    }
}
