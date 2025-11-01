using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryShopSystem.Models;
using LaundryShopSystem.Repositories.Interfaces;
using LaundryShopSystem.Services.Interfaces;

namespace LaundryShopSystem.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public long RegisterCustomer(Customer customer)
        {
            var existing = _repo.GetCustomerByPhone(customer.Phone);
            if (existing != null)
                return existing.CustomerId;
            return _repo.AddCustomer(customer);
        }

        public Customer GetCustomerByPhone(string phone)
        {
            return _repo.GetCustomerByPhone(phone);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }
    }
}