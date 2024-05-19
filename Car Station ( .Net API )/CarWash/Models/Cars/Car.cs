
using CarWash.Models.Customers;
using CarWash.Models.ServicesRequests;
using System.Text.Json.Serialization;

namespace CarWash.Models.Cars
{
    public class Car : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string? Description { get; set; }

        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer? Customer { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }



        [JsonIgnore]
        public IEnumerable<ServicesRequest>? ServicesRequests { get; set; }
    }
}
