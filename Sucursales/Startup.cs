using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sucursales.Startup))]
namespace Sucursales
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
