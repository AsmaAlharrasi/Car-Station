
using Xeptions;
namespace CarWash.Models.Cars.Exceptions
{
    public class FailedCarServiceException : Xeption
    {
        public FailedCarServiceException(Exception innerException)
            : base(message: " Failed car service error occurred, contact support",
                  innerException)
        { }
    }
}
