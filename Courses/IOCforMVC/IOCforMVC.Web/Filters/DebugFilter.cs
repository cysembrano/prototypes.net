using IOCforMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IOCforMVC.Web.Filters
{
    public class DebugFilter : ActionFilterAttribute
    {
        private IDebugMessageService _debugMessageService;

        public DebugFilter(IDebugMessageService debugMessageService)
        {
            this._debugMessageService = debugMessageService;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(_debugMessageService.Message);
            
        }
    }
}