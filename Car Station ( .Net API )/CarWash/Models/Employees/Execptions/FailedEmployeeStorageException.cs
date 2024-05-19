using Xeptions;

namespace CarWash.Models.Employees.Execptions
{
    public class FailedEmployeeStorageException : Xeption
    {
        public FailedEmployeeStorageException(Exception innerException)
           : base(message: "Failed employee storage error occurred, contact support.", innerException)
        { }
    }
}
