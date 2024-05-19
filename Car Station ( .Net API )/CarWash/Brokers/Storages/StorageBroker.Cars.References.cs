using CarWash.Models.Cars;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetCarReference(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(car => car.Customer)
                .WithMany(customer => customer.Cars)
                .HasForeignKey(car => car.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
