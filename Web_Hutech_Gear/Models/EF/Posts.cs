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
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }
        [Required(ErrorMessage = " The Description field is required.")]
        [AllowHtml]
        public string Detail { get; set; }
        [Required(ErrorMessage = "The Detail field is required.")]

        [StringLength(250)]
        public string Image { get; set; }
        [Required(ErrorMessage = "The Image field is required.")]
        public int NewsCategoryId { get; set; }
        public virtual NewsCategory NewsCategory { get; set; }
    }
}