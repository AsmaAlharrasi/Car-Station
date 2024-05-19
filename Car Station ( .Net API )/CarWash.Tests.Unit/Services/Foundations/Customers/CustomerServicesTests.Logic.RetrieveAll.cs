using CarWash.Models.Customers;
using FluentAssertions;
using Moq;

namespace CarWash.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServicesTests
    {
        [Fact]
        public void ShouldRetrieveAllStudents()
        {
            //given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            IQueryable<Customer> randomCustomers = CreateRandomCustomers(randomDateTime);
            IQueryable<Customer> storageCustomers = randomCustomers;
            IQueryable<Customer> expectedCustomers = storageCustomers;

            this.storageBrokerMock.Setup(broker =>
                    broker.SelectAllCustomers())
                    .Returns(storageCustomers);
            //when 
            IQueryable<Customer> actualCustomers = this.customerService.RetrieveAllCustomers();
            
            //then
            actualCustomers.Should().BeEquivalentTo(expectedCustomers);
            
            this.dateTimeBrokerMock.Verify(broker=>
                broker.GetCurrentDateTime(),
                Times.Never);
            this.storageBrokerMock.Verify(broker=>
                broker.SelectAllCustomers(),
                Times.Once);
            
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            
        }
    }
}