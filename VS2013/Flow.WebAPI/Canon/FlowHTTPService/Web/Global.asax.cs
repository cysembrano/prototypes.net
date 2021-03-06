﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Web.FlowCOM;
using Web.Logging;
namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Log4NetManager.EnsureConfigured();

            var route = new HttpRoute(routeTemplate: "{controller}/{id}", defaults: new HttpRouteValueDictionary { { "id", RouteParameter.Optional }});
            GlobalConfiguration.Configuration.Routes.Add("default", route);

            //GlobalConfiguration.Configuration.Formatters.Insert(0,new TextMediaTypeFormatter);


            FlowAPIObject.Application_Start(sender, e);


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            FlowAPIObject.Application_End(sender, e);
        }
    }
}