using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.VNPay;
using Web_Hutech_Gear.Models.Support;
using Web_Hutech_Gear.Models.EF;
using System.Collections.Generic;
using System.Text;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Web_Hutech_Gear.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        [Authorize]

        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart_Detail()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ApplyCoupon(string couponCode)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                // Kiểm tra xem mã giảm giá hợp lệ và áp dụng giảm giá cho từng sản phẩm trong giỏ hàng
                if (!string.IsNullOrEmpty(couponCode))
                {
                    var find = db.Coupon.FirstOrDefault(p => p.Code == couponCode && !(p.IsActivate));
                    // Giả sử mã giảm giá 5% là "DISCOUNT5"
                    if (find != null)
                    {
                        decimal discountRate = find.discount/100; // Phần trăm giảm giá (5%)

                        foreach (var item in cart.Items)
                        {
                            // Tính toán giá sản phẩm sau khi giảm giá
                            item.PriceSale = item.TotalPrice * discountRate ;
                            item.TotalPriceSale = item.PriceSale;
                        }
                    }
                }

                return PartialView("Partial_Item_Cart_Detail", cart.Items);
            }

            return Redirect("Index");
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    ProductImg = checkProduct.Image,
                    CategoryName = checkProduct.ProductCategory.Title,
                    SupplierName = checkProduct.Supplier.Title,
                    Quantity = quantity,
                };
                if(checkProduct.IsSale)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }else
                {
                    item.Price = checkProduct.Price;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm vào giở hàng thành công!", code = 1, Count = cart.Items.Count };

            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }



        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        public ActionResult Partial_CheckOut()
        {
            var ID = User.Identity.GetUserId();
            var findUser = db.Users.FirstOrDefault(p => p.Id == ID);
            return PartialView("Partial_CheckOut", findUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(int TypePayment)
        {

            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(p => p.Id == userId);
            var cart = (ShoppingCart)Session["Cart"];

            if (user == null)
            {
                return Json(new { success = false, message = "Gặp lỗi khi đặt hàng: Hãy kiểm tra lại Tài khoản đăng nhập" });
            }
            if (cart == null)
            {
                return Json(new { success = false, message = "Gặp lỗi khi đặt hàng: Hãy kiểm tra lại Giỏ hàng của bạn" });

            }
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        UserId = user.Id,
                        OrderDetails = cart.Items.Select(x => new OrderDetail
                        {
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            Price = x.PriceSale > 0 ? (x.Price - x.PriceSale) : x.Price
                        }).ToList(),
                        TotalAmount = cart.Items.Sum(x => x.TotalPrice-x.TotalPriceSale),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = user.UserName,
                        TypePayment = TypePayment,
                        Quantity = cart.Items.Count()
                    };
                    var errorMessage = "";
                    foreach (var sp in cart.Items)
                    {
                        var findPr = db.Products.FirstOrDefault(p => p.Id == sp.ProductId);
                        if (findPr != null && findPr.Quantity >= sp.Quantity)
                            findPr.Quantity = findPr.Quantity - sp.Quantity;
                        else
                            errorMessage += $"Số lượng sản phẩm {findPr.Title} trong kho không đủ!\n";

                        if (findPr.Quantity == 0)
                        {
                            findPr.IsStatus = true;
                            db.Products.Attach(findPr);
                            db.Entry(findPr).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        // Return error message as JSON
                        return Json(new { success = false, message = errorMessage });
                    }
                    if (order.TypePayment == 1)
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();
                    }
                    transaction.Commit();

                    if (TypePayment == 1)
                    {
                        SendOrderEmail(order, user);
                        Session["Cart"] = null;
                        return Json(new { success = true, url = Url.Action("PaymentConfirm", "ShoppingCart") });

                    }
                    else
                    {
                        Session["Order"] = order;
                        return Json(new { success = true, url = Url.Action("Payment", "ShoppingCart") });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Gặp lỗi khi đặt hàng: " + ex.Message });
                }
            }
        }

        public ActionResult Payment()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();
            long thanhToan = (int)cart.Items.Sum(x => x.TotalPrice - x.TotalPriceSale);
            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (thanhToan * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }
        public ActionResult PaymentConfirm()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(p => p.Id == userId);
            Order order = (Order)Session["Order"];
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        using (DbContextTransaction transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.Orders.Add(order);
                                foreach (var item in cart.Items)
                                {
                                    var findPr = db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                                    if (findPr != null && findPr.Quantity >= item.Quantity)
                                        findPr.Quantity = findPr.Quantity - item.Quantity;
                                    if (findPr.Quantity == 0)
                                    {
                                        findPr.IsStatus = true;
                                        db.Products.Attach(findPr);
                                        db.Entry(findPr).State = System.Data.Entity.EntityState.Modified;
                                    }
                                }
                                db.SaveChanges();
                                SendOrderEmail(order, user);
                                Session["Cart"] = null;
                                Session["Order"] = null;
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                ViewBag.Message = "Có lỗi xảy ra khi thanh toán"+ex.Message;
                            }
                        }
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + "| \n Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | \n Mã giao dịch: " + vnpayTranId + " | Mã lỗi: \n" + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return View();
        }
        public void SendOrderEmail(Order order, ApplicationUser user)
        {
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
            contentCustomer = contentCustomer.Replace("{{MaDon}}", "HD" + order.Id);
            contentCustomer = contentCustomer.Replace("{{SanPham}}", GetOrderProducts());
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", user.FullName);
            contentCustomer = contentCustomer.Replace("{{Phone}}", user.PhoneNumber);
            contentCustomer = contentCustomer.Replace("{{Email}}", user.Email);
            contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", user.Address);
            contentCustomer = contentCustomer.Replace("{{ThanhTien}}", Common.FormatNumber(GetOrderTotal(), 0));
            contentCustomer = contentCustomer.Replace("{{TongTien}}", Common.FormatNumber(order.TotalAmount, 0));
            Common.SendMail("HutechGear", "Đơn hàng #" + "HD" + order.Id, contentCustomer, user.Email);

            string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
            contentAdmin = contentAdmin.Replace("{{MaDon}}", "HD" + order.Id);
            contentAdmin = contentAdmin.Replace("{{SanPham}}", GetOrderProducts());
            contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", user.FullName);
            contentAdmin = contentAdmin.Replace("{{Phone}}", user.PhoneNumber);
            contentAdmin = contentAdmin.Replace("{{Email}}", user.Email);
            contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", user.Address);
            contentAdmin = contentAdmin.Replace("{{ThanhTien}}", Common.FormatNumber(GetOrderTotal(), 0));
            contentAdmin = contentAdmin.Replace("{{TongTien}}", Common.FormatNumber(order.TotalAmount, 0));
            Common.SendMail("HutechGear", "Đơn hàng mới #" + "HD" + order.Id, contentAdmin, ConfigurationManager.AppSettings["EmailAdmin"]);
        }

        private string GetOrderProducts()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            StringBuilder sb = new StringBuilder();
            foreach (var item in cart.Items)
            {
                var product = db.Products.FirstOrDefault(p => p.Id == item.ProductId);

                string productName = product?.Title ?? "Unknown";
                string formattedPrice = Common.FormatNumber(item.Price, 0);
                string totalPrice = Common.FormatNumber(item.Quantity * item.Price, 0);

                sb.AppendLine($"<tr><td>{productName}</td><td>{formattedPrice}</td><td>{item.Quantity}</td><td>{totalPrice}</td></tr>");
            }
            return sb.ToString();

        }

        private decimal GetOrderTotal()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            return cart.Items.Sum(x => x.TotalPrice);
        }
    }
}