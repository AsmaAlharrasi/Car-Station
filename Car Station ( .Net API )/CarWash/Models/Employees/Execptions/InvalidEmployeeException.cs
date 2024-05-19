using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class InvalidEmployeeException : Xeption
    {
        public InvalidEmployeeException()
           : base(message: "Invalid employee. Please fix the errors and try again.") { }
    }
}
