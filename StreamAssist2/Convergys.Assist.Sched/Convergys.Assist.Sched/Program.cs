using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convergys.Assist.Sched
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmAgentSchedule());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message = ((Exception)e.ExceptionObject).Message;
            Logger.Instance.Error(message);

            MessageBox.Show(message, "Unhandled Exception");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.Instance.Error(e.Exception.Message);

            MessageBox.Show(e.Exception.Message, "Unhandled Exception");
        }



        
    }

    
}
