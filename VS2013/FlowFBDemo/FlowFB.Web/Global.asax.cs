using FlowFB.Logging;
using FlowFB.Web.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FlowFB.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log4NetManager.EnsureConfigured();
            LogStart();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            SetProjectDataCache();
        }

        private void LogStart()
        {            
            string StartInfoFormat = "Flow Accounting v {0} has started.";
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Log4NetManager.Instance.Info(this.GetType(), String.Format(StartInfoFormat, version));
        }

        private void SetProjectDataCache()
        {
            ProjectCache.ProjectData = new Dictionary<string, int>();
            ProjectCache.ProjectData.Add("APIInvoice", int.Parse(ConfigurationManager.AppSettings["ProjectID.APInvoice"]));
            ProjectCache.ProjectData.Add("CostCenter", int.Parse(ConfigurationManager.AppSettings["ProjectID.CostCenter"]));
            ProjectCache.ProjectData.Add("GLCodes", int.Parse(ConfigurationManager.AppSettings["ProjectID.GLCodes"]));
            ProjectCache.ProjectData.Add("TaxCodes", int.Parse(ConfigurationManager.AppSettings["ProjectID.TaxCodes"]));

        }
    }
}