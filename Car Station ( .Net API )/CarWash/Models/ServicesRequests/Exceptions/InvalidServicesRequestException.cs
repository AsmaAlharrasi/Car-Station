
using Xeptions;
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class InvalidServicesRequestException : Xeption
    {
        public InvalidServicesRequestException()
            : base("Services Request is invalid. Please fix the errors and try again.")
        { }
    }
}
