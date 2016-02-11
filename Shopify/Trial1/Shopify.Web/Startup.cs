using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shopify.Web.Startup))]
namespace Shopify.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
