using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Data;

namespace LaundryShopSystem.Repositories.Implementations
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly IDbConnectionFactory _db;
        public IncidentRepository(IDbConnectionFactory db) => _db = db;

        public long AddIncident(Incident incident)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"INSERT INTO Incidents (OrderId, Description, Images, CompensationAmount, CustomerNotified)
                            VALUES (@OrderId, @Description, @Images, @CompensationAmount, @CustomerNotified);
                            SELECT LAST_INSERT_ID();";
                return conn.ExecuteScalar<long>(sql, incident);
            }
        }

        public IEnumerable<Incident> GetIncidentsByOrder(long orderId)
        {
            using (var conn = _db.CreateConnection())
                return conn.Query<Incident>("SELECT * FROM Incidents WHERE OrderId = @oid", new { oid = orderId });
        }

        public void UpdateIncidentStatus(long incidentId, bool resolved, bool notified)
        {
            using (var conn = _db.CreateConnection())
            {
                var sql = @"UPDATE Incidents 
                            SET ResolvedAt = CASE WHEN @resolved THEN NOW() ELSE NULL END,
                                CustomerNotified = @notified 
                            WHERE IncidentId = @id";
                conn.Execute(sql, new { id = incidentId, resolved, notified });
            }
        }
    }
}