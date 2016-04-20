using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaprisaProject.Startup))]
namespace CaprisaProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
