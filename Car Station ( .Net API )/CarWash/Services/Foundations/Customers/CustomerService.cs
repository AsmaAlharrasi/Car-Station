using System;
using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.Customers;

namespace CarWash.Services.Foundations.Customers
{
	public partial class CustomerService :ICustomerService
	{
		private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;


        public CustomerService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        //add
        public ValueTask<Customer> AddCustomerAsync(Customer customer) =>
            TryCatch(async () =>
            {
                return await storageBroker.InsertCustomer(customer);
            });

        //update 
        public ValueTask<Customer> ModifyCustomerAsync(Customer customer) =>
            TryCatch(async () =>
            {
                Customer maybeCustomer = await this.storageBroker.SelectAsyncCustomer(customer.Id);
                return await storageBroker.UpdateAsyncCustomer(customer);

            });

        //delete
        public ValueTask<Customer> RemoveCustomerByIdAsync(Guid customerId) =>
            TryCatch(async () =>
            {
                Customer maybeCustomer = await this.storageBroker.SelectAsyncCustomer(customerId);
                return await this.storageBroker.DeleteAsyncCustomer(maybeCustomer);
            });

        //select all  
        public IQueryable<Customer> RetrieveAllCustomers() =>
             TryCatch(() => this.storageBroker.SelectAllCustomers());

        //select by Id
        public ValueTask<Customer> RetrieveCustomerByIdAsync(Guid customerId) =>
            TryCatch(async () =>
            {
                Customer maybeCustomer = await this.storageBroker.SelectAsyncCustomer(customerId);
                return maybeCustomer;
            });

    }
}


