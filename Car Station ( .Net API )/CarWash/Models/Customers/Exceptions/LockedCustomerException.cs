using System;
namespace CarWash.Models.Customers.Exceptions
{
	public class LockedCustomerException:Exception
	{
        public LockedCustomerException(Exception innerException)
            : base(message: "Locked customer record exception, please try again later.", innerException) { }
    }
}

