using CarWash.Models.Employees;
using CarWash.Models.Employees.Execptions;
using FluentAssertions.Equivalency.Tracing;

namespace CarWash.Services.Foundations.Employees
{
    public partial class EmployeeService
    {
        private static void ValidateEmployeeId(Guid employeeId)
        {
            Validate((Rule: IsInvalidX(employeeId), Parameter: nameof(Employee.Id)));
        }

        private static void ValidateStorageEmployee(Employee maybeEmployee, Guid employeeId)
        {
            if (maybeEmployee is null)
            {
                throw new NotFoundEmployeeException(employeeId);
            }
        }

        private void ValidateEmployeeOnCreate(Employee employee)
        {
            ValidateEmployee(employee);

            Validate
            (
                (Rule: IsInvalidX(employee.Id), Parameter: nameof(Employee.Id)),
                (Rule: IsInvalidX(employee.UserID), Parameter: nameof(Employee.UserID)),
                (Rule: IsInvalidX(employee.EmployeeStatus), Parameter: nameof(Employee.EmployeeStatus)),
                (Rule: IsInvalidX(employee.FirstName), Parameter: nameof(Employee.FirstName)),
                //(Rule: IsInvalidX(employee.MiddleName), Parameter: nameof(Employee.MiddleName)),
                (Rule: IsInvalidX(employee.LastName), Parameter: nameof(Employee.LastName)),
                (Rule: IsInvalidX(employee.CreatedBy), Parameter: nameof(Employee.CreatedBy)),
                (Rule: IsInvalidX(employee.UpdatedBy), Parameter: nameof(Employee.UpdatedBy)),
                (Rule: IsInvalidX(employee.CreatedDate), Parameter: nameof(Employee.CreatedDate)),
                (Rule: IsInvalidX(employee.UpdatedDate), Parameter: nameof(Employee.UpdatedDate)),
                (Rule: IsNotRecent(employee.CreatedDate), Parameter: nameof(Employee.CreatedDate)),

                (Rule: IsNotSame(
                    firstId: employee.UpdatedBy,
                    secondId: employee.CreatedBy,
                    secondIdName: nameof(Employee.CreatedBy)),
                Parameter: nameof(Employee.UpdatedBy)),

                (Rule: IsNotSame(
                    firstDate: employee.UpdatedDate,
                    secondDate: employee.CreatedDate,
                    secondDateName: nameof(Employee.CreatedDate)),
                Parameter: nameof(Employee.UpdatedDate))
            );
        }

        private static dynamic IsInvalidX(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalidX(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalidX(EmployeeStatus status) => new
        {
            Condition = status != EmployeeStatus.Free,
            Message = "Value is invalid"
        };

        private static dynamic IsInvalidX(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };


        private dynamic IsNotRecent(DateTimeOffset dateTimeOffset) => new
        {
            Condition = IsDateNotRecent(dateTimeOffset),
            Message = "Date is not recent"
        };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private void ValidateEmployeeOnModify(Employee employee)
        {
            ValidateEmployee(employee);   
            Validate
            (
                (Rule: IsInvalidX(employee.Id), Parameter: nameof(Employee.Id)),
                (Rule: IsInvalidX(employee.UserID), Parameter: nameof(Employee.UserID)),
                (Rule: IsInvalidX(employee.EmployeeStatus), Parameter: nameof(Employee.EmployeeStatus)),
                (Rule: IsInvalidX(employee.FirstName), Parameter: nameof(Employee.FirstName)),
                //(Rule: IsInvalidX(employee.MiddleName), Parameter: nameof(Employee.MiddleName)),
                (Rule: IsInvalidX(employee.LastName), Parameter: nameof(Employee.LastName)),
                (Rule: IsInvalidX(employee.CreatedBy), Parameter: nameof(Employee.CreatedBy)),
                (Rule: IsInvalidX(employee.UpdatedBy), Parameter: nameof(Employee.UpdatedBy)),
                (Rule: IsInvalidX(employee.CreatedDate), Parameter: nameof(Employee.CreatedDate)),
                (Rule: IsInvalidX(employee.UpdatedDate), Parameter: nameof(Employee.UpdatedDate)),
                (Rule: IsNotRecent(employee.UpdatedDate), Parameter: nameof(Employee.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: employee.UpdatedDate,
                    secondDate: employee.CreatedDate,
                    secondDateName: nameof(Employee.CreatedDate)),

                Parameter: nameof(Employee.UpdatedDate))
            );
        }


        //--------

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTime();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

        private static void ValidateEmployee(Employee employee)
        {
            if (employee is null)
            {
                throw new NullEmployeeException();
            }
        }

        private static void ValidateAgainstStorageEmployeeOnModify(Employee inputEmployee, Employee storageEmployee)
        {
            Validate(
                (Rule: IsNotSame(
                    firstId: inputEmployee.CreatedBy,
                    secondId: storageEmployee.CreatedBy,
                    secondIdName: nameof(Employee.CreatedBy)),
                Parameter: nameof(Employee.CreatedBy)),

                (Rule: IsNotSame(
                    firstDate: inputEmployee.CreatedDate,
                    secondDate: storageEmployee.CreatedDate,
                    secondDateName: nameof(Employee.CreatedDate)),
                Parameter: nameof(Employee.CreatedDate)),

                (Rule: IsSame(
                    firstDate: inputEmployee.UpdatedDate,
                    secondDate: storageEmployee.UpdatedDate,
                    secondDateName: nameof(Employee.UpdatedDate)),
                Parameter: nameof(Employee.UpdatedDate)));
        }


        private static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        private static bool IsInvalid(Guid input) => input == default;
        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEmployeeException = new InvalidEmployeeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEmployeeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEmployeeException.ThrowIfContainsErrors();
        }
    }
}
