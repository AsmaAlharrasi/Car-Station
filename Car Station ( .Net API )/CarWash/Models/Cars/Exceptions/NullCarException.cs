
namespace CarWash.Models.Cars.Exceptions
{
    public class NullCarException : Exception
    {
        public NullCarException() : base(message: "The car is null.") { }
    }
}
