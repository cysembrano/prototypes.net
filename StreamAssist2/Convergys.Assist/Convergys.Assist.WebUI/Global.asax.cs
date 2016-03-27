using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Convergys.Assist.Logging;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.WebUI.Controllers;
using Convergys.Assist.WebUI.Infrastructure;

namespace Convergys.Assist.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Log4NetManager.Instance.Info(this.GetType(), "Application_Start(): Starting Convergys Assist Web UI.");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(EmployeeView), new UserDataModelBinder<EmployeeView>());

        }
    }
}