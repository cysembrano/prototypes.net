using FlowCOM_EXEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Logging;

namespace Web.FlowCOM
{
    internal static class FlowCOMObject
    {
        public static IFloCOM_EXEC FlowCOM_ApplicationInstance { get; private set; }

        public static void Application_Start(object sender, EventArgs args)
        {
            try
            {
                Log4NetManager.Instance.Info(typeof(FlowCOMObject), "FlowCOMObject: Application Started.");
            }
            catch(Exception ex)
            {

            }
        }

    }
}