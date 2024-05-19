using CarWash.Models.ServicesRequests;
using System.Text.Json.Serialization;

namespace CarWash.Models.Employees
{
    public class Employee : IAuditable
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; } = DateTimeOffset.Now.Date;
        public double Salary { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now.Date;
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.Now.Date;

        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid AdminId { get; set; }


        [JsonIgnore]
        public virtual Admin? Admin { get; set; }

        [JsonIgnore]

        public IEnumerable<ServicesRequest>? ServicesRequests { get; set; }


    }
}
