namespace CarWash.Models.ServicesRequests.Exceptions
{
    public class AlreadyExistsServicesRequestException : Exception
    {
        public AlreadyExistsServicesRequestException(Exception innerException)
            : base(message: "Service Request with the same id already exists.", innerException) { }

    }
}
