using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Report GetDailyReport() => _reportRepository.GetDailyReport();
        public Report GetWeeklyReport() => _reportRepository.GetWeeklyReport();
        public Report GetMonthlyReport() => _reportRepository.GetMonthlyReport();
        public Dictionary<string, int> GetDashboardStats() => _reportRepository.GetDashboardStats();
    }
}