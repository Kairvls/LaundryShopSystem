using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public string CustomerName { get; set; }      // ✅ Add this
        public string ContactNumber { get; set; }     // ✅ Add this
        public long CustomerId { get; set; }
        public string ServiceMode { get; set; } // Rush or Regular
        public string Status { get; set; } // Queued, Washing, Drying, etc.
        public decimal TotalAmount { get; set; }
        public string QRCode { get; set; } // Path or token
        public DateTime DropoffAt { get; set; }
        public DateTime? ReadyAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Incident> Incidents { get; set; }
    }

}