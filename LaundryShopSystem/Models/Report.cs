using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Models
{
    public class Report
    {
        public decimal TotalIncome { get; set; }
        public int TotalOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CancelledOrders { get; set; }
        public string Period { get; set; } // "Daily", "Weekly", or "Monthly"
    }

}