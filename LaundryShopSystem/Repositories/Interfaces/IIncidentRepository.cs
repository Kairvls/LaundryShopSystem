using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;

namespace LaundryShopSystem.Repositories.Interfaces
{
	public interface IIncidentRepository
    {
        long AddIncident(Incident incident);
        IEnumerable<Incident> GetIncidentsByOrder(long orderId);
        void UpdateIncidentStatus(long incidentId, bool resolved, bool notified);
    }
}