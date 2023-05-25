using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_NewsCategory")]

    public class NewsCategory : CommonAbstract
    {
        public NewsCategory()
        {
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không được để trống tiêu đề tin")]
        [StringLength(150)]
        public string Title { get; set; }
        public bool IsActivate { get; set; }
        public ICollection<News> News { get; set; }

    }
}