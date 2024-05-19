using CarWash.Models.Employees;
using CarWash.Models.Employees.Execptions;
using EFxceptions.Models.Exceptions;
using FluentAssertions.Equivalency.Tracing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace CarWash.Services.Foundations.Employees
{
    public partial class EmployeeService
    {
        private delegate ValueTask<Employee> ReturningEmployeeFunction();
        private delegate IQueryable<Employee> ReturningQueryableEmployeeFunction();

        private async ValueTask<Employee> TryCatch(ReturningEmployeeFunction returningEmployeeFunction)
        {
            try
            {
                return await returningEmployeeFunction();
            }
            catch (NullEmployeeException nullEmployeeException)
            {
                throw CreateAndLogValidationException(nullEmployeeException);
            }
            catch (InvalidEmployeeException invalidEmployeeException)
            {
                throw CreateAndLogValidationException(invalidEmployeeException);
            }
            catch (NotFoundEmployeeException notFoundEmployeeException)
            {
                throw CreateAndLogValidationException(notFoundEmployeeException);
            }
            catch (SqlException sqlException)
            {
                var failedEmployeeStorageExceptin =
                    new FailedEmployeeStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedEmployeeStorageExceptin);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {

                var alreadyExistsEmployeeException =
                    new AlreadyExistsEmployeeException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsEmployeeException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedEmployeeException = new LockedEmployeeException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedEmployeeException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedEmployeeStorageException =
                    new FailedEmployeeStorageException(dbUpdateException);

                throw CreateAndLogDependencyException(failedEmployeeStorageException);
            }
            catch (Exception exception)
            {
                var failedEmployeeServiceException =
                    new FailedEmployeeServiceException(exception);

                throw CreateAndLogServiceException(failedEmployeeServiceException);
            }
        }

        private IQueryable<Employee> TryCatch(ReturningQueryableEmployeeFunction returningQueryableEmployeeFunction)
        {
            try
            {
                return returningQueryableEmployeeFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEmployeeStorageExceptin =
                    new FailedEmployeeStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedEmployeeStorageExceptin);
            }
            catch (Exception exception)
            {
                var failedEmployeeServiceException =
                    new FailedEmployeeServiceException(exception);

                throw CreateAndLogServiceException(failedEmployeeServiceException);
            }
        }

        private Exception CreateAndLogDependencyValidationException(Xeption exception)
        {
            var employeeDependencyValidationException =
                new EmployeeDependencyValidationException(exception);

            //this.loggingBroker.LogError(employeeDependencyValidationException);

            return employeeDependencyValidationException;
        }

        private EmployeeValidationException CreateAndLogValidationException(Xeption exception)
        {
            var employeeValidationException = new EmployeeValidationException(exception);
            //this.loggingBroker.LogError(employeeValidationException);

            return employeeValidationException;
        }

        private EmployeeDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var employeeDependencyException = new EmployeeDependencyException(exception);
            //this.loggingBroker.LogCritical(employeeDependencyException);

            return employeeDependencyException;
        }

        private EmployeeDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var employeeDependencyException = new EmployeeDependencyException(exception);
            //this.loggingBroker.LogError(employeeDependencyException);

            return employeeDependencyException;
        }

        private EmployeeServiceException CreateAndLogServiceException(Xeption exception)
        {
            var employeeServiceException = new EmployeeServiceException(exception);
            //this.loggingBroker.LogError(employeeServiceException);

            return employeeServiceException;
        }
    }
}
