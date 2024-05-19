using CarWash.Models.ServicesRequests;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetServicesRequestReference(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServicesRequest>()
                .HasOne(serviceRequest => serviceRequest.Service)
                .WithMany(service => service.ServicesRequests)
                .HasForeignKey(serviceRequest => serviceRequest.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServicesRequest>()
                .HasOne(serviceRequest => serviceRequest.Employee)
                .WithMany(service => service.ServicesRequests)
                .HasForeignKey(serviceRequest => serviceRequest.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServicesRequest>()
               .HasOne(serviceRequest => serviceRequest.Admin)
               .WithMany(service => service.ServicesRequests)
               .HasForeignKey(serviceRequest => serviceRequest.AdminId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServicesRequest>()
               .HasOne(serviceRequest => serviceRequest.Customer)
               .WithMany(service => service.ServicesRequests)
               .HasForeignKey(serviceRequest => serviceRequest.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);
        }



    }
}
