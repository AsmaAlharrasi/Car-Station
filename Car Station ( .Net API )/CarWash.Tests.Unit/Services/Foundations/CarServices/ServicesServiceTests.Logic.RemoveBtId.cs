using CarWash.Models.ServicesModels;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.CarServices
{
    public partial class ServicesServiceTests
    {
        [Fact]
        public async Task ShouldRemoveServiceByIdAsync()
        {
            //given
            Service randomService = CreateRandomService();
            Guid ServiceId = randomService.Id;
            Service storageService = randomService;
            Service expectedService = storageService;

            this.storageBrokerMock.Setup(broker =>
                    broker.SelectAsyncServices(ServiceId))
                .ReturnsAsync(storageService);

            this.storageBrokerMock.Setup(broker =>
                    broker.DeleteAsyncServices(storageService))
                .ReturnsAsync(expectedService);
            //when 
            Service actualService = await this.serviceService.RemoveServiceByIdAsync(ServiceId);
            //then
            actualService.Should().BeEquivalentTo(expectedService);
            
            this.storageBrokerMock.Verify(broker=>
                    broker.SelectAsyncServices(ServiceId),
                Times.Once);
            
            this.storageBrokerMock.Verify(broker=>
                    broker.DeleteAsyncServices(storageService),
                Times.Once);
            
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}