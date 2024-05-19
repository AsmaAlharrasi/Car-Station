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

        public void ShouldRetrieveAllCars() 
        {

            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            IQueryable<Car> randomCars = CreateRandomCars(randomDateTime);
            IQueryable<Car> storageCars = randomCars;
            IQueryable<Car> expectedCars = storageCars;

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAllCar())
                .Returns(storageCars);

            //when
            IQueryable<Car> actualCars =
                    this.carService.RetrieveAllCars();

            //then
            actualCars.Should().BeEquivalentTo(expectedCars);

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                        Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllCar(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();


        }
    }
}
