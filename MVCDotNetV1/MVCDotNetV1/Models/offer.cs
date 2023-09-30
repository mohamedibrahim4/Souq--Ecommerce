using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCDotNetV1.Models
{
    public class offer
    {
        // Foreign Key
        //[ForeignKey("orderDetail")]
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        //public string MainCategory { get; set; }
        //public string SubCategory { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string MadeIn { get; set; }
        public string SupplierName { get; set; }
        public double Weight { get; set; }
        public int AvailableQuantity { get; set; }


      


        //#region Navigation Properties
        //public virtual List<CustomerWishList> customerWishLists { get; set; }
        //#endregion
    }
}