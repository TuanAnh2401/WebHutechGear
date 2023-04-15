using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models.EF;
using Web_Hutech_Gear.Models;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        static int tmp;
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            tmp = id;
            var items = db.ProductImages.Where(x => x.ProductId == id).ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            db.ProductImages.Remove(item);
            if (item.IsDefault)
            {
                var findProc = db.Products.Find(tmp);
                findProc.Image = null;
                db.Entry(findProc).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            var items = db.ProductImages.Where(x => x.ProductId == tmp).ToList();
            foreach (var item in items)
            {
                var obj = db.ProductImages.Find(item.Id);
                db.ProductImages.Remove(obj);
            }
            var findProc = db.Products.Find(tmp);
            findProc.Image = null;
            db.Entry(findProc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult IsDefault(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item != null)
            {
                var find = db.ProductImages.Where(x => x.ProductId == tmp).ToList();
                if(!(item.IsDefault))
                {
                    foreach (var items in find)
                    {
                        items.IsDefault = false;
                        db.Entry(items).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                if(item.IsDefault)
                {
                    var findProc = db.Products.Find(tmp);
                    findProc.Image = item.Image;
                    db.Entry(findProc).State = System.Data.Entity.EntityState.Modified;
                }

                item.IsDefault = !item.IsDefault;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsDefault = item.IsDefault });
            }

            return Json(new { success = false });
        }
    }
}