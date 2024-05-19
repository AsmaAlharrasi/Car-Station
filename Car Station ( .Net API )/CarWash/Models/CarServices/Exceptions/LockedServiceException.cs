namespace CarWash.Models.ServicesModels.Exceptions
{
    public class LockedServiceException : System.Exception
    {
        public LockedServiceException(System.Exception innerException) 
            : base(message: "Locked service record exception, please try again later.", innerException) { }

    }
}
