using CarWash.Models.Cars.Exceptions;
using CarWash.Models.Cars;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace CarWash.Tests.Unit.Services.Foundations.Cars
{
    public partial class CarsServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRegisterIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Car someCar = CreateRandomCar();
            SqlException sqlException = GetSqlException();

            var failedCarStorageException =
                new FailedCarStorageException(sqlException);

            var expectedCarDependencyException =
                new CarDependencyException(failedCarStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(sqlException);

            // when
            ValueTask<Car> registerCarTask =
                this.carService.AddCarAsync(someCar);

            // then
            await Assert.ThrowsAsync<CarDependencyException>(() =>
                 registerCarTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                     broker.LogCritical(It.Is(SameExceptionAs(
                         expectedCarDependencyException))),
                 Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCar(someCar),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRegisterWhenCarAlreadyExistsAndLogItAsync()
        {
            // given
            Car someCar = CreateRandomCar();
            string someMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsCarException =
                new AlreadyExistsCarException(duplicateKeyException);

            var expectedCarDependencyValidationException =
                new CarDependencyValidationException(alreadyExistsCarException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(duplicateKeyException);

            // when
            ValueTask<Car> registerCustomerTask =
                this.carService.AddCarAsync(someCar);

            // then
            await Assert.ThrowsAsync<CarDependencyValidationException>(() =>
                registerCustomerTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(It.Is(SameValidationExceptionAs(
                        expectedCarDependencyValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCar(someCar),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRegisterIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Car someCar = CreateRandomCar();
            var databaseUpdateException = new DbUpdateException();

            var failedCarStorageException =
                new FailedCarStorageException(databaseUpdateException);

            var expectedCarDependencyException =
                new CarDependencyException(failedCarStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(databaseUpdateException);

            // when
            ValueTask<Car> registerCarTask =
                this.carService.AddCarAsync(someCar);

            // then
            await Assert.ThrowsAsync<CarDependencyException>(() =>
                registerCarTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(It.Is(SameExceptionAs(
                        expectedCarDependencyException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCar(It.IsAny<Car>()),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRegisterIfExceptionOccursAndLogItAsync()
        {
            // given
            Car someCar = CreateRandomCar();
            var serviceException = new Exception();

            var failedCarServiceException =
                new FailedCarServiceException(serviceException);

            var expectedCarServiceException =
                new CarServiceException(failedCarServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                    broker.GetCurrentDateTime())
                .Throws(serviceException);

            // when
            ValueTask<Car> registerCustomerTask =
                this.carService.AddCarAsync(someCar);

            // then
            await Assert.ThrowsAsync<CarServiceException>(() =>
               registerCustomerTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(It.Is(SameExceptionAs(
                        expectedCarServiceException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCar(It.IsAny<Car>()),
                Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
