using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    //[Owned]
    public class CustomerAddressType

    {
        //public CustomerAddressType() { }

        //public CustomerAddressType(string _Country , string _City, string _Street, string _BuildingNumber)
        //{
            
        //    Country = _Country;
        //    City = _City;
        //    Street = _Street;
        //    BuildingNumber = _BuildingNumber;
            
            
        //}
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
    }
}