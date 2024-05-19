
using Xeptions;
namespace CarWash.Models.Cars.Exceptions
{
    public class InvalidCarException : Xeption
    {
        public InvalidCarException()
            : base("Car is invalid. Please fix the errors and try again.")
        { }
    }
}
