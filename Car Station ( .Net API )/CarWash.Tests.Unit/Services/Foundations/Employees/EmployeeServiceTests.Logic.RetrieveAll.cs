using CarWash.Models.Employees;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Tests.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllEmployees()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            IQueryable<Employee> randomEmployees = CreateRandomEmployees(randomDateTime);
            IQueryable<Employee> storageEmployees = randomEmployees;
            IQueryable<Employee> expectedEmployees = storageEmployees;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEmployees())
                    .Returns(storageEmployees);

            // when
            IQueryable<Employee> actualEmployees =
                this.employeeService.RetrieveAllEmployees();

            // then
            actualEmployees.Should().BeEquivalentTo(expectedEmployees);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEmployees(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
