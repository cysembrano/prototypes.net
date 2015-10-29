using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ShowProcessTime
{
    public class Global : System.Web.HttpApplication
    {

        public void Application_BeginRequest(object obj, EventArgs e)
        {
            Context.Items["startTime"] = DateTime.Now;
        }

        public void Application_EndRequest(object obj, EventArgs e)
        {
            DateTime t = (DateTime)Context.Items["startTime"];
            TimeSpan delta = DateTime.Now - t;
            Response.Write(String.Format("<h5>This page took {0} ms to process</h5>", delta.TotalMilliseconds.ToString()));
        }
    }
}
