using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    public class RestoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Restore
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_Adv()
        {
            var items = db.Advs.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult adv_delete(int id)
        {
            var item = db.Advs.Find(id);
            if (item != null)
            {
                db.Advs.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult adv_deleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Advs.Find(Convert.ToInt32(item));
                        db.Advs.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult adv_restore(int id)
        {
            var item = db.Advs.Find(id);
            if (item != null)
            {
                item.IsActivate = false;
                db.Advs.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult adv_restoreAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Advs.Find(Convert.ToInt32(item));
                        obj.IsActivate = false;
                        db.Advs.Attach(obj);
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Partial_Contact()
        {
            var items = db.Contacts.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult contact_delete(int id)
        {
            var item = db.Contacts.Find(id);
            if (item != null)
            {
                db.Contacts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult contact_deleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Contacts.Find(Convert.ToInt32(item));
                        db.Contacts.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult contact_restore(int id)
        {
            var item = db.Contacts.Find(id);
            if (item != null)
            {
                item.IsActivate = false;
                db.Contacts.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult contact_restoreAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Contacts.Find(Convert.ToInt32(item));
                        obj.IsActivate = false;
                        db.Contacts.Attach(obj);
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Partial_News()
        {
            var items = db.News.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult news_delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult news_deleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.News.Find(Convert.ToInt32(item));
                        db.News.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult news_restore(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActivate = false;
                db.News.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult news_restoreAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.News.Find(Convert.ToInt32(item));
                        obj.IsActivate = false;
                        db.News.Attach(obj);
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Partial_NewsCategory()
        {
            var items = db.NewsCategory.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult newsCategory_delete(int id)
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
        public ActionResult newsCategory_deleteAll(string ids)
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
        [HttpPost]
        public ActionResult newsCategory_restore(int id)
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
                                news.IsActivate = false;
                                db.News.Attach(news);
                                db.Entry(news).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                        item.IsActivate = false;
                        db.NewsCategory.Attach(item);
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
        public ActionResult newsCategory_restoreAll(string ids)
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
                                            news.IsActivate = false;
                                            db.News.Attach(news);
                                            db.Entry(news).State = System.Data.Entity.EntityState.Modified;
                                        }
                                    }

                                }
                                sp.IsActivate = false;
                                db.NewsCategory.Attach(sp);
                                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult Partial_Product()
        {
            var items = db.Products.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult product_delete(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.Products.Find(id);
                    if (item != null)
                    {
                        var checkImg = item.ProductImage.Where(x => x.ProductId == item.Id).ToList();
                        if (checkImg != null)
                        {
                            foreach (var img in checkImg)
                            {
                                db.ProductImages.Remove(img);
                            }
                        }
                        db.Products.Remove(item);
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
        public ActionResult product_deleteAll(string ids)
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
                                var obj = db.Products.Find(Convert.ToInt32(item));
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
                                }
                                db.Products.Remove(obj);
                            }
                        }
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
        public ActionResult product_restore(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.Products.Find(id);
                    if (item != null)
                    {
                        item.IsActivate = false;
                        db.Products.Attach(item);
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
        public ActionResult product_restoreAll(string ids)
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
                                var obj = db.Products.Find(Convert.ToInt32(item));
                                if (obj != null)
                                {
                                    obj.IsActivate = false;
                                    db.Products.Attach(obj);
                                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
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
        public ActionResult Partial_ProductCategory()
        {
            var items = db.ProductCategories.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult productCategory_delete(int id)
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
                        db.ProductCategories.Remove(item);
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
        public ActionResult productCategory_deleteAll(string ids)
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
                                    db.ProductCategories.Remove(sp);
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
        [HttpPost]
        public ActionResult productCategory_restore(int id)
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
                                    obj.IsActivate = false;
                                    db.Products.Attach(obj);
                                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                        item.IsActivate = false;
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
        public ActionResult productCategory_restoreAll(string ids)
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
                                                obj.IsActivate = false;
                                                db.Products.Attach(obj);
                                                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                            }
                                        }
                                    }
                                    sp.IsActivate = false;
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
        public ActionResult Partial_Supplier()
        {
            var items = db.Suppliers.Where(p => p.IsActivate);
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult supplier_delete(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.Suppliers.Find(id);
                    if (item != null)
                    {
                        var find = db.Products.Where(p => p.SupplierId == item.Id).ToList();
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
                        db.Suppliers.Remove(item);
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
        public ActionResult supplier_deleteAll(string ids)
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
                                var sp = db.Suppliers.Find(Convert.ToInt32(item));
                                if (sp != null)
                                {
                                    var find = db.Products.Where(p => p.SupplierId == sp.Id).ToList();
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
                                    db.Suppliers.Remove(sp);
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
        [HttpPost]
        public ActionResult supplier_restore(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.Suppliers.Find(id);
                    if (item != null)
                    {
                        var find = db.Products.Where(p => p.SupplierId == item.Id).ToList();
                        if (find != null)
                        {
                            foreach (var proc in find)
                            {
                                var obj = db.Products.Find(proc.Id);
                                if (obj != null)
                                {
                                    obj.IsActivate = false;
                                    db.Products.Attach(obj);
                                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                                }
                            }
                        }
                        item.IsActivate = false;
                        db.Suppliers.Attach(item);
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
        public ActionResult supplier_restoreAll(string ids)
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
                                var sp = db.Suppliers.Find(Convert.ToInt32(item));
                                if (sp != null)
                                {
                                    var find = db.Products.Where(p => p.SupplierId == sp.Id).ToList();
                                    if (find != null)
                                    {
                                        foreach (var proc in find)
                                        {
                                            var obj = db.Products.Find(proc.Id);
                                            if (obj != null)
                                            {
                                                obj.IsActivate = false;
                                                db.Products.Attach(obj);
                                                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                                            }
                                        }
                                    }
                                    sp.IsActivate = false;
                                    db.Suppliers.Attach(sp);
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