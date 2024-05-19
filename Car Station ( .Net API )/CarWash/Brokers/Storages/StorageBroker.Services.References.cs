using CarWash.Models.ServicesModels;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Brokers.Storages
{
   public partial class StorageBroker
    {
        private static void SetServiceReference(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasOne(service => service.Admin)
                .WithMany(admin => admin.Service)
                .HasForeignKey(service => service.AdminId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
