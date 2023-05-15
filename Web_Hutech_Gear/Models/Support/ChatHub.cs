using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;
using System.Web;
using Web_Hutech_Gear.Models.EF;

namespace Web_Hutech_Gear.Models.Support
{
    public class ChatHub : Hub
    {
        [Authorize]
        public async Task SendMessage(string sender, string receiver, string message)
        {
            string formattedMessage = $"{message}";
            Clients.User(receiver).receiveMessage(formattedMessage, sender);
            Clients.Caller.receiveMessage(formattedMessage, sender);
            // Lưu tin nhắn vào CSDL tại đây nếu bạn muốn
            var newMessage = new Messages
            {
                SenderId = sender,
                ReceiverId = receiver,
                Message = message,
                Timestamp = DateTime.UtcNow
            };
            using (var db = new ApplicationDbContext())
            {
                db.Messages.Add(newMessage);
                await db.SaveChangesAsync();
            }
        }

    }
}