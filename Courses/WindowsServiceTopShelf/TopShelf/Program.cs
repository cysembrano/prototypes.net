using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Builders;
using Topshelf.Configurators;
using Topshelf.Constants;
using Topshelf.HostConfigurators;
using Topshelf.Hosts;
using Topshelf.Logging;
using Topshelf.Options;
using Topshelf.Runtime;
using Topshelf.ServiceConfigurators;


namespace TopShelfProject
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.Service<ConverterService>(serviceInstance =>
                {
                    
                    serviceInstance.ConstructUsing(() => new ConverterService());
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());                    

                });

                serviceConfig.SetServiceName("AwesomeFileConverter");
                serviceConfig.SetDisplayName("Awesome File Converter");
                serviceConfig.SetDescription("A Topshelf demo service");
                serviceConfig.StartAutomatically();

            });
        }
    }
}
