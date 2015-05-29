using IOCforMVC.Web.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOCforMVC.Web.Pages
{
    public class ProteinTrackerBasePage : WebViewPage
    {
        [Dependency]
        public IAnalyticsService AnalyticsService { get; set; }

        public override void Execute()
        {
            
        }
    }

}