using CarWash.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetEmployeeReference(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.Admin)
                .WithMany(admin => admin.Employee)
                .HasForeignKey(employee => employee.AdminId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
