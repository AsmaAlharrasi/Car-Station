using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Storages;
using CarWash.Services.Foundations.Employees;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Tynamix.ObjectFiller;
using CarWash.Models.Employees;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.Data.SqlClient;
using System.Runtime.Serialization;


using System.Linq.Expressions;

using Xeptions;

using Xunit;
using CarWash.Brokers.Loggings;


namespace CarWash.Tests.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IEmployeeService employeeService;

        public EmployeeServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.employeeService = new EmployeeService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object
                ); 
        }

        //------------------------------------------------
        private static Employee CreateRandomEmployee() =>
       CreateRandomEmployeeFiller(dateTime: DateTimeOffset.UtcNow).Create();
        private static Employee CreateRandomEmployee(DateTimeOffset dates) =>
           CreateRandomEmployeeFiller(dates).Create();

        //------------------------------------------------

        public static TheoryData InvalidMinuteCases()
        {
            int randomMoreThanMinuteFromNow = GetRandomNumber();
            int randomMoreThanMinuteBeforeNow = GetNegativeRandomNumber();

            return new TheoryData<int>
            {
                randomMoreThanMinuteFromNow ,
                randomMoreThanMinuteBeforeNow
            };
        }
        //-------------------------------------------------
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

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();
        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();

        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        private static string GetRandomMessage() => new MnemonicString().GetValue();


        //-------------------------------------------------

        private static IQueryable<Employee> CreateRandomEmployees(DateTimeOffset dates) =>
            CreateRandomEmployeeFiller(dates).Create(GetRandomNumber()).AsQueryable();

        //------------------------------------------------

        private static Filler<Employee> CreateRandomEmployeeFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Employee>();
            Guid createdById = Guid.NewGuid();

            filler.Setup()
                .OnProperty(employee => employee.EmployeeStatus).Use(EmployeeStatus.Free)
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnProperty(employee => employee.BirthDate).Use(GetRandomDateTime())
                .OnProperty(employee => employee.CreatedDate).Use(dateTime)
                .OnProperty(employee => employee.UpdatedDate).Use(dateTime)
                .OnProperty(employee => employee.CreatedBy).Use(createdById)
                .OnProperty(employee => employee.UpdatedBy).Use(createdById)
                .OnProperty(employee => employee.ServicesRequests).IgnoreIt();
            
            return filler;
        }
    }
}
