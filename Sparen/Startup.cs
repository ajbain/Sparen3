using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sparen.Startup))]
namespace Sparen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
