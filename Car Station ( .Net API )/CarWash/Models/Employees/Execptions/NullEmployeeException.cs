using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class NullEmployeeException : Xeption
    {
        public NullEmployeeException() : base(message: "The employee is null.") { }
    }
}

