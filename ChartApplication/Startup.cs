using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChartApplication.Startup))]
namespace ChartApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
