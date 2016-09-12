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

            string ip = "192.168.5.31";
            string startport = "35699";
            
            //Get services in your given address = ASYNC
            MonitorAdmin_AsyncProxy IMonitorServiceAsync = new MonitorAdmin_AsyncProxy("tcp://" + ip + ":" + startport + "/");
            var task = IMonitorServiceAsync.GetServicesAsync();
            Console.Write("Awaiting");
            task.ContinueWith((antecedent) => {
                var @new = antecedent.Result;
                Console.Write("Done"); 
            });


            //Get Services in your given address
            MonitorAdmin_Proxy IMonitorService = new MonitorAdmin_Proxy("tcp://" + ip + ":" + startport + "/");
            var servicesArray = IMonitorService.GetServices(); //Working
            FloBaseTypes.ServiceInfo firstService = servicesArray.ElementAtOrDefault(0);
            if(firstService != null)
            {
                string Id = firstService.ServiceId;
                //var actionss = IMonitorService.GetScheduleActions(Id, aShowDisabled: true); //Not working
                bool result = IMonitorService.StartFlowServer(Id); // Working 
                


                //To Talk to Flow Service
                Admin_Proxy IService = new Admin_Proxy("tcp://" + ip + ":" + firstService.ServicePort + "/");
                var ping = IService.Ping();
                var name = IService.GetComputerName();
                var actions = IService.GetScheduleActions(false);
            }





            

        }
    }
}
