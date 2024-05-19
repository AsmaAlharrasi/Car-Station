using CarWash.Models.Customers;
using CarWash.Models.Customers.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRegisterIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Customer someCustomer = CreateRandomCustomer();
            SqlException sqlException = GetSqlException();

            var failedCustomerStorageException =
                new FailedCustomerStorageException(sqlException);

            var expectedCustomerDependencyException =
                new CustomerDependencyException(failedCustomerStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(sqlException);

            // when
            ValueTask<Customer> registerCustomerTask =
                this.customerService.AddCustomerAsync(someCustomer);

            // then
           await Assert.ThrowsAsync<CustomerDependencyException>(() =>
                registerCustomerTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

           this.loggingBrokerMock.Verify(broker =>
                    broker.LogCritical(It.Is(SameExceptionAs(
                        expectedCustomerDependencyException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCustomer(someCustomer),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            
        }
        
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRegisterWhenCustomerAlreadyExistsAndLogItAsync()
        {
            // given
            Customer someCustomer = CreateRandomCustomer();
            string someMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsCustomerException =
                new AlreadyExistsCustomerException(duplicateKeyException);

            var expectedCustomerDependencyValidationException =
                new CustomerDependencyValidationException(alreadyExistsCustomerException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(duplicateKeyException);

            // when
            ValueTask<Customer> registerCustomerTask =
                this.customerService.AddCustomerAsync(someCustomer);

            // then
            await Assert.ThrowsAsync<CustomerDependencyValidationException>(() =>
                registerCustomerTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(It.Is(SameValidationExceptionAs(
                        expectedCustomerDependencyValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCustomer(someCustomer),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Customer someCustomer = CreateRandomCustomer();
            var databaseUpdateException = new DbUpdateException();

            var failedCustomerStorageException =
                new FailedCustomerStorageException(databaseUpdateException);

            var expectedCustomerDependencyException =
                new CustomerDependencyException(failedCustomerStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(databaseUpdateException);

            // when
            ValueTask<Customer> registerCustomerTask =
                this.customerService.AddCustomerAsync(someCustomer);

            // then
            await Assert.ThrowsAsync<CustomerDependencyException>(() =>
                registerCustomerTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(It.Is(SameExceptionAs(
                        expectedCustomerDependencyException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCustomer(It.IsAny<Customer>()),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterIfExceptionOccursAndLogItAsync()
        {
            // given
            Customer someCustomer = CreateRandomCustomer();
            var serviceException = new Exception();

            var failedCustomerServiceException =
                new FailedCustomerServiceException(serviceException);

            var expectedCustomerServiceException =
                new CustomerServiceException(failedCustomerServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(serviceException);

            // when
            ValueTask<Customer> registerCustomerTask =
                this.customerService.AddCustomerAsync(someCustomer);

            // then
             await Assert.ThrowsAsync<CustomerServiceException>(() =>
                registerCustomerTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(It.Is(SameExceptionAs(
                        expectedCustomerServiceException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCustomer(It.IsAny<Customer>()),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}