using CarWash.Models.ServicesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.CarServices
{
    public partial class ServicesServiceTests
    {
        [Fact]
        public async Task ShouldAddServiceAsync()
        {
            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            DateTimeOffset dateTime = randomDateTime;
            Service randomService = CreateRandomService(randomDateTime);
            randomService.UpdatedBy = randomService.CreatedBy;
            Service inputService = randomService;
            Service StorageService = randomService;
            Service expectesService = StorageService;
            
            //this.dateTimeBrokerMock.Setup(Brokers =>
            //        Brokers.GetCurrentDateTime())
            //    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                    broker.InsertServices(inputService))
                .ReturnsAsync(StorageService);
            //when
            Service actualService =
                await this.serviceService.AddServiceAsync(inputService);

            //then
            actualService.Should().BeEquivalentTo(expectesService);

            //this.dateTimeBrokerMock.Verify(broker =>
            //        broker.GetCurrentDateTime(),
            //    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertServices(inputService),
                Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            // this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
