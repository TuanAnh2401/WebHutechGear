﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Hutech_Gear.Models.Support;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory : CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Thêm ảnh sản phẩm")]
        public string Image { get; set; }
        public bool IsActivate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}