
namespace CarWash.Models.Cars.Exceptions
{
    public class CarValidationException : Exception
    {
        public CarValidationException(Exception innerException)
            : base(message: "Invalid input, contact support.", innerException) { }
    }
}
