using System;
using Xeptions;

namespace CarWash.Models.Customers.Exceptions
{
	public class CustomerValidationException:Xeption
	{
        public CustomerValidationException(Exception innerException)
             : base(message: "Invalid input, contact support.", innerException) { }
    }
}

