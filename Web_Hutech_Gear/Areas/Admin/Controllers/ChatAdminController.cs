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
            return View();
        }
        public ActionResult Partial_List()
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
                    LastMessage = g.OrderByDescending(m => m.Timestamp).FirstOrDefault().Message,
                    Avatar = g.Key.Avatar,
                    Timestamp = g.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp,
                    IsRead = g.Where(m => m.IsRead == false).Count()
                }).ToList();
            ViewBag.ListChat = messages;
            return PartialView("Partial_List", ViewBag.ListChat);
        }
        public ActionResult Chat(string receiverId)
        {
            // Lấy thông tin user hiện tại
            var userId = User.Identity.GetUserId();
            ViewBag.UserId = userId;

            // Lấy thông tin người nhận
            var receiver = db.Users.Find(receiverId);
            ViewBag.ReceiverId = receiverId;
            ViewBag.ReceviverName = receiver.FullName;

            // Lấy lịch sử tin nhắn giữa hai người
            var messages = db.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToList();
            ViewBag.Messages = messages;

            if (messages != null)
            {
                foreach(var mess in messages)
                {
                    mess.IsRead = true;
                    db.Entry(mess).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }

            return View();
        }

        public JsonResult SaveMessage(string sender, string receiver, string message)
        {
            string chatKey = $"{sender}_{receiver}_{DateTime.UtcNow.Ticks}";
            ChatLog.TryAdd(chatKey, $"{sender}: {message}");
            return Json(new { success = true });
        }
        public ActionResult Partial_Chat()
        {
            // Lấy thông tin user hiện tại
            var userId = User.Identity.GetUserId();
            ViewBag.CountChat = db.Messages.Where(p => p.IsRead == false && p.ReceiverId == userId).GroupBy(p => p.SenderId).Count();

            // Trả về partial view chứa phần tử countChatBadge
            return PartialView("Partial_Chat", ViewBag.CountChat);
        }


    }
}