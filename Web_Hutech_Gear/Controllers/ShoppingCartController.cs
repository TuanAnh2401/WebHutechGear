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


        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }

        static ApplicationUser find = null;
        static Order order = null;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            IdentityDbContext<ApplicationUser> context = new IdentityDbContext<ApplicationUser>();
            var ID = User.Identity.GetUserId();
            find = context.Users.FirstOrDefault(p => p.Id == ID);
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (cart != null)
                        {
                            order = new Order();
                            order.UserId = find.Id;
                            cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                            {
                                ProductId = x.ProductId,
                                Quantity = x.Quantity,
                                Price = x.Price
                            }));
                            order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                            order.CreatedDate = DateTime.Now;
                            order.ModifiedDate = DateTime.Now;
                            order.CreatedBy = find.PhoneNumber;
                            order.TypePayment = req.TypePayment;
                            //send mail cho khachs hang
                            if (order.TypePayment == 1)
                            {
                                db.Orders.Add(order);
                                db.SaveChanges();
                            }
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Content("Gặp lỗi khi đặt hàng: " + ex.Message);
                    }
                }
                var strSanPham = "";
                var thanhtien = decimal.Zero;
                var TongTien = decimal.Zero;
                if (req.TypePayment == 1)
                {
                    foreach (var sp in cart.Items)
                    {
                        var findPr = db.Products.FirstOrDefault(p => p.Id == sp.ProductId);
                        findPr.Quantity = findPr.Quantity - sp.Quantity;
                        db.SaveChanges();
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                        strSanPham += "</tr>";
                        thanhtien += sp.Price * sp.Quantity;
                    }
                    TongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}","HD" + order.Id);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", find.FullName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", find.PhoneNumber);
                    contentCustomer = contentCustomer.Replace("{{Email}}", find.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", find.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}",Common.FormatNumber(TongTien, 0));
                    Common.SendMail("ShopOnline", "Đơn hàng #" + "HD" + order.Id, contentCustomer.ToString(), find.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", "HD" + order.Id);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", find.FullName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", find.PhoneNumber);
                    contentAdmin = contentAdmin.Replace("{{Email}}", find.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", find.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}",Common.FormatNumber(TongTien, 0));
                    Common.SendMail("ShopOnline", "Đơn hàng mới #" + "HD" + order.Id, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    Session["Cart"] = null;
                    return View("PaymentConfirm");

                }
                else
                {
                    return RedirectToAction("Payment", "ShoppingCart");

                }

            }
            return Json(new { Success = false, Code = -1 });
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            var findPr = db.Products.FirstOrDefault(p => p.Id == id);
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
                    CategoryName = checkProduct.ProductCategory.Title,
                    SupplierName = checkProduct.Supplier.Title,
                    Quantity = quantity,
                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
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
        public ActionResult Payment()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();
            int thanhToan = (int)cart.Items.Sum(p => p.Quantity * p.Price);
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
                        db.Orders.Add(order);
                        db.SaveChanges();
                        var strSanPham = "";
                        var thanhtien = decimal.Zero;
                        var TongTien = decimal.Zero;
                        foreach (var sp in cart.Items)
                        {
                            var findPr = db.Products.FirstOrDefault(p => p.Id == sp.ProductId);
                            findPr.Quantity = findPr.Quantity - sp.Quantity;
                            db.SaveChanges();
                            strSanPham += "<tr>";
                            strSanPham += "<td>" + sp.ProductName + "</td>";
                            strSanPham += "<td>" + sp.Quantity + "</td>";
                            strSanPham += "<td>" + Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                            strSanPham += "</tr>";
                            thanhtien += sp.Price * sp.Quantity;
                        }
                        TongTien = thanhtien;
                        string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                        contentCustomer = contentCustomer.Replace("{{MaDon}}","HD"+ order.Id);
                        contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                        contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                        contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", find.FullName);
                        contentCustomer = contentCustomer.Replace("{{Phone}}", find.FullName);
                        contentCustomer = contentCustomer.Replace("{{Email}}", find.Email);
                        contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", find.Address);
                        contentCustomer = contentCustomer.Replace("{{ThanhTien}}", Common.FormatNumber(thanhtien, 0));
                        contentCustomer = contentCustomer.Replace("{{TongTien}}", Common.FormatNumber(TongTien, 0));
                        Common.SendMail("ShopOnline", "Đơn hàng #" + "HD" + order.Id, contentCustomer.ToString(), find.Email);

                        string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                        contentAdmin = contentAdmin.Replace("{{MaDon}}", "HD" + order.Id);
                        contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                        contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                        contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", find.FullName);
                        contentAdmin = contentAdmin.Replace("{{Phone}}", find.PhoneNumber);
                        contentAdmin = contentAdmin.Replace("{{Email}}", find.Email);
                        contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", find.Address);
                        contentAdmin = contentAdmin.Replace("{{ThanhTien}}", Common.FormatNumber(thanhtien, 0));
                        contentAdmin = contentAdmin.Replace("{{TongTien}}", Common.FormatNumber(TongTien, 0));
                        Common.SendMail("ShopOnline", "Đơn hàng mới #" + "HD" + order.Id, contentAdmin.ToString(), "lokino.00002@gmail.com");
                        Session["Cart"] = null;
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
    }
}