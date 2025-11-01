using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Services.Implementations
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repo;

        public IncidentService(IIncidentRepository repo)
        {
            _repo = repo;
        }

        public long ReportIncident(Incident incident)
        {
            return _repo.AddIncident(incident);
        }

        public IEnumerable<Incident> GetIncidentsByOrder(long orderId)
        {
            return _repo.GetIncidentsByOrder(orderId);
        }

        public void ResolveIncident(long incidentId, bool notified)
        {
            _repo.UpdateIncidentStatus(incidentId, true, notified);
        }
    }
}