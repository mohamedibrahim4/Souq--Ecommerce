using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDotNetV1.Models
{
    public class CustomerPhone
    {
        #region Composite Key
        [Key, Column(Order = 0)]
        public string PhoneNumber { get; set; }

        [Key, Column(Order = 1)]
        public string id { get; set; }
        #endregion


        // Navigation Properties
        public virtual ApplicationUser customer { get; set; }
    }
}