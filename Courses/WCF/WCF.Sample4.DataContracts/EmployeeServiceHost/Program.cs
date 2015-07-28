using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace EmployeeServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(EmployeeService.EmployeeService)))
            {
                host.Open();
                Console.WriteLine("Host started @ " + DateTime.Now);
                Console.ReadLine();
            }
        }
    }
}
