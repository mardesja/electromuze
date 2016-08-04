using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoWebApp.Startup))]
namespace DemoWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
