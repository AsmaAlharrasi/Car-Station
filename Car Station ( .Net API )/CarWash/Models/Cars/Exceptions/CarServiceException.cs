
namespace CarWash.Models.Cars.Exceptions
{
    public class CarServiceException : Exception
    {
        public CarServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}
