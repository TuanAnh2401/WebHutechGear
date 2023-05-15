using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System.Text;

namespace Web_Hutech_Gear.Models.Support
{
    public class MyIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            // Lấy context HTTP từ yêu cầu
            var context = request.GetHttpContext();

            // Kiểm tra xem người dùng đã xác thực hay chưa
            if (context.User.Identity.IsAuthenticated)
            {
                // Lấy ID của người dùng từ định danh hiện tại
                string userId = context.User.Identity.GetUserId();
                return userId;
            }

            // Trả về giá trị mặc định nếu người dùng không xác thực
            return null;
        }
    }
}