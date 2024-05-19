using System;
namespace CarWash.Models.Customers.Exceptions
{
	public class CustomerServiceException:Exception
	{
        public CustomerServiceException(Exception innerException)
            : base(message: "Customer error occurred, contact support.", innerException) { }
    }
}

