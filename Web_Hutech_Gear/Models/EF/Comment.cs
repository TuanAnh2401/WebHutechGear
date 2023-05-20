using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Comment")]

    public class Comment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public int NewsId { get; set; }
        [Required(ErrorMessage = "Content không được để trống")]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual News News { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}