using CarWash.Models.Employees;
using FluentAssertions;
using FluentAssertions.Equivalency.Tracing;
using Force.DeepCloner;
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
        public async Task ShouldModifyEmployeeAsync()
        {
            // given
            int randomNumber = GetRandomNumber();
            int randomDays = randomNumber;
            DateTimeOffset randomDate = GetRandomDateTime();
            DateTimeOffset randomInputDate = GetRandomDateTime();
            Employee randomEmployee = CreateRandomEmployee(randomInputDate);
            Employee inputEmployee = randomEmployee;
            Employee afterUpdateStorageEmployee = inputEmployee;
            Employee expectedEmployee = afterUpdateStorageEmployee;
            Employee beforeUpdateStorageEmployee = randomEmployee.DeepClone();
            inputEmployee.UpdatedDate = randomDate;
            Guid employeeId = inputEmployee.Id;

            //this.dateTimeBrokerMock.Setup(broker =>
            //  broker.GetCurrentDateTime())
            //      .Returns(randomDate);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEmployeeByIdAsync(employeeId))
                    .ReturnsAsync(beforeUpdateStorageEmployee);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateEmployeeAsync(inputEmployee))
                    .ReturnsAsync(afterUpdateStorageEmployee);

            // when
            Employee actualEmployee =
                await this.employeeService.ModifyEmployeeAsync(inputEmployee);

            // then
            actualEmployee.Should().BeEquivalentTo(expectedEmployee);

            //this.dateTimeBrokerMock.Verify(broker =>
            //    broker.GetCurrentDateTime(),
            //        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEmployeeByIdAsync(employeeId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateEmployeeAsync(inputEmployee),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
