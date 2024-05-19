using CarWash.Models.ServicesModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
       

        public DbSet<Service> Services { get; set; }


        public async ValueTask<Service> InsertServices(Service service)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Service> ServicesEntyiyEntry = await broker.Services.AddAsync(entity: service);
            await broker.SaveChangesAsync();
            return ServicesEntyiyEntry.Entity;
        }

        public IQueryable<Service> SelectAllServices() => this.Services;

        public async ValueTask<Service> SelectAsyncServices(Guid serviceId)
        {
            var broker = new StorageBroker(configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await Services.FindAsync(serviceId);
        }

        public async ValueTask<Service> UpdateAsyncServices(Service service)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Service> ServicesEntyiyEntry = broker.Services.Update(entity: service);
            await broker.SaveChangesAsync();
            return ServicesEntyiyEntry.Entity;

        }

        public async ValueTask<Service> DeleteAsyncServices(Service service)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Service> ServicesEntyiyEntry = broker.Services.Remove(entity: service);
            await broker.SaveChangesAsync();
            return ServicesEntyiyEntry.Entity;

        }


    }
}
