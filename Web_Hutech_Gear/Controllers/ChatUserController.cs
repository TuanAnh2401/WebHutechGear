using Microsoft.AspNet.Identity;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;

namespace Web_Hutech_Gear.Controllers
{
    public class ChatUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static ConcurrentDictionary<string, string> ChatLog = new ConcurrentDictionary<string, string>();

        // GET: ChatUser
        public ActionResult Partial_Chat()
        {
            var userId = User.Identity.GetUserId();
            var adminUsers = db.Roles.FirstOrDefault(p=>p.Name=="admin").Users.FirstOrDefault();
            if (adminUsers.UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var messages = db.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == adminUsers.UserId) || (m.SenderId == adminUsers.UserId && m.ReceiverId == userId))
                .OrderBy(m => m.Timestamp)
                .ToList();

            ViewBag.AdminId = adminUsers.UserId;
            ViewBag.UserId = userId;
            ViewBag.Messages = messages;

            return PartialView();
        }
        public JsonResult SaveMessage(string sender, string receiver, string message)
        {
            string chatKey = $"{sender}_{receiver}_{DateTime.UtcNow.Ticks}";
            ChatLog.TryAdd(chatKey, $"{sender}: {message}");
            return Json(new { success = true });
        }
    }
}