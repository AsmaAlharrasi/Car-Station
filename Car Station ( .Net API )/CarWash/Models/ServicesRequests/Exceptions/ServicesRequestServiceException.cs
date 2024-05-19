
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class ServicesRequestServiceException : Exception
    {
        public ServicesRequestServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}
