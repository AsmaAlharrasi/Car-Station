

using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class AlreadyExistsEmployeeException : Xeption
    {
        public AlreadyExistsEmployeeException(Exception innerException)
           : base(message: "Employee with the same id already exists.", innerException) { }
    }
}
