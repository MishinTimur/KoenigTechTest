using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KoenigTechTest.Startup))]
namespace KoenigTechTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
