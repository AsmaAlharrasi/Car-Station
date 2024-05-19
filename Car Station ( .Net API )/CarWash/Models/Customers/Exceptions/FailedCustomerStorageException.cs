using Xeptions;

namespace CarWash.Models.Customers.Exceptions
{
    public class FailedCustomerStorageException : Xeption
    {
        public FailedCustomerStorageException(Exception innerException)
            : base(message: "Failed post storage error occurred, contact support.", innerException)
        { }
    }
}