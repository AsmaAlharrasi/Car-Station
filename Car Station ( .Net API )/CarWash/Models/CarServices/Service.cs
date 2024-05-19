
using CarWash.Models.ServicesRequests;
using System.Text.Json.Serialization;


namespace CarWash.Models.ServicesModels
{
    public class Service : IAuditable
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string? Type { get; set; }

        public Guid AdminId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        [JsonIgnore]
        public virtual Admin? Admin { get; set; }

        [JsonIgnore]
        public IEnumerable<ServicesRequest>? ServicesRequests { get; set; }


    }
}
