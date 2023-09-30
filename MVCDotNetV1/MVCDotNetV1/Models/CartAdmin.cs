using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class CartAdmin
    {
        [Key]
        public int CartAdminID { get; set; }
        public string BuyerID { get; set; }
        public string BuyerName { get; set; }
        public string CartContent { get; set; }
        public DateTime? AddTime { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActionTaken { get; set; }
    }
}