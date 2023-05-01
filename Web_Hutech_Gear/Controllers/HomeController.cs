using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;
namespace Web_Hutech_Gear.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.ActiveMenu = "Contact";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedBy = model.Name;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Contacts.Add(model);
                db.SaveChanges();
                return View();
            }
            return View(model);
        }
    }
}