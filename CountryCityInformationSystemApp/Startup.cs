using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CountryCityInformationSystemApp.Startup))]
namespace CountryCityInformationSystemApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
