using System;
using CarWash.Models;
using CarWash.Models.ServicesRequests;

namespace CarWash.Services.Foundations.ServiceRequests
{
	public interface IServiceRequestService
	{
        ValueTask<ServicesRequest> AddServicesRequestAsync(ServicesRequest serviceRequest);
        IQueryable<ServicesRequest> RetriveAllServicesRequest();
        ValueTask<ServicesRequest> RetriveServicesRequestByIdAsync(Guid serviceRequestId);
        ValueTask<ServicesRequest> ModifyServicesRequestAsync(ServicesRequest serviceRequest);
        ValueTask<ServicesRequest> RemoveServicesRequestAsync(Guid serviceRequestId);

    }
}

