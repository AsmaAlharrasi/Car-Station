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
        public async Task ShouldRegisterEmployeeAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            DateTimeOffset dateTime = randomDateTime;
            Employee ramdonEmployee = CreateRandomEmployee(randomDateTime);
            ramdonEmployee.UpdatedBy = ramdonEmployee.CreatedBy;
            Employee inputEmployee = ramdonEmployee;
            Employee storageEmployee = ramdonEmployee;
            Employee expectedEmployee = storageEmployee;

            //this.dateTimeBrokerMock.Setup(broker =>
            //    broker.GetCurrentDateTime())
            //        .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEmployeeAsync(inputEmployee))
                    .ReturnsAsync(storageEmployee);

            // when
            Employee actualEmployee =
                await this.employeeService.AddEmployeeAsync(inputEmployee);

            // then
            actualEmployee.Should().BeEquivalentTo(expectedEmployee);

            //this.dateTimeBrokerMock.Verify(broker =>
            //    broker.GetCurrentDateTime(),
            //        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEmployeeAsync(inputEmployee),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();


        }
    }
}
