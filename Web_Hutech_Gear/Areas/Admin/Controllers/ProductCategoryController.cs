using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.ProductCategories.Where(p=>!(p.IsActivate));
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.Identity.GetUserName();
                model.Modifiedby = User.Identity.GetUserName();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.ProductCategories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.Modifiedby = User.Identity.GetUserName();
                model.ModifiedDate = DateTime.Now;
                db.ProductCategories.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.ProductCategories.Find(id);
                    if (item != null)
                    {
                        var find = db.Products.Where(p => p.ProductCategoryId == item.Id).ToList();
                        if (find != null)
                        {
                            foreach (var proc in find)
                            {
                                var obj = db.Products.Find(proc.Id);
                                if (obj != null)
                                {
                                    obj.IsActivate = true;
                                    db.Products.Attach(obj);
                                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                        item.IsActivate = true;
                        db.ProductCategories.Attach(item);
                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        return Json(new { success = true });
                    }
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            using (var transaction = db.Database.BeginTransaction())
            {

                try
                {
                    if (!string.IsNullOrEmpty(ids))
                    {
                        var items = ids.Split(',');
                        if (items != null && items.Any())
                        {
                            foreach (var item in items)
                            {
                                var sp = db.ProductCategories.Find(Convert.ToInt32(item));
                                if (sp != null)
                                {
                                    var find = db.Products.Where(p => p.ProductCategoryId == sp.Id).ToList();
                                    if (find != null)
                                    {
                                        foreach (var proc in find)
                                        {
                                            var obj = db.Products.Find(proc.Id);
                                            if (obj != null)
                                            {
                                                obj.IsActivate = true;
                                                db.Products.Attach(obj);
                                                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                            }
                                        }
                                    }
                                    sp.IsActivate = true;
                                    db.ProductCategories.Attach(sp);
                                    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                            db.SaveChanges();
                            transaction.Commit();
                            return Json(new { success = true });
                        }
                    }

                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return Json(new { success = false });
        }

    }
}