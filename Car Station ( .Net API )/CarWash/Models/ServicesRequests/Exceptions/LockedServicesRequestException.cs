
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class LockedServicesRequestException : Exception
    {
        public LockedServicesRequestException(Exception innerException)
            : base(message: "Locked Service Request record exception, please try again later.", innerException) { }
    }
}
