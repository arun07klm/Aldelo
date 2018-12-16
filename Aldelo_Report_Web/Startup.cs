using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aldelo_Report_Web.Startup))]
namespace Aldelo_Report_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
