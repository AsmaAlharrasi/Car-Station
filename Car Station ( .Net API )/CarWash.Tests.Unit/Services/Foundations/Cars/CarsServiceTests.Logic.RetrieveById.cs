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

        public async Task ShouldRetrieveCarByIdAsync()
        {
            //given
            Guid randomCarId = Guid.NewGuid();
            Guid inputCarId = randomCarId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Car randomCar = CreateRandomCar(randomDateTime);
            Car storageCar = randomCar;
            Car expectedCar = storageCar;

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncCar(inputCarId))
                .ReturnsAsync(storageCar);

            //when
            Car actualCar = 
                await this.carService.RetrieveCarByIdAsync(inputCarId);

            //then
            actualCar.Should().BeEquivalentTo(expectedCar);

            this.dateTimeBrokerMock.Verify(broker =>
            broker.GetCurrentDateTime(),
            Times.Never);

            this.storageBrokerMock.Verify(broker =>
            broker.SelectAsyncCar(inputCarId),
            Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();



        }
    }
}
