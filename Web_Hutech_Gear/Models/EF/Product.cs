using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Product")]
    public class Product : CommonAbstract
    {
        public Product()
        {
            this.ProductImage = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Comment = new HashSet<Comment>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Description { get; set; }
        
        [AllowHtml]
        [Required(ErrorMessage = "Chi tiết không được để trống.")]
        public string Detail { get; set; }

        [StringLength(250)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Giá nhập không được để trống")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Giá nhập phải lớn hơn 0.")]
        public decimal OriginalPrice { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Giá khuyến mãi phải lớn hơn 0.")]
        public decimal? PriceSale { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Loại sản phẩm không được để trống")]
        public int ProductCategoryId { get; set; }
        [Required(ErrorMessage = "Nhà phân phối không được để trống")]
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public int StatusId { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsHot { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}