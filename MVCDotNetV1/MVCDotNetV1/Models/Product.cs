using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDotNetV1.Models
{
    public class Product
    {
        // Foreign Key
        [ForeignKey("orderDetail")]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string MadeIn { get; set; }
        public string SupplierName { get; set; }
        public double Weight { get; set; }
        public int AvailableQuantity { get; set; }


        // Foreign Key
        public int OrderDetailID { get; set; }


        #region Navigation Properties
        public virtual OrderDetail orderDetail { get; set; }
        public virtual List<CustomerWishList> customerWishLists { get; set; }
        #endregion
    }
}