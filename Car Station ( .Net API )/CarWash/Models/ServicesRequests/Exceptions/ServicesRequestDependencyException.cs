
namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class ServicesRequestDependencyException : Exception
    {
        public ServicesRequestDependencyException(Exception innerException)
            : base(message: "Service dependency error occurred, contact support.", innerException) { }
    }
}
