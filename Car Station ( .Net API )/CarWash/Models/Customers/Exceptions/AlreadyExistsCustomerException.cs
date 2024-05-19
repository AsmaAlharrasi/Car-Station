using System;
using Xeptions;

namespace CarWash.Models.Customers.Exceptions
{
	public class AlreadyExistsCustomerException : Xeption
    {
        public AlreadyExistsCustomerException(Exception innerException)
            : base(message: "Customer with the same id already exists.", innerException) { }
    }
}

