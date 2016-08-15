using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HelloWebAPIDemo
{
    public class SecondController : ApiController
    {
        public string Get()
        {
            return "Second Hello from API at " + DateTime.Now.ToString();
        }
    }
}