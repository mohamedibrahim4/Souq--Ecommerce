using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class OrderDetail
    {
        [ForeignKey("product")]

        public int OrderDetailID { set; get; }
        public int OrderQuantity { set; get; }
        public string ShippingType { set; get; }

        #region Derived Attributes
        //public double shippingPrice { set; get; }
        //public DateTime ArrivingDate { get; set; }
        //public double TotalPrice { get; set; }
        #endregion


        // foriegn Key
        //public int OrderID { set; get; }


        // Navigation Properites
        public virtual Product product { set; get; }
        //public virtual offer offer { set; get; }

        public virtual Order order { set; get; }
    }
}