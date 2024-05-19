

using Xeptions;

namespace CarWash.Models.ServicesModels.Exceptions
{
    public class ServiceValidationException : Xeption
    {
        public ServiceValidationException(System.Exception innerException)
             : base(message: "Invalid input, contact support.", innerException) { }
    }
}
