using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Status")]

    public class Status
    {
        public Status()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsHot { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}