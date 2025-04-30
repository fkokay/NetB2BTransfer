﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.B2B.Library.Models
{
    public class B2BResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public object Validation { get; set; }
        public object Detay { get; set; }
    }
}
