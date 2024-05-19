using System;
using CarWash.Models.Cars;
using CarWash.Models.Cars.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Services.Foundations.Cars
{
    public partial class CarService
	{
        private delegate IQueryable<Car> ReturningQueryableCarFunction();
        private delegate ValueTask<Car> ReturningCarFunction();

        private async ValueTask<Car> TryCatch(ReturningCarFunction returningCarFunction)
        {
            try
            {
                return await returningCarFunction();
            }
            catch (NullCarException nullCarException)
            {
                throw CreateAndLogValidationException(nullCarException);
            }
            catch (InvalidCarException invalidCarInputException)
            {
                throw CreateAndLogValidationException(invalidCarInputException);
            }
            catch (NotFoundCarException notFoundCarException)
            {
                throw CreateAndLogValidationException(notFoundCarException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsCarException =
                    new AlreadyExistsCarException(duplicateKeyException);

                throw CreateAndLogValidationException(alreadyExistsCarException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedCarException =
                    new LockedCarException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyException(lockedCarException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogDependencyException(dbUpdateException);
            }
            catch (Exception exception)
            {
                var failedCarServiceException =
                    new FailedCarServiceException(exception);

                throw CreateAndLogServiceException(failedCarServiceException);
            }
        }

        private IQueryable<Car> TryCatch(ReturningQueryableCarFunction returningQueryableCarFunction)
        {
            try
            {
                return returningQueryableCarFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (Exception exception)
            {
                var failedCarServiceException =
                    new FailedCarServiceException(exception);

                throw CreateAndLogServiceException(failedCarServiceException);
            }
        }

        private CarValidationException CreateAndLogValidationException(Exception exception)
        {
            var carValidationException = new CarValidationException(exception);
            //this.loggingBroker.LogError(carValidationException);

            return carValidationException;
        }

        private CarDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var carDependencyException = new CarDependencyException(exception);
            //this.loggingBroker.LogCritical(carDependencyException);

            return carDependencyException;
        }

        private CarDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var carDependencyException = new CarDependencyException(exception);
            //this.loggingBroker.LogError(carDependencyException);

            return carDependencyException;
        }

        private CarServiceException CreateAndLogServiceException(Exception exception)
        {
            var carServiceException = new CarServiceException(exception);
            //this.loggingBroker.LogError(carServiceException);

            return carServiceException;
        }
    }
}

