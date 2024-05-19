using CarWash.Models.Cars;
using CarWash.Models.Employees;
using CarWash.Models.ServicesRequests;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace CarWash.Models.Customers
{
    public class Customer : IAuditable
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; }


        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        // ---------------------------------------
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        // ---------------------------------------
        public Guid AdminId { get; set; }

        [JsonIgnore]
        public virtual Admin? Admin { get; set; }

        [JsonIgnore]
        public IEnumerable<ServicesRequest>? ServicesRequests { get; set; }

        [JsonIgnore]
        public IEnumerable<Car>? Cars { get; set; }

    }
}
