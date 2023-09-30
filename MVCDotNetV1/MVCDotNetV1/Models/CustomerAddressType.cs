using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    //[Owned]
    public class CustomerAddressType
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
    }
}