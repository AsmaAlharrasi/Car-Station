namespace CarWash.Models.ServicesModels.Exceptions
{
    public class AlreadyExistsServiceException : System.Exception
    {
        public AlreadyExistsServiceException(System.Exception innerException)
            : base(message: "Service with the same id already exists.", innerException) { }
    }
}
