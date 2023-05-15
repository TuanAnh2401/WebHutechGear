using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Web.Services.Description;
using Web_Hutech_Gear.Models.Support;

[assembly: OwinStartupAttribute(typeof(Web_Hutech_Gear.Startup))]
namespace Web_Hutech_Gear
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
