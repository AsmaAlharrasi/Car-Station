
namespace CarWash.Models.ServicesModels.Exceptions
{
    public class InvalidServiceReferenceException : Exception
    {
        public InvalidServiceReferenceException(Exception innerException)
            : base(message: "Invalid service reference error occurred.", innerException) { }
    }
}
