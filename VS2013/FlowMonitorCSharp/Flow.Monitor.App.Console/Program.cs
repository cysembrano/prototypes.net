using FlowMonitor;
using FlowService;
using RemObjects.SDK.ZeroConf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Monitor.App
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Get services in your given address = ASYNC
            MonitorAdmin_AsyncProxy IMonitorServiceAsync = new MonitorAdmin_AsyncProxy("tcp://localhost:35699/");
            var task = IMonitorServiceAsync.GetServicesAsync();
            Console.Write("Awaiting");
            task.ContinueWith((antecedent) => {
                var @new = antecedent.Result;
                Console.Write("Done"); 
            });


            //Get Services in your given address
            MonitorAdmin_Proxy IMonitorService = new MonitorAdmin_Proxy("tcp://localhost:35699/");
            var servicesArray = IMonitorService.GetServices(); //Working
            FloBaseTypes.ServiceInfo firstService = servicesArray.ElementAtOrDefault(1);
            if(firstService != null)
            {
                string Id = firstService.ServiceId;
                var actionss = IMonitorService.GetScheduleActions(Id, aShowDisabled: true); //Not working
                bool result = IMonitorService.StartFlowServer(Id); // Working
                
            }

            //To Talk to Flow Service
            Admin_Proxy IService = new Admin_Proxy("tcp://localhost:3569/");
            var ping = IService.Ping();
            var name = IService.GetComputerName();
            var actions = IService.GetScheduleActions(false);


            

        }
    }
}
