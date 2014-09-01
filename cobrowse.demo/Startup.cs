using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cobrowse.demo.Startup))]
namespace cobrowse.demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
