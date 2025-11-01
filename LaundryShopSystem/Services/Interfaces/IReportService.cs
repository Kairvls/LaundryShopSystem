using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Services.Interfaces
{
    public interface IReportService
    {
        Report GetDailyReport();
        Report GetWeeklyReport();
        Report GetMonthlyReport();
        Dictionary<string, int> GetDashboardStats();
    }
}