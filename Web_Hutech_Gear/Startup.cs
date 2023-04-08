using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Hutech_Gear.Startup))]
namespace Web_Hutech_Gear
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
