using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    public class Coupon : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không được để trống mã code.")]
        [StringLength(150)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Không được để trống khuyến mãi.")]
        [Range(1, 100, ErrorMessage = "Khuyến mãi phải nằm trong khoảng tỉ lệ 1 đến 100 %.")]
        public decimal discount { get; set; }
        public bool IsActivate { get; set; }
    }
}