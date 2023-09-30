using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class Cart
    {
        [ForeignKey("customer")]
        //[Key]
        public string CartID { get; set; }
        public string CartContant { get; set; }



        //[ForeignKey("customer"), Required]
        //public string id { get; set; }
        //public string currentUser { get; set; }


        //navigation property
        public virtual ApplicationUser customer { get; set; }
    }
}