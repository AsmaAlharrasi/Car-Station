namespace CarWash.Models.ServicesModels.Exceptions
{
    public class ServiceServiceException : Exception
    {
        public ServiceServiceException(System.Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }

    }
}
