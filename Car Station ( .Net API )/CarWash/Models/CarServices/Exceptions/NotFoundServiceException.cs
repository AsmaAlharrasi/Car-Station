namespace CarWash.Models.ServicesModels.Exceptions
{
    public class NotFoundServiceException : IOException
    {
        public NotFoundServiceException(Guid serviceId)
            : base(message: $"Couldn't find service with id: {serviceId}.") { }
    }
}
