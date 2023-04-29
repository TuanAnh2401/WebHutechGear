using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        public ActionResult Detail(int id)
        {
            var detailProduct = db.Products.SingleOrDefault(n => n.Id == id);

            // Sản phẩm liên quan
            ViewBag.listProduct = db.Products.Where(n => n.ProductCategoryId == detailProduct.ProductCategoryId).ToList().Take(3);

            // Hiển thị danh sách bình luận
            ViewBag.listCommnet = db.Comment.Where(n => n.ProductId == id).ToList();

            return View(detailProduct);
        }

        public ActionResult Comment(int productId, FormCollection f, string strURL)
        {
            int rating = int.Parse(f["rate"].ToString()); ;
            string content = f["content"].ToString();
            var ID = User.Identity.GetUserId();
            Comment cm = new Comment();
            cm.UserId = ID;
            cm.ProductId = productId;
            cm.Content = content;
            cm.Rating = rating;
            cm.CreatedDate = DateTime.Now;
            db.Comment.Add(cm);
            db.SaveChanges();
            return Redirect(strURL);
        }
    }
}