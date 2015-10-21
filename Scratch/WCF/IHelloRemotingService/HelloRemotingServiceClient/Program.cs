using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HelloRemotingServiceClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);   

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var message = String.Format("Sorry, something went wrong. \r\n {0} \r\n Please contact support.", e.Exception.Message);
            
            //Log here
            Console.Write(e.Exception + " Date Time:" + DateTime.Now);

            //Show here
            MessageBox.Show(message, "Unexpected Error");
        }

        
    }
}
