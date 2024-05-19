using CarWash.Models.Cars;
using FluentAssertions;
using FluentAssertions.Equivalency.Tracing;
using Force.DeepCloner;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Tests.Unit.Services.Foundations.Cars
{
    public partial class CarsServiceTests
    {
        [Fact]

        public async Task ShouldModifyCarAsync()
        {
            //given
            int randomNumber = GetRandomNumber();
            int randomDays = randomNumber;
            DateTimeOffset randomDate = GetRandomDateTime();
            DateTimeOffset randomInputDate = GetRandomDateTime();
            Car randomCar = CreateRandomCar(randomInputDate);
            Car inputCar = randomCar;
            Car beforeUpdateStorageCar = randomCar.DeepClone();
            Car afterUpdateStorageCar = inputCar;
            Car expectedCar = afterUpdateStorageCar;
            inputCar.UpdatedDate = randomDate;
            Guid carId = inputCar.Id;

            //this.dateTimeBrokerMock.Setup(broker =>
            //broker.GetCurrentDateTime())
            //    .Returns(randomDate);

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncCar(carId))
                .ReturnsAsync(beforeUpdateStorageCar);

            this.storageBrokerMock.Setup(broker =>
            broker.UpdateAsyncCar(inputCar))
                .ReturnsAsync(afterUpdateStorageCar);


            //when
            Car actualCar =
               await this.carService.ModifyCarAsync(inputCar);

            //then
            actualCar.Should().BeEquivalentTo(expectedCar);

            //this.dateTimeBrokerMock.Verify(broker =>
            //broker.GetCurrentDateTime(),
            //Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.SelectAsyncCar(carId),
            Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.UpdateAsyncCar(inputCar),
            Times.Once);



            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();

        } 
    }
}
