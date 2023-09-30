using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class subcat
    {
        public int subcatId { get; set; }
        public string subcatName { get; set; }
        [ForeignKey("Maincat")]

        public int maincatID { get; set; }

        public maincat Maincat { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}