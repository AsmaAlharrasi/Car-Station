
using Xeptions;
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class FailedServicesRequestServiceException : Xeption
    {
        public FailedServicesRequestServiceException(Exception innerException)
            : base(message: " Failed Service Request service error occurred, contact support",
                  innerException)
        { }
    }
}
