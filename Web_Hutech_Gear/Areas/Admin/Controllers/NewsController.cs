using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        // GET: Admin/News
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            IEnumerable<News> items = db.News.Where(p=> !(p.IsActivate)).OrderByDescending(x => x.Id); 
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            if (!string.IsNullOrEmpty(searchString))
                items = items.Where(p => p.Title.ToLower().Contains(searchString.ToLower())).ToList().ToPagedList(pageIndex, pageSize);
            else
                items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.CurrentFilter = searchString;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            ViewBag.NewsCategory = new SelectList(db.NewsCategory.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            ViewBag.NewsCategory = new SelectList(db.NewsCategory.ToList(), "Id", "Title");
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.Identity.GetUserName();
                model.Modifiedby = User.Identity.GetUserName();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.NewsCategory = new SelectList(db.NewsCategory.ToList(), "Id", "Title");
            var item = db.News.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                model.Modifiedby = User.Identity.GetUserName();
                model.ModifiedDate = DateTime.Now;
                db.News.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActivate = true;
                db.News.Attach(item);
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
                        var obj = db.News.Find(Convert.ToInt32(item));
                        obj.IsActivate = true;
                        db.News.Attach(obj);
                        db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsHot(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsHot = !item.IsHot;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHot = item.IsHot });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    // Mở tệp Excel sử dụng EPPlus
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.First(); // Lấy trang tính đầu tiên

                        // Tạo danh sách News để lưu dữ liệu từ tệp Excel
                        List<News> newsList = new List<News>();

                        // Lặp qua các hàng trong worksheet và tạo các đối tượng News
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            string title = worksheet.Cells[row, 1].Value.ToString();
                            string description = worksheet.Cells[row, 2].Value.ToString();
                            string detail = worksheet.Cells[row, 3].Value.ToString();
                            string image = worksheet.Cells[row, 4].Value.ToString();
                            var titleCategory = worksheet.Cells[row, 5].Value.ToString();
                            int newsCategoryId = db.NewsCategory.FirstOrDefault(p => p.Title == titleCategory).Id;
                            bool isHome = Convert.ToBoolean(worksheet.Cells[row, 6].Value.ToString());
                            bool isSale = Convert.ToBoolean(worksheet.Cells[row, 7].Value.ToString());
                            bool isHot = Convert.ToBoolean(worksheet.Cells[row, 8].Value.ToString());

                            var news = new News
                            {
                                Title = title,
                                Description = description,
                                Detail = detail,
                                Image = image,
                                NewsCategoryId = newsCategoryId,
                                IsHome = isHome,
                                IsSale = isSale,
                                IsHot = isHot,
                                CreatedBy = User.Identity.GetUserName(),
                                Modifiedby = User.Identity.GetUserName(),
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            newsList.Add(news);
                        }

                        // Lưu danh sách News vào cơ sở dữ liệu
                        db.News.AddRange(newsList);
                        db.SaveChanges();
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}