using System;
namespace CarWash.Models.Customers.Exceptions
{
	public class FailedCustomerServiceException:Exception
	{
        public FailedCustomerServiceException(Exception innerException)
           : base(message: "Failed assignment customer error occured, contact support.", innerException) { }
    }
}

