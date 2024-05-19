
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

        public void ShouldRetrieveAllServicesRequests() 
        {

            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            IQueryable<ServicesRequest> randomServicesRequests = CreateRandomServicesRequests(randomDateTime);
            IQueryable<ServicesRequest> storageServicesRequests = randomServicesRequests;
            IQueryable<ServicesRequest> expectedServicesRequests = storageServicesRequests;

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAllServicesRequest())
                .Returns(storageServicesRequests);
            //when
            IQueryable<ServicesRequest> actualServicesRequests =
               this.servicesRequestService.RetriveAllServicesRequest();

            //then
            actualServicesRequests.Should().BeEquivalentTo(expectedServicesRequests);

            this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                        Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllServicesRequest(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();


        }
    }
}
