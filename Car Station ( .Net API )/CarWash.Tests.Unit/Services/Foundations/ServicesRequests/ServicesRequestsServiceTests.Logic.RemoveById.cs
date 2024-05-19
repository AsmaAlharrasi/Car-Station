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

        public async Task ShouldRemoveServicesRequestByIdAsync()
        {
            //given -- broker side
            ServicesRequest randomServicesRequest = CreateRandomServicesRequest();
            Guid ServicesRequestId = randomServicesRequest.Id;
            ServicesRequest storageServicesRequest = randomServicesRequest;
            ServicesRequest expectedServicesRequest = storageServicesRequest;

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncServicesRequest(ServicesRequestId))
                .ReturnsAsync(storageServicesRequest);

            this.storageBrokerMock.Setup(broker =>
            broker.DeleteAsyncServicesRequest(storageServicesRequest))
                .ReturnsAsync(storageServicesRequest);

            //when -- service side
            ServicesRequest actualServicesRequest = 
                await this.servicesRequestService.RemoveServicesRequestAsync(ServicesRequestId);

            //then
            actualServicesRequest.Should().BeEquivalentTo(expectedServicesRequest);

            this.storageBrokerMock.Verify(broker =>
               broker.SelectAsyncServicesRequest(ServicesRequestId),
                   Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteAsyncServicesRequest(storageServicesRequest),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
