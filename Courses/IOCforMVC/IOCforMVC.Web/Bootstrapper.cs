using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using IOCforMVC.Web.Models;
using System.Web.Configuration;

namespace IOCforMVC.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        //container.RegisterTypes(
        //    AllClasses.FromAssemblies(), 
        //    WithMappings.FromAllInterfacesInSameAssembly, 
        //    WithName.Default);
        
        //Unique instance everytime.
        //container.RegisterType<IProteinRepository, ProteinRepository>(new InjectionConstructor("testDataSource"));

        //Singleton way
        //container.RegisterInstance(typeof(IProteinRepository), new ProteinRepository("test Data source 123"));

        container.RegisterType<IProteinRepository, ProteinRepository>("Standard", new InjectionConstructor("Test"));
        container.RegisterType<IProteinRepository, DebugProteinRepository>("Debug");

        var repositoryType = WebConfigurationManager.AppSettings["RepositoryType"];
        container.RegisterType<IProteinTrackingService, ProteinTrackingService>
            (new InjectionConstructor(container.Resolve<IProteinRepository>(repositoryType)));

        container.RegisterType<IAnalyticsService, AnalyticsService>();
        container.RegisterType<IDebugMessageService, DebugMessageService>();
        container.RegisterType<IProteinTrackingService, ProteinTrackingService>();

    }
  }

    public class DebugProteinRepository : IProteinRepository
    {

        public ProteinData GetData(System.DateTime date)
        {
            return new ProteinData();
        }

        public void SetGoal(System.DateTime date, int val)
        {
            
        }

        public void SetTotal(System.DateTime date, int val)
        {
            
        }
    }
}