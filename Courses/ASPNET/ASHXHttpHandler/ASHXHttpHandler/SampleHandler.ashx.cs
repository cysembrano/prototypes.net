using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASHXHttpHandler
{
    /// <summary>
    /// Summary description for SampleHandler
    /// </summary>
    public class SampleHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}