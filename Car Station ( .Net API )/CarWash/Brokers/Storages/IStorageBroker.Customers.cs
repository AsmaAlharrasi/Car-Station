using CarWash.Models.Customers;
    

namespace CarWash.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Customer> InsertCustomer(Customer customer);
        IQueryable<Customer> SelectAllCustomers();
        ValueTask<Customer> SelectAsyncCustomer(Guid customerId);
        ValueTask<Customer> UpdateAsyncCustomer(Customer customer);
        ValueTask<Customer> DeleteAsyncCustomer(Customer customer);
    }
}
