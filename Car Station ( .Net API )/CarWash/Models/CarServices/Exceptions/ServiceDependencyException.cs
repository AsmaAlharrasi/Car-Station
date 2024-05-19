namespace CarWash.Models.ServicesModels.Exceptions
{
    public class ServiceDependencyException : System.Exception
    {
        public ServiceDependencyException(System.Exception innerException)
            : base(message: "Service dependency error occurred, contact support.", innerException) { }

    }
}
