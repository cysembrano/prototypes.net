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
                serviceConfig.UseNLog();
                serviceConfig.Service<ConverterService>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new ConverterService());
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());
                    //Pause and Continue Button wireup
                    serviceInstance.WhenPaused(execute => execute.Pause());
                    serviceInstance.WhenContinued(execute => execute.Continue());

                    serviceInstance.WhenCustomCommandReceived(
                        (execute, hostControl, commandNumber) => execute.CustomCommand(commandNumber)
                        );
                  
                });
                //See details on Recovery Tab.
                //First, Second and Subsequent Failure are specified here.
                serviceConfig.EnableServiceRecovery(recoveryOption => {
                    recoveryOption.RestartService(1);
                    recoveryOption.RestartComputer(60, "Awesome File Converter Demo");
                    recoveryOption.RunProgram(5, @"c:\someprogram.exe");
                });

                //Pause and Continue Button enabling
                serviceConfig.EnablePauseAndContinue();

                serviceConfig.SetServiceName("AwesomeFileConverter");
                serviceConfig.SetDisplayName("Awesome File Converter");
                serviceConfig.SetDescription("A Topshelf demo service");
                serviceConfig.StartAutomatically();

            });
        }
    }
}
