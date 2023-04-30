using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(String currentFilter)
        {
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.Max = db.Products.Max(p=>p.Price);
            ViewBag.ActiveMenu = "Products";
            return View();
        }
        public ActionResult Partial_GetCategory()
        {
            var listCategory = db.ProductCategories.ToList();
            return PartialView("Partial_GetCategory", listCategory);
        }
        public ActionResult Partial_GetSupplier()
        {
            var listSupplier = db.Suppliers.ToList();
            return PartialView("Partial_GetSupplier", listSupplier);
        }
        public ActionResult Partial_Procducts(int? page, string searchString, List<int> categoryIds, List<int> supplierIds, int? min, int? max, string currentFilter, List<int> currentId, List<int> currentSpId, int? currentMin, int? currentMax)
        {
            searchString = searchString ?? currentFilter;
            categoryIds = categoryIds ?? currentId;
            supplierIds = supplierIds ?? currentSpId;
            min = min ?? currentMin;
            max = max ?? currentMax;

            var items = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(p => p.Title.ToLower().Contains(searchString.ToLower()));
            }
            if (categoryIds != null && categoryIds.Count() != 0)
            {
                items = items.Where(p => categoryIds.Contains(p.ProductCategoryId));
            }
            if (supplierIds != null && supplierIds.Count()!= 0)
            {
                items = items.Where(p => supplierIds.Contains(p.SupplierId));
            }
            if (min != null && max != null && (min != 0 || max != 0))
            {
                items = items.Where(p => p.Price >= min && p.Price <= max);
            }

            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var pagedList = items.ToList().ToPagedList(pageIndex, pageSize);

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentId = categoryIds;
            ViewBag.CurrentSpId = supplierIds;
            ViewBag.CurrentMin = min;
            ViewBag.CurrentMax = max;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return PartialView("Partial_Procducts", pagedList);
        }

    }
}