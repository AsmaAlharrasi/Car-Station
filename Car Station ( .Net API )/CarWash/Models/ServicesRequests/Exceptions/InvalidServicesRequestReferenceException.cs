
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class InvalidServicesRequestReferenceException : Exception
    {
        public InvalidServicesRequestReferenceException(Exception innerException)
            : base(message: "Invalid Service Request reference error occurred.", innerException) { }
    }
}
