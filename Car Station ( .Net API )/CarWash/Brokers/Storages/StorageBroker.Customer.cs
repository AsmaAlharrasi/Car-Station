using CarWash.Models.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Customer> customer { get; set; }

        //add

        public async ValueTask<Customer> InsertCustomer(Customer customer)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Customer> CustomerEntyiyEntry = await broker.customer.AddAsync(entity: customer);
            await broker.SaveChangesAsync();
            return CustomerEntyiyEntry.Entity;
        }

        //select all
        public IQueryable<Customer> SelectAllCustomers() => customer;


        //select by Id
        public async ValueTask<Customer> SelectAsyncCustomer(Guid customerId)
        {
            var broker = new StorageBroker(configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await customer.FindAsync(customerId);
        }

        //update
        public async ValueTask<Customer> UpdateAsyncCustomer(Customer customer)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Customer> CustomerEntyiyEntry = broker.customer.Update(entity: customer);
            await broker.SaveChangesAsync();
            return CustomerEntyiyEntry.Entity;
        }

        //delete
        public async ValueTask<Customer> DeleteAsyncCustomer(Customer customer)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Customer> CustomerEntyiyEntry = broker.customer.Remove(entity: customer);
            await broker.SaveChangesAsync();
            return CustomerEntyiyEntry.Entity;

        }
    }
}
