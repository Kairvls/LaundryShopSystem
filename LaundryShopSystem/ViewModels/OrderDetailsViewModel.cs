using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Incident> Incidents { get; set; }
    }
}