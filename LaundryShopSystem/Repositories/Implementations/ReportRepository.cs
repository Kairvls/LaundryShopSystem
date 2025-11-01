using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using LaundryShopSystem.Data;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;

namespace LaundryShopSystem.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ReportRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Report GetDailyReport() => GetReport("DAY");
        public Report GetWeeklyReport() => GetReport("WEEK");
        public Report GetMonthlyReport() => GetReport("MONTH");

        private Report GetReport(string type)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                string query = $@"
                    SELECT 
                        SUM(TotalAmount) AS TotalIncome,
                        COUNT(OrderId) AS TotalOrders,
                        SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedOrders,
                        SUM(CASE WHEN Status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledOrders
                    FROM Orders
                    WHERE {type}(CreatedAt) = {type}(CURRENT_DATE());";

                var result = conn.QueryFirstOrDefault<Report>(query);
                if (result == null)
                {
                    result = new Report
                    {
                        TotalIncome = 0,
                        TotalOrders = 0,
                        CompletedOrders = 0,
                        CancelledOrders = 0,
                        Period = type
                    };
                }
                else
                {
                    result.Period = type;
                }

                return result;
            }
        }

        public Dictionary<string, int> GetDashboardStats()
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                string query = @"
                    SELECT Status, COUNT(*) AS Count 
                    FROM Orders 
                    GROUP BY Status;";

                var results = conn.Query(query).ToList();
                var stats = new Dictionary<string, int>();
                foreach (var row in results)
                {
                    stats.Add(row.Status, (int)row.Count);
                }
                return stats;
            }
        }
    }
}