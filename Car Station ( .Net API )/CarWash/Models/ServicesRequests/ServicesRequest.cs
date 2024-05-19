
using CarWash.Models.Cars;
using CarWash.Models.Customers;
using CarWash.Models.Employees;
using CarWash.Models.ServicesModels;
using System.Text.Json.Serialization;




namespace CarWash.Models.ServicesRequests
{
    public class ServicesRequest : IAuditable
    {
        public Guid Id { get; set; }
        public DateTimeOffset Booking { get; set; } = DateTimeOffset.Now.Date;
        public string? Comment { get; set; }
        public ServicesRequestStatus Status { get; set; } = ServicesRequestStatus.Pending;
        public ServiceType? ServicesType { get; set; }


        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

        public Guid CustomerId { get; set;}
        public Guid? EmployeeId { get; set;}
        public Guid ServiceId { get; set; }
        public Guid AdminId { get; set; }
        public Guid? CarId { get; set; }




        [JsonIgnore]
        public virtual Car? Car { get; set; }
        [JsonIgnore]
        public virtual Customer? Customer { get; set; }

        [JsonIgnore]
        public virtual Employee? Employee { get; set; }
        
        [JsonIgnore]
        public virtual Service? Service { get; set; }

        [JsonIgnore]
        public virtual Admin? Admin { get; set; }

    }
}
