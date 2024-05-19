using Xeptions;

namespace CarWash.Models.Cars.Exceptions
{
    public class FailedCarStorageException : Xeption
    {
        public FailedCarStorageException(Exception innerException)
            : base(message: "Failed post storage error occurred, contact support.", innerException)
        { }
    }
}