
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class NullServicesRequestException : Exception
    {
        public NullServicesRequestException() : base(message: "The Service Request is null.") { }
    }
}
