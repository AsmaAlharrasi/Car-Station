using System;
namespace CarWash.Models.Customers.Exceptions
{
	public class NullCustomerException:Exception
	{
		public NullCustomerException() : base(message: "The customer is null.") { }
		
	}
}

