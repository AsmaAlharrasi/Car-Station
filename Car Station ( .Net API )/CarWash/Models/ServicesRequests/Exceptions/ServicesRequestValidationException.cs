
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class ServicesRequestValidationException : Exception
    {
        public ServicesRequestValidationException(Exception innerException)
            : base(message: "Invalid input, contact support.", innerException) { }
    }
}
