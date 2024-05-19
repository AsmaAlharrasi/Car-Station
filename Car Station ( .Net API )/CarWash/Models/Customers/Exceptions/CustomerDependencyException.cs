using System;
using Xeptions;
namespace CarWash.Models.Customers.Exceptions
{
	public class CustomerDependencyException: Xeption
	{
        public CustomerDependencyException(Exception innerException)
           : base(message: "Customer dependency error occurred, contact support.", innerException) { }
    }
}

