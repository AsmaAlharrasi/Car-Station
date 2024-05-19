
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
        public async Task ShouldAddServicesRequestAsync()
        {
            //given -- broker side
            DateTimeOffset dateTime = DateTimeOffset.UtcNow;
            ServicesRequest randomServicesRequest = CreateRandomServicesRequest(dateTime);
            randomServicesRequest.UpdatedBy = randomServicesRequest.CreatedBy;
            randomServicesRequest.UpdatedDate = randomServicesRequest.CreatedDate;
            ServicesRequest inputServicesRequest = randomServicesRequest;
            ServicesRequest storageServicesRequest = randomServicesRequest;
            ServicesRequest expectedServicesRequest = randomServicesRequest;

            this.dateTimeBrokerMock.Setup(broker =>
            broker.GetCurrentDateTime())
                .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertServicesRequest(inputServicesRequest))
                .ReturnsAsync(storageServicesRequest);

            //when -- service side
            ServicesRequest actualServicesRequest = 
                await this.servicesRequestService.AddServicesRequestAsync(inputServicesRequest);

            //then
            actualServicesRequest.Should().BeEquivalentTo(expectedServicesRequest);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertServicesRequest(inputServicesRequest),
            Times.Once);


            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();

        }
    }
}
