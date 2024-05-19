
using CarWash.Models.ServicesRequests;
using FluentAssertions;
using FluentAssertions.Equivalency.Tracing;
using Force.DeepCloner;
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

        public async Task ShouldModifyServicesRequestAsync()
        {
            //given
            int randomNumber = GetRandomNumber();
            int randomDays = randomNumber;
            DateTimeOffset randomDate = GetRandomDateTime();
            DateTimeOffset randomInputDate = GetRandomDateTime();
            ServicesRequest randomServicesRequest = CreateRandomServicesRequest(randomInputDate);
            ServicesRequest inputServicesRequest = randomServicesRequest;
            ServicesRequest beforeUpdateStorageServicesRequest = randomServicesRequest.DeepClone();
            ServicesRequest afterUpdateStorageServicesRequest = inputServicesRequest;
            ServicesRequest expectedServicesRequest = afterUpdateStorageServicesRequest;
            inputServicesRequest.UpdatedDate = randomDate;
            Guid ServicesRequestId = inputServicesRequest.Id;

            //this.dateTimeBrokerMock.Setup(broker =>
            //broker.GetCurrentDateTime())
            //    .Returns(randomDate);

            this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncServicesRequest(ServicesRequestId))
                .ReturnsAsync(beforeUpdateStorageServicesRequest);

            this.storageBrokerMock.Setup(broker =>
            broker.UpdateAsyncServicesRequest(inputServicesRequest))
                .ReturnsAsync(afterUpdateStorageServicesRequest);


            //when
            ServicesRequest actualServicesRequest =
               await this.servicesRequestService.ModifyServicesRequestAsync(inputServicesRequest);
            //DateTimeOffset CurrentDateTime = this.dateTimeBroker.GetCurrentDateTime(randomDate);

            //then
            actualServicesRequest.Should().BeEquivalentTo(expectedServicesRequest);

            //this.dateTimeBrokerMock.Verify(broker =>
            //broker.GetCurrentDateTime(),
            //Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.SelectAsyncServicesRequest(ServicesRequestId),
            Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.UpdateAsyncServicesRequest(inputServicesRequest),
            Times.Once);



            this.storageBrokerMock.VerifyNoOtherCalls();
            //this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();

        } 
    }
}
