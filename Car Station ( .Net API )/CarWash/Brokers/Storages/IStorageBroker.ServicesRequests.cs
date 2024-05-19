using CarWash.Models.ServicesModels;
using CarWash.Models.ServicesRequests;

namespace CarWash.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<ServicesRequest> InsertServicesRequest(ServicesRequest serviceRequest);
        IQueryable<ServicesRequest> SelectAllServicesRequest();
        ValueTask<ServicesRequest> SelectAsyncServicesRequest(Guid serviceRequestId);
        ValueTask<ServicesRequest> UpdateAsyncServicesRequest(ServicesRequest serviceRequest);
        ValueTask<ServicesRequest> DeleteAsyncServicesRequest(ServicesRequest serviceRequest);
    }
}
