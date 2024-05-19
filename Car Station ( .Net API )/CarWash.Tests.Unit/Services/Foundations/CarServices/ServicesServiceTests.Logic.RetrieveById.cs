using CarWash.Models.ServicesModels;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.CarServices
{
    public partial class ServicesServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveServiceByIdAsunc()
        {
            //given
            Guid randomServiceId = Guid.NewGuid();
            Guid inputServiceId = randomServiceId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Service randomService = CreateRandomService(randomDateTime);
            Service storageService = randomService;
            Service expectedService = storageService;

            this.storageBrokerMock.Setup(broker =>
                    broker.SelectAsyncServices(inputServiceId))
                .ReturnsAsync(storageService);

            //when
            Service actualService = await this.serviceService.RetrieveServiceByIdAsync(inputServiceId);
            
            //then
            actualService.Should().BeEquivalentTo(expectedService);
            
            //this.dateTimeBrokerMock.Verify(broker=>
            //    broker.GetCurrentDateTime(),
            //    Times.Never);
            
            this.storageBrokerMock.Verify(broker=>
                    broker.SelectAsyncServices(inputServiceId),
                Times.Once);
            
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
            
        }
    }
}