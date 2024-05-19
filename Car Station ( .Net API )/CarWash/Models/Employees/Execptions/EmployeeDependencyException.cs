using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class EmployeeDependencyException : Exception
    {
        public EmployeeDependencyException(Exception innerException)
          : base(message: "Employee dependency error occurred, contact support.", innerException) { }
    }
}
