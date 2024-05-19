namespace CarWash.Models.ServicesModels.Exceptions
{
    public class NullServiceException : System.Exception
    {
        public NullServiceException() : base(message: "The service is null.") { }
    }
}
