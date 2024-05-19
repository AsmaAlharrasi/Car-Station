using CarWash.Models.ServicesRequests.Exceptions;
using CarWash.Models.ServicesRequests;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Services.Foundations.ServiceRequests
{
    public partial class ServiceRequestService
    {
        private delegate IQueryable<ServicesRequest> ReturningQueryableServicesRequestFunction();
        private delegate ValueTask<ServicesRequest> ReturningServicesRequestFunction();

        private async ValueTask<ServicesRequest> TryCatch(ReturningServicesRequestFunction returningServicesRequestFunction)
        {
            try
            {
                return await returningServicesRequestFunction();
            }
            catch (NullServicesRequestException nullServicesRequestException)
            {
                throw CreateAndLogValidationException(nullServicesRequestException);
            }
            catch (InvalidServicesRequestException invalidCarInputException)
            {
                throw CreateAndLogValidationException(invalidCarInputException);
            }
            catch (NotFoundServicesRequestException notFoundServicesRequestException)
            {
                throw CreateAndLogValidationException(notFoundServicesRequestException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsServicesRequestException =
                    new AlreadyExistsServicesRequestException(duplicateKeyException);

                throw CreateAndLogValidationException(alreadyExistsServicesRequestException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedServicesRequestException =
                    new LockedServicesRequestException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyException(lockedServicesRequestException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDependencyException(dbUpdateException);
            }
            catch (Exception exception)
            {
                var failedServicesRequestServiceException =
                    new FailedServicesRequestServiceException(exception);

                throw CreateAndLogServiceException(failedServicesRequestServiceException);
            }
        }

        private IQueryable<ServicesRequest> TryCatch(ReturningQueryableServicesRequestFunction returningQueryableServicesRequestFunction)
        {
            try
            {
                return returningQueryableServicesRequestFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (Exception exception)
            {
                var failedServicesRequestServiceException =
                    new FailedServicesRequestServiceException(exception);

                throw CreateAndLogServiceException(failedServicesRequestServiceException);
            }
        }

        private ServicesRequestValidationException CreateAndLogValidationException(Exception exception)
        {
            var servicesRequestValidationException = new ServicesRequestValidationException(exception);
            //this.loggingBroker.LogError(servicesRequestValidationException);

            return servicesRequestValidationException;
        }

        private ServicesRequestDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var servicesRequestDependencyException = new ServicesRequestDependencyException(exception);
            //this.loggingBroker.LogCritical(servicesRequestDependencyException);

            return servicesRequestDependencyException;
        }

        private ServicesRequestDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var servicesRequestDependencyException = new ServicesRequestDependencyException(exception);
            //this.loggingBroker.LogError(servicesRequestDependencyException);

            return servicesRequestDependencyException;
        }

        private ServicesRequestServiceException CreateAndLogServiceException(Exception exception)
        {
            var servicesRequestServiceException = new ServicesRequestServiceException(exception);
            //this.loggingBroker.LogError(servicesRequestServiceException);

            return servicesRequestServiceException;
        }
    }
}
