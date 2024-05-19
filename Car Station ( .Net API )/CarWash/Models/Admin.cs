using CarWash.Models.Employees;
using CarWash.Models.ServicesModels;
using CarWash.Models.ServicesRequests;
using System.Text.Json.Serialization;

namespace CarWash.Models
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; }

        [JsonIgnore]
        public IEnumerable<ServicesRequest>? ServicesRequests { get; set; }

        [JsonIgnore]
        public IEnumerable<Service>? Service { get; set; }

        [JsonIgnore]
        public IEnumerable<Employee>? Employee { get; set; }



    }
}
