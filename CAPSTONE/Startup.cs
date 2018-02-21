using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CAPSTONE.Startup))]
namespace CAPSTONE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
