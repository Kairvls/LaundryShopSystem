using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [Required]
        public string ServiceMode { get; set; } // Rush or Regular
        public string Notes { get; set; }

        public List<OrderItem> Items { get; set; }

        public CreateOrderViewModel()
        {
            Items = new List<OrderItem>();
        }
    }
}