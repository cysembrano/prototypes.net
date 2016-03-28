using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf.Logging;

namespace TimerConsole
{
    public class TimerService
    {
        private Timer _timer;
        private static readonly LogWriter _log = HostLogger.Get<TimerService>();

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessReports();
        }

        public bool Start()
        {
            _timer = new Timer(10000);
            _timer.Elapsed += _timer_Elapsed;  
            _timer.Enabled = true;
            _timer.Start();
            _log.InfoFormat("Convergys Assist Reporter STARTED at {0}", DateTime.Now.ToLongDateString());
            return true;
        }

        public bool Stop()
        {
            
            _timer.Stop();
            _log.InfoFormat("Convergys Assist Reporter STOPPED at {0}", DateTime.Now.ToLongDateString());
            _timer.Enabled = false;
            _timer.Dispose();
            return true;
        }

        private void ProcessReports()
        {
            _log.InfoFormat("Convergys Assist Reporter RAN at {0}", DateTime.Now.ToLongDateString());

        }
    }


}
