using System.Web.Mvc;

namespace Web_Hutech_Gear.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("Error");
        }
    }


}