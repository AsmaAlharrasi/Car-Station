using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.ServicesModels;

namespace CarWash.Services.Foundations.Services
{
    public partial class ServiceService : IServiceService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ServiceService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Service> AddServiceAsync(Service service) =>
            TryCatch(async () =>
            {
                return await storageBroker.InsertServices(service);
            });

        public IQueryable<Service> RetrieveAllServices() =>
          TryCatch(() => storageBroker.SelectAllServices());


        public ValueTask<Service> RetrieveServiceByIdAsync(Guid serviceId) =>
            TryCatch(async () =>
            {
                Service maybeService = await this.storageBroker.SelectAsyncServices(serviceId);
                return maybeService;
            });

        public ValueTask<Service> ModifyServiceAsync(Service service) =>
            TryCatch(async () =>
            {
                Service maybeService = await this.storageBroker.SelectAsyncServices(service.Id);
                return await storageBroker.UpdateAsyncServices(service);

            });

        public ValueTask<Service> RemoveServiceByIdAsync(Guid serviceId) =>
            TryCatch(async () =>
            {
                Service maybeService = await this.storageBroker.SelectAsyncServices(serviceId);
                return await this.storageBroker.DeleteAsyncServices(maybeService);
            });


    }









}
