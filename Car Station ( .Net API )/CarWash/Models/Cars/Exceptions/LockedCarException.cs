
namespace CarWash.Models.Cars.Exceptions
{
    public class LockedCarException : Exception
    {
        public LockedCarException(Exception innerException)
            : base(message: "Locked car record exception, please try again later.", innerException) { }
    }
}
