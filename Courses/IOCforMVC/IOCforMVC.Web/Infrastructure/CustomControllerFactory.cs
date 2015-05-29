using IOCforMVC.Web.Controllers;
using IOCforMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace IOCforMVC.Web.Infrastructure
{
    public class CustomControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (String.Compare(controllerName.ToLower(), "proteintracker") == 0)
            {
                var repo = new ProteinRepository("abc123");
                var srv = new ProteinTrackingService(repo);
                var ctrl = new ProteinTrackerController(srv);
                return ctrl;
            }

            var defaultFactory = new DefaultControllerFactory();
            return defaultFactory.CreateController(requestContext, controllerName);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

    }
}