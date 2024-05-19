namespace CarWash.Models.Cars.Exceptions
{
    public class AlreadyExistsCarException: Exception
    {
        public AlreadyExistsCarException(Exception innerException)
            : base(message: "Car with the same id already exists.", innerException) { }

    }
}
