using CarWash.Models.Employees;
using CarWash.Models.Employees.Execptions;
using EFxceptions.Models.Exceptions;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnRegisterIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Employee someEmployee = CreateRandomEmployee();
            SqlException sqlException = GetSqlException();

            var failedEmployeeStorageException =
                new FailedEmployeeStorageException(sqlException);

            var expectedEmployeeDependencyException =
                new EmployeeDependencyException(failedEmployeeStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<Employee> addEmployeeTask =
                this.employeeService.AddEmployeeAsync(someEmployee);

            // then
            await Assert.ThrowsAsync<EmployeeDependencyException>(() =>
                addEmployeeTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedEmployeeDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEmployeeAsync(It.IsAny<Employee>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfEmployeeAlreadyExsitsAndLogItAsync()
        {
            // given
            var someEmployee = CreateRandomEmployee();
            var alreadyExistsEmployee = someEmployee;
            string randomMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(randomMessage);

            var alreadyExistsEmployeeException =
                new AlreadyExistsEmployeeException(duplicateKeyException);

            var expectedEmployeeDependencyValidationException =
                new EmployeeDependencyValidationException(alreadyExistsEmployeeException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when 
            ValueTask<Employee> addEmployeeTask =
                this.employeeService.AddEmployeeAsync(alreadyExistsEmployee);

            // then
            await Assert.ThrowsAsync<EmployeeDependencyValidationException>(() =>
                addEmployeeTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker => broker.LogError(It.Is(SameExceptionAs(
                    expectedEmployeeDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEmployeeAsync(It.IsAny<Employee>()),
                    Times.Never());

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Employee someEmployee = CreateRandomEmployee();
            var databaseUpdateException = new DbUpdateException();

            var failedEmployeeStorageException =
                new FailedEmployeeStorageException(databaseUpdateException);

            var expectedEmployeeDependencyException =
                new EmployeeDependencyException(failedEmployeeStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<Employee> addEmployeeTask =
                this.employeeService.AddEmployeeAsync(someEmployee);

            // then
            await Assert.ThrowsAsync<EmployeeDependencyException>(() =>
                addEmployeeTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEmployeeDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEmployeeAsync(It.IsAny<Employee>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Employee someEmployee = CreateRandomEmployee();
            var serviceException = new Exception();

            var failedEmployeeServiceException =
                new FailedEmployeeServiceException(serviceException);

            var expectedEmployeeServiceException =
                new EmployeeServiceException(failedEmployeeServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(serviceException);

            // when
            ValueTask<Employee> addTeacherTask =
                 this.employeeService.AddEmployeeAsync(someEmployee);

            // then
            await Assert.ThrowsAsync<EmployeeServiceException>(() =>
                addTeacherTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEmployeeServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEmployeeAsync(It.IsAny<Employee>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
