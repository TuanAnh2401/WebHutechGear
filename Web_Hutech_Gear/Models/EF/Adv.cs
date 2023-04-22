using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Adv")]
    public class Adv : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không được để trống tiêu đề.")]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(500)]
        [Required(ErrorMessage = "Nội dung quảng cáo không được để trống.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Thêm ảnh quảng cáo.")]
        [StringLength(500)]
        public string Image { get; set; }
        [StringLength(500)]
        public string Link { get; set; }
    }
}