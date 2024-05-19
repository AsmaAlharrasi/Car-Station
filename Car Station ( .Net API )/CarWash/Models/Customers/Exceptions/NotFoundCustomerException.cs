using System;
namespace CarWash.Models.Customers.Exceptions
{
	public class NotFoundCustomerException:Exception
	{
        public NotFoundCustomerException(Guid customerId)
            : base(message: $"Couldn't find service with id: {customerId}.") { }
    }
}

