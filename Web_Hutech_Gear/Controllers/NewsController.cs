using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;

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

            var items = db.News.Where(p=>!(p.IsActivate)).AsQueryable();

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
        public ActionResult Partial_Comment(Comment listCmt)
        {
            return PartialView("Partial_Comment", listCmt);
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
            // Hiển thị danh sách bình luận
            ViewBag.listComment = db.Comments.Where(n => n.NewsId == id).ToList();
            // Truyền dữ liệu sang view bằng Viewbag
            ViewBag.idCurrent = idCurrent;
            ViewBag.idMaxTmp = idMaxTmp;
            var aaaa = db.Comments.ToList();
            // Hiển thị danh sách bình luận hiện có
            ViewBag.comments = db.Comments.Where(x => x.NewsId == id).ToList();

            return View(news);
        }

        // Hiển thị sanh sách Comment_News
        public ActionResult Comment(int newsId, string content, string url)
        {
            // Lấy ID của người dùng đăng nhập
            var userId = User.Identity.GetUserId();

            // Tạo đối tượng Comment và lưu vào database
            var comment = new Comment
            {
                UserId = userId,
                NewsId = newsId,

                Content = content,
                CreatedDate = DateTime.Now
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            // Lấy danh sách bình luận của sản phẩm
            var listComments = db.Comments.Where(c => c.NewsId == newsId).ToList();

            ViewBag.ListRepplyComment = db.Replies.ToList().OrderBy(c => c.CreatedDate);

            // Trả về PartialView Partial_Rated với dữ liệu danh sách bình luận
            return PartialView("Partial_Comment", listComments);
        }

    }
}