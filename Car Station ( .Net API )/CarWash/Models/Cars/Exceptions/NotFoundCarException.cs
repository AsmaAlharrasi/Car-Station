

namespace CarWash.Models.Cars.Exceptions
{
    public class NotFoundCarException : Exception
    {
        public NotFoundCarException(Guid carId)
            : base(message: $"Couldn't find car with id: {carId}.") { }
    }
}
