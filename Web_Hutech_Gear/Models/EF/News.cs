using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_News")]
    public class News : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không để trống tiêu đề tin")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [AllowHtml]
        public string Detail { get; set; }
        [Required(ErrorMessage = "The Detail field is required.")]
        public string Image { get; set; }
        [Required(ErrorMessage = "The Image field is required.")]
        public int NewsCategoryId { get; set; }
        [Required(ErrorMessage = "The NewsCategoryId field is required.")]
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsHot { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }

    }
}