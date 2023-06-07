using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web_Hutech_Gear.Controllers;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException httpException && httpException.GetHttpCode() == 404)
            {
                Response.Clear();
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                RouteData routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "NotFound";
                IController errorController = new ErrorController();
                errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }

        protected void Application_Start()
        {
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer<ApplicationDbContext>(null);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
