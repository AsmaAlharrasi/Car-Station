using CarWash.Models.ServicesModels;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.CarServices
{
    public partial class ServicesServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllServices()
        {
            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            IQueryable<Service> randomServices = CreateRandomServices(randomDateTime);
            IQueryable<Service> storageServices = randomServices;
            IQueryable<Service> expectedServices = storageServices;

            this.storageBrokerMock.Setup(broker =>
                    broker.SelectAllServices())
                .Returns(storageServices);
            //when
            IQueryable<Service> actualServices = this.serviceService.RetrieveAllServices();
            
            //then
            actualServices.Should().BeEquivalentTo(expectedServices);
            
            this.dateTimeBrokerMock.Verify(broker=>
                    broker.GetCurrentDateTime(),
                Times.Never);
            this.storageBrokerMock.Verify(broker=>
                    broker.SelectAllServices(),
                Times.Once);
            
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            // this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}