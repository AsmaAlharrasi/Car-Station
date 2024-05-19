using System;
using Xeptions;

namespace CarWash.Models.Customers.Exceptions
{
	public class InvalidCustomerException : Xeption
    {
		public InvalidCustomerException(string parameterName, object parameterValue)
            : base(message: $"Invalid customer, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        {
		}

        public InvalidCustomerException()
            : base(message: "Invalid Customer. Please fix the errors and try again.") { }
    }
}

