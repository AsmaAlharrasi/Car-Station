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
        public async Task ShouldRemoveEmployeeByIdAsync()
        {
            // given
            Employee randomEmployee = CreateRandomEmployee();
            Guid employeeId = randomEmployee.Id;
            Employee storageEmployee = randomEmployee;
            Employee expectedEmployee = storageEmployee;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEmployeeByIdAsync(employeeId))
                    .ReturnsAsync(storageEmployee);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteEmployeeAsync(storageEmployee))
                    .ReturnsAsync(expectedEmployee);

            // when
            Employee actualEmployee =
                await this.employeeService.RemoveEmployeeByIdAsync(employeeId);

            // then
            actualEmployee.Should().BeEquivalentTo(expectedEmployee);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEmployeeByIdAsync(employeeId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteEmployeeAsync(storageEmployee),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
