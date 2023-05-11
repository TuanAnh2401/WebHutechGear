using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;

namespace Web_Hutech_Gear.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(String currentFilter)
        {
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.ActiveMenu = "Posts";
            return View();
        }
        public ActionResult Partial_Posts(int? page)
        {
            var items = db.Posts.AsQueryable();

            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var pagedList = items.ToList().ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return PartialView("Partial_Posts", pagedList);
        }

        // Chi tiết bài viết
        public ActionResult DetailBlog(int id)
        {
            // Tìm kiếm bài viết
            var post = db.Posts.SingleOrDefault(x => x.Id == id);

            //Lấy id hiện tại
            int idCurrent = post.Id;

            //id nhỏ nhất là 1 max là tmp
            int idMaxTmp = db.Posts.Count();

            // Truyền dữ liệu sang view bằng Viewbag
            ViewBag.idCurrent = idCurrent;
            ViewBag.idMaxTmp = idMaxTmp;

            return View(post);
        }
    }
}