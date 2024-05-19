using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class EmployeeServiceException : Xeption
    {
        public EmployeeServiceException(Xeption innerException)
            : base(message: "Employee service error occurred, contact support.", innerException) { }
    }
}
