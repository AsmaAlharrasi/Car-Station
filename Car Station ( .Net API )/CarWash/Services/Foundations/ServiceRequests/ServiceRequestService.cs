using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.ServicesRequests;

namespace CarWash.Services.Foundations.ServiceRequests
{
	public partial class ServiceRequestService : IServiceRequestService
	{
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ServiceRequestService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker)
		{
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        //add
        public ValueTask<ServicesRequest> AddServicesRequestAsync(ServicesRequest servicesRequest) =>
            TryCatch(async () =>
            {
                return await storageBroker.InsertServicesRequest(servicesRequest);
            });

        //update 
        public ValueTask<ServicesRequest> ModifyServicesRequestAsync(ServicesRequest servicesRequest) =>
            TryCatch(async () =>
            {
                ServicesRequest maybeServicesRequest = await this.storageBroker.SelectAsyncServicesRequest(servicesRequest.Id);
                return await storageBroker.UpdateAsyncServicesRequest(servicesRequest);

            });

        //delete
        public ValueTask<ServicesRequest> RemoveServicesRequestAsync(Guid serviceRequestId) =>
            TryCatch(async () =>
            {
                ServicesRequest maybeServicesRequest = await this.storageBroker.SelectAsyncServicesRequest(serviceRequestId);
                return await this.storageBroker.DeleteAsyncServicesRequest(maybeServicesRequest);
            });

        //select all  
        public IQueryable<ServicesRequest> RetriveAllServicesRequest() =>
             TryCatch(() => this.storageBroker.SelectAllServicesRequest());

        //select by Id
        public ValueTask<ServicesRequest> RetriveServicesRequestByIdAsync(Guid serviceRequestId) =>
            TryCatch(async () =>
            {
                ServicesRequest maybeServicesRequest = await this.storageBroker.SelectAsyncServicesRequest(serviceRequestId);
                return maybeServicesRequest;
            });
    }
}

