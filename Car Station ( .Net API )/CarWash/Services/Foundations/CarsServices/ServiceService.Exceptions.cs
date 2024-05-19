using CarWash.Models.ServicesModels;

using CarWash.Models.ServicesModels.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Services.Foundations.Services
{
    public partial class ServiceService
    {
        private delegate IQueryable<Service> ReturningQueryableServiceFunction();
        private delegate ValueTask<Service> ReturningServiceFunction();

        private async ValueTask<Service> TryCatch(ReturningServiceFunction returningServiceFunction)
        {
            try
            {
                return await returningServiceFunction();
            }
            catch (NullServiceException nullServiceException)
            {
                throw CreateAndLogValidationException(nullServiceException);
            }
            catch (InvalidServiceException invalidServiceInputException)
            {
                throw CreateAndLogValidationException(invalidServiceInputException);
            }
            catch (NotFoundServiceException notFoundServiceException)
            {
                throw CreateAndLogValidationException(notFoundServiceException);
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

        private IQueryable<Service> TryCatch(
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
                var failedServiceServiceException =
                    new FailedServiceServiceException(exception);

                throw CreateAndLogServiceException(failedServiceServiceException);
            }
        }

        private ServiceValidationException CreateAndLogValidationException(Exception exception)
        {
            var serviceValidationException = new ServiceValidationException(exception);
            //this.loggingBroker.LogError(serviceValidationException);

            return serviceValidationException;
        }

        private ServiceDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var serviceDependencyException = new ServiceDependencyException(exception);
           // this.loggingBroker.LogCritical(serviceDependencyException);

            return serviceDependencyException;
        }

        private ServiceDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var serviceDependencyException = new ServiceDependencyException(exception);
            //this.loggingBroker.LogError(serviceDependencyException);

            return serviceDependencyException;
        }

        private ServiceServiceException CreateAndLogServiceException(Exception exception)
        {
            var serviceException = new ServiceServiceException(exception);
            //this.loggingBroker.LogError(serviceException);

            return serviceException;
        }
    }
}
