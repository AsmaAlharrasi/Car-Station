using CarWash.Models.ServicesModels;

namespace CarWash.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Service> InsertServices(Service service);
        IQueryable<Service> SelectAllServices();
        ValueTask<Service> SelectAsyncServices(Guid serviceId);
        ValueTask<Service> UpdateAsyncServices(Service service);
        ValueTask<Service> DeleteAsyncServices(Service service);

    }
}
