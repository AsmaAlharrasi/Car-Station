using CarWash.Models.Customers;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        [Fact]
        public async Task shouldRegisterCustomerAsync()
        {
            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            DateTimeOffset dateTime = randomDateTime;
            Customer randomCustomer = CreateRandomCustomer(randomDateTime);
            randomCustomer.UpdatedBy = randomCustomer.CreatedBy;
            Customer inputCustomer = randomCustomer;
            Customer StorageCustomer = randomCustomer;
            Customer expectesCustomer = StorageCustomer;

            //this.dateTimeBrokerMock.Setup(Brokers =>
            //        Brokers.GetCurrentDateTime())
            //    .Returns(dateTime);

            this.storageBrokerMock.Setup(broker =>
                    broker.InsertCustomer(inputCustomer))
                .ReturnsAsync(StorageCustomer);

            //when 
            Customer actualCustomer =
                await this.customerService.AddCustomerAsync(inputCustomer);

            //then
            actualCustomer.Should().BeEquivalentTo(expectesCustomer);

            //this.dateTimeBrokerMock.Verify(broker =>
            //        broker.GetCurrentDateTime(),
            //    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                    broker.InsertCustomer(inputCustomer),
                Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}