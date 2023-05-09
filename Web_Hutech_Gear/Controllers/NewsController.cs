using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;

namespace Web_Hutech_Gear.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index(String currentFilter)
        {
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.ActiveMenu = "News";
            return View();
        }
        public ActionResult Partial_News(int? page, string searchString, string currentFilter)
        {
            searchString = searchString ?? currentFilter;

            var items = db.News.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(p => p.Title.ToLower().Contains(searchString.ToLower()));
            }

            var pageSize = 3;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var pagedList = items.ToList().ToPagedList(pageIndex, pageSize);

            ViewBag.CurrentFilter = searchString;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return PartialView("Partial_News", pagedList);
        }

        // Chi tiết tin tức
        public ActionResult DetailNew(int id)
        {
            // Tìm kiếm bài viết
            var news = db.News.SingleOrDefault(x => x.Id == id);

            //Lấy id hiện tại
            int idCurrent = news.Id;

            //id nhỏ nhất là 1 max là tmp
            int idMaxTmp = db.News.Count();

            // Truyền dữ liệu sang view bằng Viewbag
            ViewBag.idCurrent = idCurrent;
            ViewBag.idMaxTmp = idMaxTmp;

            return View(news);
        }
    }
}