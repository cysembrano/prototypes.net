using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOCforMVC.Web.Models
{
    public class DebugMessageService : IDebugMessageService
    {
        public string Message
        {
            get { return DateTime.Now.ToString(); }
        }
    }
}