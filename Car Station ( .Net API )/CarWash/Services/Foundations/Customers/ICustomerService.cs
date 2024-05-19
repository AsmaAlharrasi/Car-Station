using System;
using CarWash.Models.Customers;

namespace CarWash.Services.Foundations.Customers
{
	public interface ICustomerService
	{
        ValueTask<Customer> AddCustomerAsync(Customer customer);
        IQueryable<Customer> RetrieveAllCustomers();
        ValueTask<Customer> RetrieveCustomerByIdAsync(Guid customerId);
        ValueTask<Customer> ModifyCustomerAsync(Customer customer);
        ValueTask<Customer> RemoveCustomerByIdAsync(Guid customerId);
    }
}

