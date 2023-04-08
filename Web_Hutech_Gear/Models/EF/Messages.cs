using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

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