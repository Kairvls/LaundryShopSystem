using System;
using System.Collections.Generic;

namespace LaundryShopSystem.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public long OrderId { get; set; }               // ✅ Matches DB column
        public decimal AmountPaid { get; set; }         // ✅ Matches DB column
        public string PaymentMethod { get; set; }       // ✅ Matches DB column
        public DateTime PaidAt { get; set; }            // ✅ Matches DB column

        // Optional: navigation link to order
        public Order Order { get; set; }
        public string CustomerName => Order?.Customer?.FullName; // computed property
        public List<TransactionItem> Items { get; set; } = new List<TransactionItem>();

        // Optional: used in Receipt.cshtml to show QR
        public string QRCodeUrl { get; set; }

        public class TransactionItem
        {
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public string ServiceType { get; set; }
            public decimal Price { get; set; }
            public decimal Subtotal => Quantity * Price;
        }
    }
}
