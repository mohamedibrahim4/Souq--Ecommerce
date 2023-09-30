using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetV1.Models
{
    public enum OrderStatusType : byte
    {
        Padding = 0,
        Charging = 1,
        Recevied = 2
    }
}