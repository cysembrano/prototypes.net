using IOCforMVC.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace IOCforMVC.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(DependencyResolver.Current.GetService<DebugFilter>());
        }
    }
}