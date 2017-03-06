using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace HelloServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HelloService.HelloService)))
            {
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior()
                {
                    HttpGetEnabled = true
                };
                host.Description.Behaviors.Add(serviceBehavior);
                host.AddServiceEndpoint(typeof(HelloService.IHelloServiceChanged), new NetTcpBinding(), "HelloService");
                host.Open();
                Console.WriteLine("Host Started @ " + DateTime.Now.ToString());
                Console.ReadLine();
            }
        }
    }
}
