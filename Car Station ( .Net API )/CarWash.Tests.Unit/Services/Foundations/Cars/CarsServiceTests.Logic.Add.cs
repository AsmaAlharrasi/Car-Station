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
        public async Task ShouldAddCarAsync()
        {
            //given -- broker side
            DateTimeOffset dateTime = DateTimeOffset.UtcNow;
            Car randomCar = CreateRandomCar(dateTime);
            randomCar.UpdatedBy = randomCar.CreatedBy;
            randomCar.UpdatedDate = randomCar.CreatedDate;
            Car inputCar = randomCar;
            Car storageCar = randomCar;
            Car expectedCar = randomCar;

            //this.dateTimeBrokerMock.Setup(broker =>
            //broker.GetCurrentDateTime())
            //    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertCar(inputCar))
                .ReturnsAsync(storageCar);

            //when -- service side
            Car actualCar = 
                await this.carService.AddCarAsync(inputCar);

            //then
            actualCar.Should().BeEquivalentTo(expectedCar);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertCar(inputCar),
            Times.Once);

            //this.dateTimeBrokerMock.Verify(broker =>
            //broker.GetCurrentDateTime(),
            //Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();

        }
    }
}
