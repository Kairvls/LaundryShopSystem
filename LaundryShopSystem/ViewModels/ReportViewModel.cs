using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.ViewModels
{
    public class ReportViewModel
    {
        public Report Report { get; set; }
        public Dictionary<string, int> DashboardStats { get; set; }
    }
}