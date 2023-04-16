using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Status")]

    public class Status : CommonAbstract
    {
        public Status()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}