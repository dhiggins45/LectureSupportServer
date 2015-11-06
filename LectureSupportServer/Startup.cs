using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LectureSupportServer.Startup))]
namespace LectureSupportServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
