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
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public int NewsCategoryId { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsHot { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }

    }
}