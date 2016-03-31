using System.Web;
using System.Web.Mvc;
using Convergys.Assist.WebUI.App_Start;

namespace Convergys.Assist.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}