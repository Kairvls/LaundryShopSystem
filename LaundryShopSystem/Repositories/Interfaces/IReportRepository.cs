using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Report GetDailyReport();
        Report GetWeeklyReport();
        Report GetMonthlyReport();
        Dictionary<string, int> GetDashboardStats();
    }
}