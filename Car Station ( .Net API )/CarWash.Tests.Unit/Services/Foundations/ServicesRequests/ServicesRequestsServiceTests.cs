using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.ServicesRequests;
using CarWash.Services.Foundations.ServiceRequests;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace CarWash.Tests.Unit.Services.Foundations.ServicesRequests
{
    public partial class ServicesRequestsServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IServiceRequestService servicesRequestService;

        public ServicesRequestsServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.servicesRequestService = new ServiceRequestService (
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }


        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static ServicesRequest CreateRandomServicesRequest(DateTimeOffset dateTime) =>
            CreateRandomServicesRequestFiller(dateTime).Create();

        private static ServicesRequest CreateRandomServicesRequest() =>
            CreateRandomServicesRequestFiller(dates: GetRandomDateTime()).Create();
        private static IQueryable<ServicesRequest> CreateRandomServicesRequests(DateTimeOffset dates) =>
           CreateRandomServicesRequestFiller(dates).Create(GetRandomNumber()).AsQueryable();
        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
      
        public static TheoryData InvalidMinuteCases()
        {
            int randomMoreThanMinuteFromNow = GetRandomNumber();
            int randomMoreThanMinuteBeforeNow = GetNegativeRandomNumber();

            return new TheoryData<int>
            {
                randomMoreThanMinuteFromNow,
                randomMoreThanMinuteBeforeNow
            };
        }

        private static Filler<ServicesRequest> CreateRandomServicesRequestFiller(DateTimeOffset dates)
        {
            var filler = new Filler<ServicesRequest>();
            Guid createdById = Guid.NewGuid();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(serviceRequest => serviceRequest.Status).Use(ServicesRequestStatus.Pending)
                .OnProperty(serviceRequest => serviceRequest.CreatedDate).Use(dates)
                .OnProperty(serviceRequest => serviceRequest.UpdatedDate).Use(dates)
                .OnProperty(serviceRequest => serviceRequest.CreatedBy).Use(createdById)
                .OnProperty(serviceRequest => serviceRequest.UpdatedBy).Use(createdById)
                .OnProperty(serviceRequest => serviceRequest.Customer).IgnoreIt()
                .OnProperty(serviceRequest => serviceRequest.Employee).IgnoreIt()
                .OnProperty(serviceRequest => serviceRequest.Admin).IgnoreIt()
                .OnProperty(serviceRequest => serviceRequest.Service).IgnoreIt();


            return filler;
        }

    }
}
