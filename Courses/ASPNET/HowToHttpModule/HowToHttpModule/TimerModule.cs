using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowToHttpModule
{
    public class TimerModule : IHttpModule
    {
        public void Dispose()
        {
        }

        /// <summary>
        /// Rename the stubbed out parameter from context to application
        /// </summary>
        public void Init(HttpApplication application)
        {
            application.BeginRequest += new EventHandler(application_BeginRequest);
            application.EndRequest += new EventHandler(application_EndRequest);
        }

        void application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            app.Context.Items["timerModuleStartTime"] = DateTime.Now;
        } 
        void application_EndRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            DateTime start = (DateTime)app.Context.Items["timerModuleStartTime"];
            TimeSpan delta = DateTime.Now - start;
            app.Response.Write(String.Format("<h5>This request took {0} ms to process</h5>", delta.TotalMilliseconds.ToString()));
        }

    }
}