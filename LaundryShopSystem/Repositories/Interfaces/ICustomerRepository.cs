using LaundryShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryShopSystem.Repositories.Interfaces
{
	public interface ICustomerRepository
	{
        long AddCustomer(Customer customer);
        Customer GetCustomerById(long id);
        Customer GetCustomerByPhone(string phone);
        IEnumerable<Customer> GetAllCustomers();
    }
}