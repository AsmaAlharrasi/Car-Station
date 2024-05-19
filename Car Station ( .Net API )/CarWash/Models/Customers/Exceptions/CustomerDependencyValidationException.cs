using Xeptions;

namespace CarWash.Models.Customers.Exceptions
{
    public class CustomerDependencyValidationException : Xeption
    {
        public CustomerDependencyValidationException(Xeption innerException)
            : base(message: "Customer dependency validation error occurred, please try again.", innerException)
        { }

        public CustomerDependencyValidationException(AlreadyExistsCustomerException alreadyExistsCustomerException)
        {
        }
    }
}