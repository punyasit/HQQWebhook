using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HQQWebReport.Data
{
    public class WebReportSettings
    {
        public int LimitStockMovement{ get; set; }
        public int LimitSaleMovement { get; set; }
        public int MaxReportDate { get; set; }
    }
}
