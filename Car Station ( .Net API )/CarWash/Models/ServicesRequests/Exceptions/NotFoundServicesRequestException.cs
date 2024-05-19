
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class NotFoundServicesRequestException : Exception
    {
        public NotFoundServicesRequestException(Guid serviceRequestId)
            : base(message: $"Couldn't find Service Request with id: {serviceRequestId}.") { }
    }
}
