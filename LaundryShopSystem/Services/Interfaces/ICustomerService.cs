using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;

namespace LaundryShopSystem.Services.Interfaces
{
    public interface ICustomerService
    {
        long RegisterCustomer(Customer customer);
        Customer GetCustomerByPhone(string phone);
        IEnumerable<Customer> GetAllCustomers();
    }
}