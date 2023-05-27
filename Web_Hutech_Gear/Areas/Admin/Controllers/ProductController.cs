using Microsoft.AspNet.Identity;
using OfficeOpenXml;
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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            IEnumerable<Product> items = db.Products.Where(p=>!(p.IsActivate)).OrderByDescending(x => x.Id);
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
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Title");
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                            model.Image = Images[i];
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedBy = User.Identity.GetUserName();
                model.Modifiedby = User.Identity.GetUserName();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Title");
            var item = db.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategories.ToList(), "Id", "Title");
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Title");
            if (ModelState.IsValid)
            {
                model.Modifiedby = User.Identity.GetUserName();
                model.ModifiedDate = DateTime.Now;
                db.Products.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.Products.Find(id);
                    if (item != null)
                    {
                        item.IsActivate = true;
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
                                var obj = db.Products.Find(Convert.ToInt32(item));
                                if (obj != null)
                                {
                                    obj.IsActivate = true;
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
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
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
            var item = db.Products.Find(id);
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
            var item = db.Products.Find(id);
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

                        // Tạo danh sách sản phẩm để lưu dữ liệu từ tệp Excel
                        List<Product> productList = new List<Product>();

                        // Lặp qua các hàng trong worksheet và tạo các đối tượng sản phẩm
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            string title = worksheet.Cells[row, 1].Value.ToString();
                            string description = worksheet.Cells[row, 2].Value.ToString();
                            string detail = worksheet.Cells[row, 3].Value.ToString();
                            string image = worksheet.Cells[row, 4].Value.ToString();
                            decimal originalPrice = Convert.ToDecimal(worksheet.Cells[row, 5].Value.ToString());
                            decimal price = Convert.ToDecimal(worksheet.Cells[row, 6].Value.ToString());
                            decimal? priceSale = null;
                            if (worksheet.Cells[row, 7].Value != null)
                            {
                                priceSale = Convert.ToDecimal(worksheet.Cells[row, 7].Value.ToString());
                            }
                            int quantity = Convert.ToInt32(worksheet.Cells[row, 8].Value.ToString());
                            var productCategory = worksheet.Cells[row, 9].Value.ToString();
                            int productCategoryId = db.ProductCategories.FirstOrDefault(p => p.Title == productCategory).Id;
                            var supplier = worksheet.Cells[row, 10].Value.ToString();
                            int supplierId = db.Suppliers.FirstOrDefault(p => p.Title == supplier).Id;
                            bool isHome = Convert.ToBoolean(worksheet.Cells[row, 11].Value.ToString());
                            bool isSale = Convert.ToBoolean(worksheet.Cells[row, 12].Value.ToString());
                            bool isHot = Convert.ToBoolean(worksheet.Cells[row, 13].Value.ToString());

                            var product = new Product
                            {
                                Title = title,
                                Description = description,
                                Detail = detail,
                                Image = image,
                                OriginalPrice = originalPrice,
                                Price = price,
                                PriceSale = priceSale,
                                Quantity = quantity,
                                ProductCategoryId = productCategoryId,
                                SupplierId = supplierId,
                                IsHome = isHome,
                                IsSale = isSale,
                                IsHot = isHot,
                                CreatedBy = User.Identity.GetUserName(),
                                Modifiedby = User.Identity.GetUserName(),
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            productList.Add(product);
                        }

                        // Lưu danh sách sản phẩm vào cơ sở dữ liệu
                        db.Products.AddRange(productList);
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