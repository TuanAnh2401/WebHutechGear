using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataHandler.Encoder;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }
        [HttpPost]
        public ActionResult IsOrder(int id)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                item.IsOrder = true;
                item.Modifiedby = User.Identity.GetUserName();
                item.ModifiedDate = DateTime.Now;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public FileResult ExportToExcel()
        {
            IEnumerable<Order> items = db.Orders.OrderByDescending(x => x.Id);
            // Tạo một tệp Excel mới
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Order Data");

                // Đặt tiêu đề cột
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Mã đơn hàng";
                worksheet.Cells[1, 3].Value = "Tên khách hàng";
                worksheet.Cells[1, 4].Value = "Số điện thoại";
                worksheet.Cells[1, 5].Value = "Tiền";
                worksheet.Cells[1, 6].Value = "Ngày tạo";
                worksheet.Cells[1, 7].Value = "Tình trạng";

                // Đổ dữ liệu vào các ô
                int row = 2,i = 1;
                foreach (var item in items)
                {
                    var find = db.Users.FirstOrDefault(p => p.Id == item.UserId);
                    worksheet.Cells[row, 1].Value = i;
                    worksheet.Cells[row, 2].Value = "HD: " + item.Id;
                    worksheet.Cells[row, 3].Value = find.FullName;
                    worksheet.Cells[row, 4].Value = find.PhoneNumber;
                    worksheet.Cells[row, 5].Value = Common.FormatNumber(item.TotalAmount, 0);
                    worksheet.Cells[row, 6].Value = item.CreatedDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 7].Value = item.TypePayment == 1 ? "Chờ thành toán" : "Đã thanh toán";
                    row++;
                    i++;
                }

                // Tùy chỉnh định dạng và kiểu dữ liệu cho các ô
                using (var range = worksheet.Cells[1, 1, row - 1, 7])
                {
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Font.Bold = true;
                }

                // AutoFit các cột
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Chuyển đổi tệp Excel thành một mảng byte
                byte[] excelBytes = package.GetAsByteArray();

                // Trả về tệp Excel dưới dạng tải về
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "order_data.xlsx");
            }
        }
    }
}
