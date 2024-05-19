using CarWash.Models.Cars;
using FluentAssertions;
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

        public async Task ShouldRemoveCarByIdAsync()
        {
            //given -- broker side
            Car randomCar = CreateRandomCar();
            Guid carId = randomCar.Id;
            Car storageCar = randomCar;
            Car expectedCar = storageCar;

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncCar(carId))
                .ReturnsAsync(storageCar);

            this.storageBrokerMock.Setup(broker =>
            broker.DeleteAsyncCar(storageCar))
                .ReturnsAsync(storageCar);

            //when -- service side
            Car actualCar = 
                await this.carService.RemoveCarByIdAsync(carId);

            //then
            actualCar.Should().BeEquivalentTo(expectedCar);

            this.storageBrokerMock.Verify(broker =>
               broker.SelectAsyncCar(carId),
                   Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteAsyncCar(storageCar),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
