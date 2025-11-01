using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Models
{
    public class Incident
    {
        public long IncidentId { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public string Images { get; set; } // Comma-separated paths or URLs
        public decimal CompensationAmount { get; set; }
        public DateTime ReportedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public bool CustomerNotified { get; set; }

        // Navigation
        public Order Order { get; set; }
        
    }
}