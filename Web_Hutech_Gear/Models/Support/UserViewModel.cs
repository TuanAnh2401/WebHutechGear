﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Hutech_Gear.Models.Support
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public DateTime Timestamp { get; set; }
        public int IsRead { get; set; }

        public string LastMessage { get; set; }

    }
}