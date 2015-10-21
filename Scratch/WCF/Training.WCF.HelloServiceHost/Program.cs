using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Training.WCF.HelloServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(HelloService.HelloService)))
            {
                host.Open();
                Console.WriteLine(String.Format("Host started @ {0}", DateTime.Now));
                Console.ReadLine();
            }
        }
    }
}
