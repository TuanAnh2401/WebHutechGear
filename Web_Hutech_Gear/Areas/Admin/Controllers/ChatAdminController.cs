using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models.EF;
using Web_Hutech_Gear.Models.Support;
using Web_Hutech_Gear.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using System.Data.Entity;
using System.Collections.Concurrent;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ChatAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static ConcurrentDictionary<string, string> ChatLog = new ConcurrentDictionary<string, string>();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var messages = db.Messages
                .Where(m => m.ReceiverId == userId && m.SenderId != userId)
                .Include(m => m.Sender)
                .GroupBy(m => m.Sender)
                .Select(g => new UserViewModel
                {
                    Id = g.Key.Id,
                    FullName = g.Key.FullName,
                    LastMessage = g.OrderByDescending(m => m.Timestamp).FirstOrDefault().Message
                })
                .ToList();

            return View(messages);
        }

        public ActionResult Chat(string receiverId)
        {
            // Lấy thông tin user hiện tại
            var userId = User.Identity.GetUserId();
            ViewBag.UserId = userId;

            // Lấy thông tin người nhận
            var receiver = db.Users.Find(receiverId);
            ViewBag.ReceiverId = receiverId;

            // Lấy lịch sử tin nhắn giữa hai người
            var messages = db.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToList();
            ViewBag.Messages = messages;

            return View();
        }

        public JsonResult SaveMessage(string sender, string receiver, string message)
        {
            string chatKey = $"{sender}_{receiver}_{DateTime.UtcNow.Ticks}";
            ChatLog.TryAdd(chatKey, $"{sender}: {message}");
            return Json(new { success = true });
        }

    }
}