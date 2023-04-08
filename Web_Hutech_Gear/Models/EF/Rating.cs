using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web_Hutech_Gear.Models.Support;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Rating")]

    public class Rating
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}