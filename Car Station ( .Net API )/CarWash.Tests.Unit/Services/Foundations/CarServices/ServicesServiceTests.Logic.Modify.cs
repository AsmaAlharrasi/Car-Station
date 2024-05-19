using CarWash.Models.Customers;
using CarWash.Models.ServicesModels;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.CarServices
{
    public partial class ServicesServiceTests
    {
        [Fact]
        public async Task ShouldModifyServiceAsync()
        {
            //given
            int randomNumber = GetRandomNumber();
            int randomDays = randomNumber;
            DateTimeOffset randomDate = GetRandomDateTime();
            Service randonService = CreateRandomService();
            Service inputService = randonService;
            Service afterupdateStorageService = inputService;
            Service expectedService = afterupdateStorageService;
            Service beforeUpdateStorageService = randonService.DeepClone();
            inputService.UpdatedDate = randomDate;
            Guid ServiceId = inputService.Id;

            this.storageBrokerMock.Setup(broker =>
                    broker.SelectAsyncServices(ServiceId))
                .ReturnsAsync(beforeUpdateStorageService);

            /*this.dateTimeBrokerMock.Setup(broker =>
            broker.GetCurrentDateTime())
            .Returns(randomDate);*/

            this.storageBrokerMock.Setup(broker =>
                    broker.UpdateAsyncServices(inputService))
                .ReturnsAsync(afterupdateStorageService);
            //when 
            Service actualService = await this.serviceService.ModifyServiceAsync(inputService);
            //then 
            actualService.Should().BeEquivalentTo(expectedService);

            this.storageBrokerMock.Verify(broker =>
                    broker.SelectAsyncServices(ServiceId),
                Times.Once);

            /* this.dateTimeBrokerMock.Verify(broker =>
                     broker.GetCurrentDateTime(),
                     Times.Once);*/

            this.storageBrokerMock.Verify(broker =>
                    broker.UpdateAsyncServices(inputService),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
            
        }
    }
}