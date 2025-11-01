using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public string ItemType { get; set; } // Clothes, Blanket, etc.
        public string FabricOrCareNotes { get; set; }
        public decimal Quantity { get; set; }
        public string UnitType { get; set; } // Kilo or Piece
        public string ServiceType { get; set; } // Wash, Dry, Fold, Iron
        public decimal PricePerUnit { get; set; }
        public decimal Subtotal { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}