using System.Linq.Expressions;
using System.Runtime.Serialization;
using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.Customers;
using CarWash.Services.Foundations.Customers;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICustomerService customerService;

        public CustomerServicesTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.customerService = new CustomerService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object
            );
        }
        private static Customer CreateRandomCustomer() =>
            CreateCustomerFiller(dates: DateTimeOffset.UtcNow).Create();
        
        private static Customer CreateRandomCustomer(DateTimeOffset dates) =>
            CreateCustomerFiller(dates).Create();

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static Expression<Func<Exception, bool>> SameValidationExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static IQueryable<Customer> CreateRandomCustomers(DateTimeOffset dates) =>
            CreateCustomerFiller(dates).Create(GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        //private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        private static string GetRandomMessage() => new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime()=>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();
        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));
        
        private static Filler<Customer> CreateCustomerFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Customer>();
            Guid createdById = Guid.NewGuid();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(customer => customer.BirthDate).Use(GetRandomDateTime())
                .OnProperty(customer => customer.CreatedDate).Use(dates)
                .OnProperty(customer => customer.UpdatedDate).Use(dates)
                .OnProperty(customer => customer.CreatedBy).Use(createdById)
                .OnProperty(customer => customer.UpdatedBy).Use(createdById)
                .OnProperty(customer =>customer.ServicesRequests).IgnoreIt()
                .OnProperty(customer =>customer.Cars).IgnoreIt();
            return filler;
        }

        
    }
}