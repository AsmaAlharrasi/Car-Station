
namespace CarWash.Models.Cars.Exceptions
{
    public class InvalidServiceReferenceException : Exception
    {
        public InvalidServiceReferenceException(Exception innerException)
            : base(message: "Invalid car reference error occurred.", innerException) { }
    }
}
