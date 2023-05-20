using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
        public ActionResult Partial_Products_Home()
        {
            var listProducts = db.Products.Where(p=>p.IsHome).ToList();
            return PartialView("Partial_Products_Home", listProducts);
        }
        public ActionResult Partial_Products_News()
        {
            var listNews = db.News.Where(p => p.IsHome).ToList();
            return PartialView("Partial_Products_News", listNews);
        }
        public ActionResult Sale()
        {
            ViewBag.ActiveMenu = "Sale";

            var items = db.Products.Where(p=>p.IsSale).ToList();

            return View(items);
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
        [HttpPost]
        public ActionResult ContactByEmail(string email)
        {
            try
            {
                // Tạo một đối tượng Contact mới
                var contact = new Contact
                {
                    Email = email,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                // Lưu đối tượng Contact vào cơ sở dữ liệu
                db.Contacts.Add(contact);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }
    }
}