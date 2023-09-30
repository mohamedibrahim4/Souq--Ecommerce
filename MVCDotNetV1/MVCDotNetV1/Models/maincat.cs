using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public class maincat
    {
        public int maincatID { get; set; }
        public string maincatName { get; set; }
        public virtual List<subcat> Subcats { get; set; }


    }
}