using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class EmployeeValidationException : Xeption
    {
        public EmployeeValidationException(Xeption innerException)
           : base(message: "Invalid input, contact support.", innerException) { }
    }
}
