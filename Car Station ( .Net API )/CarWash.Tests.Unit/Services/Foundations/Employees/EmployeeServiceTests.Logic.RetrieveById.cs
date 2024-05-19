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
        public async Task ShouldRetrieveEmployeeByIdAsync()
        {

            // given
            Guid randomEmployeeId = Guid.NewGuid();
            Guid inputEmployeeId = randomEmployeeId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Employee randomEmployee = CreateRandomEmployee(randomDateTime);
            Employee storageEmployee = randomEmployee;
            Employee expectedEmployee = storageEmployee;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEmployeeByIdAsync(inputEmployeeId))
                    .ReturnsAsync(storageEmployee);

            // when
            Employee actualEmployee =
                await this.employeeService.RetrieveEmployeeByIdAsync(inputEmployeeId);

            // then
            actualEmployee.Should().BeEquivalentTo(expectedEmployee);

            //this.dateTimeBrokerMock.Verify(broker =>
            //    broker.GetCurrentDateTime(),
            //        Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEmployeeByIdAsync(inputEmployeeId),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
