using System;
using CarWash.Models.Customers;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        [Fact]
        public async Task ShouldModifyCustomerAsync()
        {
            //given
            int randomNumber = GetRandomNumber();
            int randomDays = randomNumber;
            DateTimeOffset randomDate = GetRandomDateTime();
            Customer randonCustomer = CreateRandomCustomer();
            Customer inputCustomer = randonCustomer;
            Customer afterupdateStorageCustomer = inputCustomer;
            Customer expectedCustomer = afterupdateStorageCustomer;
            Customer beforeUpdateStorageCustomer = randonCustomer.DeepClone();
            inputCustomer.UpdatedDate = randomDate;
            Guid CustomerId = inputCustomer.Id;

             this.storageBrokerMock.Setup(broker =>
            broker.SelectAsyncCustomer(CustomerId))
                 .ReturnsAsync(beforeUpdateStorageCustomer);

            /*this.dateTimeBrokerMock.Setup(broker =>
            broker.GetCurrentDateTime())
            .Returns(randomDate);*/

             this.storageBrokerMock.Setup(broker =>
            broker.UpdateAsyncCustomer(inputCustomer))
            .ReturnsAsync(afterupdateStorageCustomer);

            //when
            Customer actualCustomer = await this.customerService.ModifyCustomerAsync(inputCustomer);

            //then
            actualCustomer.Should().BeEquivalentTo(expectedCustomer);

            this.storageBrokerMock.Verify(broker =>
                    broker.SelectAsyncCustomer(CustomerId),
                    Times.Once);

           /* this.dateTimeBrokerMock.Verify(broker =>
                    broker.GetCurrentDateTime(),
                    Times.Once);*/

            this.storageBrokerMock.Verify(broker =>
                    broker.UpdateAsyncCustomer(inputCustomer),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            
        }
    }
}

