using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDotNetV1.Models
{
    public class CustomerWishList
    {
        #region Composite Key
        [Key, Column(Order = 0)]
        public string id { get; set; }

        [Key, Column(Order = 1)]
        public int ProductID { get; set; }
        #endregion


        public DateTime DateOfAdding { get; set; }


        #region Navigation Properties
        public virtual ApplicationUser customer { get; set; }
        public virtual Product product { get; set; }
        #endregion
    }
}