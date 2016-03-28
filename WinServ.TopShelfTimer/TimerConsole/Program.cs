using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TimerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.UseNLog();
                serviceConfig.Service<TimerService>(serviceInstance =>
                    {
                        serviceInstance.ConstructUsing(() => new TimerService());
                        serviceInstance.WhenStarted(execute => execute.Start());
                        serviceInstance.WhenStopped(execute => execute.Stop());
                    });
                serviceConfig.SetServiceName("ConvergysAssistReportService");
                serviceConfig.SetDisplayName("ConvergysAssist Report Service");
                serviceConfig.SetDescription("Report Service that queries ConvergysAssist DB to report emails to client teams.");
                serviceConfig.StartAutomatically();
            });
        }
    }
}
