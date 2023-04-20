using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatusController : Controller
    {
        // GET: Admin/Status
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.Status;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Status model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.Identity.GetUserName();
                model.Modifiedby = User.Identity.GetUserName();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Status.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.Status.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Status model)
        {
            if (ModelState.IsValid)
            {
                model.Modifiedby = User.Identity.GetUserName();
                model.ModifiedDate = DateTime.Now;
                db.Status.Attach(model);
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
                    var item = db.Status.Find(id);
                    if (item != null)
                    {
                        var find = db.Products.Where(p => p.StatusId == item.Id).ToList();
                        if (find != null)
                        {
                            foreach (var proc in find)
                            {
                                var obj = db.Products.Find(proc.Id);
                                if (obj != null)
                                {
                                    var checkImg = obj.ProductImage.Where(x => x.ProductId == obj.Id).ToList();
                                    if (checkImg != null)
                                    {
                                        foreach (var img in checkImg)
                                        {
                                            db.ProductImages.Remove(img);
                                        }
                                    }
                                    db.Products.Remove(obj);

                                }
                            }
                        }
                        db.Status.Remove(item);
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
                                var sp = db.Status.Find(Convert.ToInt32(item));
                                if (sp != null)
                                {
                                    var find = db.Products.Where(p => p.StatusId == sp.Id).ToList();
                                    if (find != null)
                                    {
                                        foreach (var proc in find)
                                        {
                                            var obj = db.Products.Find(proc.Id);
                                            if (obj != null)
                                            {
                                                var checkImg = obj.ProductImage.Where(x => x.ProductId == obj.Id).ToList();
                                                if (checkImg != null)
                                                {
                                                    foreach (var img in checkImg)
                                                    {
                                                        db.ProductImages.Remove(img);
                                                    }
                                                }
                                                db.Products.Remove(obj);
                                            }
                                        }
                                    }
                                    db.Status.Remove(sp);
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