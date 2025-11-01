using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Models
{
	public class Customer
	{
        public long CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}