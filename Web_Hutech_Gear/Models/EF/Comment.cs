﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Hutech_Gear.Models.EF
{
    [Table("tb_Comment")]

    public class Comment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(128)]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}