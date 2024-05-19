using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class FailedEmployeeServiceException : Xeption
    {
        public FailedEmployeeServiceException(Exception innerException)
           : base(message: "Failed employee service error occurred. ", innerException)
        { }
    }
}
