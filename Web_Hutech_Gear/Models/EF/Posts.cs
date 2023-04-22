using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Posts")]
    public class Posts : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không được để trống tiêu đề")]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Nội dung bài viết không được để trống")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Chi tiết bài viết không được để trống")]
        [AllowHtml]
        public string Detail { get; set; }

        [StringLength(250)]
        public string Image { get; set; }        
        public int NewsCategoryId { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}