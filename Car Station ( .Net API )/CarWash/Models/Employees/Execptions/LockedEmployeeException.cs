using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class LockedEmployeeException : Xeption
    {
        public LockedEmployeeException(Exception innerException)
          : base(message: "Locked employee error occurred, please try again later.", innerException) { }
    }
}
