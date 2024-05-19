using CarWash.Models.ServicesModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CarWash.Models.ServicesRequests;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<ServicesRequest> ServicesRequests { get; set; }


        public async ValueTask<ServicesRequest> InsertServicesRequest(ServicesRequest servicesRequest)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<ServicesRequest> ServiceRequestEntyiyEntry = await broker.ServicesRequests.AddAsync(entity: servicesRequest);
            await broker.SaveChangesAsync();
            return ServiceRequestEntyiyEntry.Entity;
        }

        public IQueryable<ServicesRequest> SelectAllServicesRequest() => ServicesRequests;


        public async ValueTask<ServicesRequest> SelectAsyncServicesRequest(Guid serviceRequestId)
        {
            var broker = new StorageBroker(configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await ServicesRequests.FindAsync(serviceRequestId);
        }

        public async ValueTask<ServicesRequest> UpdateAsyncServicesRequest(ServicesRequest serviceRequest)
        {
            var broker = new StorageBroker(configuration);

            EntityEntry<ServicesRequest> ServiceRequestEntyiyEntry = broker.ServicesRequests.Update(entity: serviceRequest);
            await broker.SaveChangesAsync();
            return ServiceRequestEntyiyEntry.Entity;

        }

        public async ValueTask<ServicesRequest> DeleteAsyncServicesRequest(ServicesRequest serviceRequest)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<ServicesRequest> ServiceRequestEntyiyEntry = broker.ServicesRequests.Remove(entity: serviceRequest);
            await broker.SaveChangesAsync();
            return ServiceRequestEntyiyEntry.Entity;

        }

    }
}
