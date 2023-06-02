using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Contact
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            IEnumerable<Contact> items = db.Contacts.Where(p=>!(p.IsActivate));
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            if (!string.IsNullOrEmpty(searchString))
                items = items.Where(p => p.Name.ToLower().Contains(searchString.ToLower()) ||
                                         String.Compare(p.PhoneNumber, searchString, true) == 0 ||
                                         String.Compare(p.Email, searchString, true) == 0).ToList().ToPagedList(pageIndex, pageSize);
            else
                items = items.ToList().ToPagedList(pageIndex, pageSize);
            ViewBag.CurrentFilter = searchString;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Detail(int id)
        {
            var detail = db.Contacts.SingleOrDefault(p => p.Id == id);
            detail.IsRead = true;
            detail.Modifiedby = User.Identity.GetUserName();
            detail.ModifiedDate = DateTime.Now;
            db.Contacts.Attach(detail);
            db.Entry(detail).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(detail);
        }
        public ActionResult Partial_Contact ()
        {
            ViewBag.CountContact = db.Contacts.Where(p => p.IsRead == false).Count();
            return PartialView();
        }
        [HttpPost]
        public ActionResult IsRead(int id)
        {
            var item = db.Contacts.Find(id);
            if (item != null)
            {
                item.IsRead = !item.IsRead;
                item.Modifiedby = User.Identity.GetUserName();
                item.ModifiedDate = DateTime.Now;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsRead = item.IsRead });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Contacts.Find(id);
            if (item != null)
            {
                item.IsActivate = true;
                db.Contacts.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Contacts.Find(Convert.ToInt32(item));
                        obj.IsActivate = true;
                        db.Contacts.Attach(obj);
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}