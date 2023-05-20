using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.NewsCategory;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NewsCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.Identity.GetUserName();
                model.Modifiedby = User.Identity.GetUserName();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.NewsCategory.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.NewsCategory.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsCategory model)
        {
            if (ModelState.IsValid)
            {
                model.Modifiedby = User.Identity.GetUserName();
                model.ModifiedDate = DateTime.Now;
                db.NewsCategory.Attach(model);
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
                    var item = db.NewsCategory.Find(id);
                    if (item != null)
                    {
                        var findNews = db.News.Where(p => p.NewsCategoryId == item.Id).ToList();
                        if (findNews != null)
                        {
                            foreach (var news in findNews)
                            {
                                db.News.Remove(news);
                            }
                        }
                        db.NewsCategory.Remove(item);
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
                                var sp = db.NewsCategory.Find(Convert.ToInt32(item));
                                if (sp != null)
                                {
                                    var findNews = db.News.Where(p => p.NewsCategoryId == sp.Id).ToList();
                                    if (findNews != null)
                                    {
                                        foreach (var news in findNews)
                                        {
                                            db.News.Remove(news);
                                        }
                                    }
                                   
                                }
                                db.NewsCategory.Remove(sp);
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