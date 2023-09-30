using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatusType OrderStatus { get; set; }


        //Forien key
        [ForeignKey("customer"), Required]
        public string id { get; set; }


        //navigation property
        public virtual ApplicationUser customer { get; set; }
        public virtual List<OrderDetail> orderDetails { get; set; }
    }
}