using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Reply")]

    public class Reply
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(128)]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public int RatedId { get; set; }
        public virtual Rated Rated { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}