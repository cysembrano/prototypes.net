using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdvancedTopics.Web.Startup))]
namespace AdvancedTopics.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
