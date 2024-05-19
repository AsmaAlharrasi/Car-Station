using Xeptions;

namespace CarWash.Models.Cars.Exceptions
{
    public class CarDependencyValidationException : Xeption
    {
        public CarDependencyValidationException(Xeption innerException)
            : base(message: "Car dependency validation error occurred, please try again.", innerException)
        { }

        public CarDependencyValidationException(AlreadyExistsCarException alreadyExistsCarException)
        {
        }
    }
}