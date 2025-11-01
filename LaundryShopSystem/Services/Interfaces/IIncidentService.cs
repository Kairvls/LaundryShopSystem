using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Services.Interfaces
{
    public interface IIncidentService
    {
        long ReportIncident(Incident incident);
        IEnumerable<Incident> GetIncidentsByOrder(long orderId);
        void ResolveIncident(long incidentId, bool notified);
    }
}