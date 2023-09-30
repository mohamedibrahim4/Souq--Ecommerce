using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class CartViewModel
    {
        public string name { get; set; }
        public double price { get; set; }
        public int AvailableQuantity { get; set; }
        public int count { get; set; }
    }
}