using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class EmployeeDependencyValidationException : Xeption
    {
        public EmployeeDependencyValidationException(Xeption innerException)
          : base(message: "Employee dependency validation occurred, fix the errors and try again.", innerException)
        { }
    }
}
