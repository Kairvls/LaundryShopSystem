using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public ActionResult Daily() => View(_reportService.GetDailyReport());
        public ActionResult Weekly() => View(_reportService.GetWeeklyReport());
        public ActionResult Monthly() => View(_reportService.GetMonthlyReport());
    }
}