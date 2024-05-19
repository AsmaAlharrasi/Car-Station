using System;
using CarWash.Models.Customers;
using CarWash.Models.Customers.Exceptions;
using CarWash.Models.ServicesModels;
using CarWash.Models.ServicesModels.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Services.Foundations.Customers
{
	public partial class CustomerService : ICustomerService
    {
        private delegate IQueryable<Customer> ReturningQueryableServiceFunction();
        private delegate ValueTask<Customer> ReturningServiceFunction();

        private async ValueTask<Customer> TryCatch(ReturningServiceFunction returningServiceFunction)
        {
            try
            {
                return await returningServiceFunction();
            }
            catch (NullCustomerException nullCustomerException)
            {
                throw CreateAndLogValidationException(nullCustomerException);
            }
            catch (InvalidCustomerException invalidCustomerException)
            {
                throw CreateAndLogValidationException(invalidCustomerException);
            }
            catch (NotFoundCustomerException notFoundCustomerException)
            {
                throw CreateAndLogValidationException(notFoundCustomerException);
            }

            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedServiceException =
                    new LockedServiceException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyException(lockedServiceException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDependencyException(dbUpdateException);
            }
            catch (Exception exception)
            {
                var failedServiceServiceException =
                    new FailedServiceServiceException(exception);

                throw CreateAndLogServiceException(failedServiceServiceException);
            }
        }

        private IQueryable<Customer> TryCatch(
          ReturningQueryableServiceFunction returningQueryableServiceFunction)
        {
            try
            {
                return returningQueryableServiceFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (Exception exception)
            {
                var failedCustomerException =
                    new FailedCustomerServiceException(exception);

                throw CreateAndLogServiceException(failedCustomerException);
            }
        }

        private CustomerValidationException CreateAndLogValidationException(Exception exception)
        {
            var customerValidationException = new CustomerValidationException(exception);
            //this.loggingBroker.LogError(serviceValidationException);

            return customerValidationException;
        }

        private CustomerDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var customerDependencyException = new CustomerDependencyException(exception);
            // this.loggingBroker.LogCritical(serviceDependencyException);

            return customerDependencyException;
        }

        private CustomerDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var customerDependencyException = new CustomerDependencyException(exception);
            //this.loggingBroker.LogError(serviceDependencyException);

            return customerDependencyException;
        }

        private CustomerServiceException CreateAndLogServiceException(Exception exception)
        {
            var customerException = new CustomerServiceException(exception);
            //this.loggingBroker.LogError(serviceException);

            return customerException;
        }
    }
}

