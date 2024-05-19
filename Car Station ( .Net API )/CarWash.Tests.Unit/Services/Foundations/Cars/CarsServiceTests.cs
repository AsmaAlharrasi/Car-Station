using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.Cars;
using CarWash.Services.Foundations.Cars;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;

namespace CarWash.Tests.Unit.Services.Foundations.Cars
{
    public partial class CarsServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICarService carService;

        public CarsServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.carService = new CarService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }


        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        private static string GetRandomMessage() => new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Car CreateRandomCar(DateTimeOffset dateTime) =>
            CreateRandomCarFiller(dateTime).Create();

        private static Car CreateRandomCar() =>
            CreateRandomCarFiller(dates: GetRandomDateTime()).Create();
        private static IQueryable<Car> CreateRandomCars(DateTimeOffset dates) =>
           CreateRandomCarFiller(dates).Create(GetRandomNumber()).AsQueryable();
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
        private static Expression<Func<Exception, bool>> SameValidationExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static SqlException GetSqlException() =>
           (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static Filler<Car> CreateRandomCarFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Car>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(car => car.Customer).IgnoreIt();


                return filler;
        }

    }
}
