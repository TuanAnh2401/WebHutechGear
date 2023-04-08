using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Hutech_Gear.Models.Support
{
    public class CommonAbstract
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Modifiedby { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}