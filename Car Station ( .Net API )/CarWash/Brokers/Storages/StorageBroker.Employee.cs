using CarWash.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker 
    {
        public DbSet<Employee> Employees { set; get; }

        public async ValueTask<Employee> InsertEmployeeAsync(Employee employee)
        {
            try {
                var broker = new StorageBroker(this.configuration);
                EntityEntry<Employee> employeesEntityEntry = await broker.Employees.AddAsync(entity: employee);
                await broker.SaveChangesAsync();

                return employeesEntityEntry.Entity;
            }
            catch (Exception e) {
                throw e; 
            }
            
             return new Employee();
        }

        public IQueryable<Employee> SelectAllEmployees() => this.Employees;

        public async ValueTask<Employee> SelectEmployeeByIdAsync(Guid employeeId)
        {
            var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await Employees.FindAsync(employeeId);
        }

        public async ValueTask<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var broker = new StorageBroker(this.configuration);
            EntityEntry<Employee> employeeEntityEntry = broker.Employees.Update(entity: employee);
            await broker.SaveChangesAsync();

            return employeeEntityEntry.Entity;
        }

        public async ValueTask<Employee> DeleteEmployeeAsync(Employee employee)
        {
            var broker = new StorageBroker(this.configuration);
            EntityEntry<Employee> employeeEntityEntry = broker.Employees.Remove(entity: employee);
            await broker.SaveChangesAsync();

            return employeeEntityEntry.Entity;
        }



    }
}
