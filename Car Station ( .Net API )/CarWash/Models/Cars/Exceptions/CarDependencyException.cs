
namespace CarWash.Models.Cars.Exceptions
{
    public class CarDependencyException : Exception
    {
        public CarDependencyException(Exception innerException)
            : base(message: "Service dependency error occurred, contact support.", innerException) { }
    }
}
