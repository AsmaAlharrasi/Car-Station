using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class NotFoundEmployeeException : Xeption
    {
        public NotFoundEmployeeException(Guid employeeId)
           : base(message: $"Couldn't find employee with id: {employeeId}.") { }
    }
}
