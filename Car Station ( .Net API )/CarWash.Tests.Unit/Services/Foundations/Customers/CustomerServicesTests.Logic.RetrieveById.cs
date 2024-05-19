using CarWash.Models.Customers;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        [Fact]
        public async Task ShouldRetrieveCustomerByIdAsunc()
        {
            //given
            Guid randomCustomerId = Guid.NewGuid();
            Guid inputCustomerId = randomCustomerId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Customer randomCustomer = CreateRandomCustomer(randomDateTime);
            Customer storageCustomer = randomCustomer;
            Customer expectedCustomer = storageCustomer;

            this.storageBrokerMock.Setup(broker =>
                    broker.SelectAsyncCustomer(inputCustomerId))
                .ReturnsAsync(storageCustomer);
            
            //when
            Customer actualCustomer = await this.customerService.RetrieveCustomerByIdAsync(inputCustomerId);
            //then
            actualCustomer.Should().BeEquivalentTo(expectedCustomer);
            
            //this.dateTimeBrokerMock.Verify(broker=>
            //    broker.GetCurrentDateTime(),
            //    Times.Never);
            
            this.storageBrokerMock.Verify(broker=>
                broker.SelectAsyncCustomer(inputCustomerId),
                Times.Once);
            
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}