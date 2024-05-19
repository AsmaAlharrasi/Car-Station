using CarWash.Models.Customers;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        [Fact]
        public async Task ShouldRemoveCustomerByIdAsync()
        {
            //given 
            Customer randomCustomer = CreateRandomCustomer();
            Guid CustomerId = randomCustomer.Id;
            Customer storageCustomer = randomCustomer;
            Customer expectedCustomer = storageCustomer;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAsyncCustomer(CustomerId))
                .ReturnsAsync(storageCustomer);

            this.storageBrokerMock.Setup(broker =>
                    broker.DeleteAsyncCustomer(storageCustomer))
                .ReturnsAsync(expectedCustomer);

            //when 
            Customer actualCustomer = await this.customerService.RemoveCustomerByIdAsync(CustomerId);
            
            //then
            actualCustomer.Should().BeEquivalentTo(expectedCustomer);
            
            this.storageBrokerMock.Verify(broker=>
                broker.SelectAsyncCustomer(CustomerId),
                Times.Once);
            
            this.storageBrokerMock.Verify(broker=>
                broker.DeleteAsyncCustomer(storageCustomer),
                Times.Once);
            
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}