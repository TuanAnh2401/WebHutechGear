using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
namespace Web_Hutech_Gear
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Web_Hutech_Gear.Controllers" }
            );
        }
    }
}
