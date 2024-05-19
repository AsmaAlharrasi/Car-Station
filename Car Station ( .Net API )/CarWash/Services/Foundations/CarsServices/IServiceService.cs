
using CarWash.Models.ServicesModels;

namespace CarWash.Services.Foundations.Services
{
    public interface IServiceService
    {
        ValueTask<Service> AddServiceAsync(Service service);
        IQueryable<Service> RetrieveAllServices();
        ValueTask<Service> RetrieveServiceByIdAsync(Guid serviceId);
        ValueTask<Service> ModifyServiceAsync(Service service);
        ValueTask<Service> RemoveServiceByIdAsync(Guid serviceId);
    }

}
