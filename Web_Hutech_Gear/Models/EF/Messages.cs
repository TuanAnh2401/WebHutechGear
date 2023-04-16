using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Messages")]

    public class Messages
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}