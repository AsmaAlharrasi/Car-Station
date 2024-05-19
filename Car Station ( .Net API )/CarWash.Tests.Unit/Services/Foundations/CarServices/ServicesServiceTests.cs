using System;
using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Storages;
using CarWash.Services.Foundations.Services;
using CarWash.Models.ServicesModels;
using Moq;
using Tynamix.ObjectFiller;
using CarWash.Brokers.Loggings;

namespace CarWash.Tests.Unit.Services.Foundations.CarServices

{
	public partial class ServicesServiceTests
	{
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
       private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IServiceService serviceService;
        public ServicesServiceTests()
		{
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.serviceService = new ServiceService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object
                );
        }
        private static Service CreateRandomService() =>
            CreateServiceFiller(dates: DateTimeOffset.UtcNow).Create();
        
        private static Service CreateRandomService(DateTimeOffset dates) =>
            CreateServiceFiller(dates).Create();
        
        private static IQueryable<Service> CreateRandomServices(DateTimeOffset dates) =>
            CreateServiceFiller(dates).Create(GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        //private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        //private static string GetRandomMessage() => new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime()=>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();
        
        
        private static Filler<Service> CreateServiceFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Service>();
            Guid createdById = Guid.NewGuid();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                //.OnProperty(customer => customer.BirthDate).Use(GetRandomDateTime())
                .OnProperty(Service => Service.CreatedDate).Use(dates)
                .OnProperty(Service => Service.UpdatedDate).Use(dates)
                .OnProperty(Service => Service.CreatedBy).Use(createdById)
                .OnProperty(Service => Service.UpdatedBy).Use(createdById)
                .OnProperty(Service => Service.ServicesRequests).IgnoreIt();
               // .OnProperty(customer =>customer.Cars).IgnoreIt();
            return filler;
        }

       
    }
}

