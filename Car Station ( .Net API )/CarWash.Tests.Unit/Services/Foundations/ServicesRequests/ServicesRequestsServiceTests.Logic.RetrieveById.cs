using CarWash.Models.Cars;
using CarWash.Models.ServicesRequests;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Tests.Unit.Services.Foundations.ServicesRequests
{
    public partial class ServicesRequestsServiceTests
    {
        [Fact]

        public async Task ShouldRetrieveServicesRequestByIdAsync()
        {
            //given
            Guid randomServicesRequestId = Guid.NewGuid();
            Guid inputServicesRequestId = randomServicesRequestId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            ServicesRequest randomServicesRequest = CreateRandomServicesRequest(randomDateTime);
            ServicesRequest storageServicesRequest = randomServicesRequest;
            ServicesRequest expectedServicesRequest = storageServicesRequest;

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncServicesRequest(inputServicesRequestId))
                .ReturnsAsync(storageServicesRequest);

            //when
            ServicesRequest actualServicesRequest = 
                await this.servicesRequestService.RetriveServicesRequestByIdAsync(inputServicesRequestId);

            //then
            actualServicesRequest.Should().BeEquivalentTo(expectedServicesRequest);

            this.dateTimeBrokerMock.Verify(broker =>
            broker.GetCurrentDateTime(),
            Times.Never);

            this.storageBrokerMock.Verify(broker =>
            broker.SelectAsyncServicesRequest(inputServicesRequestId),
            Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();



        }
    }
}
