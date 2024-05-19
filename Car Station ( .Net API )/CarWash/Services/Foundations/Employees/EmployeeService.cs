using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.Employees;

namespace CarWash.Services.Foundations.Employees
{
    public partial class EmployeeService : IEmployeeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public EmployeeService
        (
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker
        )
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        //add
        public ValueTask<Employee> AddEmployeeAsync(Employee employee) =>
          TryCatch(async () =>
          {
              //ValidateEmployeeOnCreate(employee);

              return await this.storageBroker.InsertEmployeeAsync(employee);
          });

        //select all
        public IQueryable<Employee> RetrieveAllEmployees() =>
            TryCatch(() => this.storageBroker.SelectAllEmployees());

        //select by id
        public ValueTask<Employee> RetrieveEmployeeByIdAsync(Guid employeeId) =>
        TryCatch(async () =>
        {
            ValidateEmployeeId(employeeId);

            Employee maybeEmployee =
                await this.storageBroker.SelectEmployeeByIdAsync(employeeId);

            ValidateStorageEmployee(maybeEmployee, employeeId);

            return maybeEmployee;
        });

        //update
        public ValueTask<Employee> ModifyEmployeeAsync(Employee employee) =>
        TryCatch(async () =>
        {
            ValidateEmployeeOnModify(employee);
            Employee maybeEmployee = await this.storageBroker.SelectEmployeeByIdAsync(employee.Id);
            ValidateStorageEmployee(maybeEmployee, employee.Id);
            ValidateAgainstStorageEmployeeOnModify(inputEmployee: employee, storageEmployee: maybeEmployee);

            return await this.storageBroker.UpdateEmployeeAsync(employee);
        });

        //delete 
        public ValueTask<Employee> RemoveEmployeeByIdAsync(Guid employeeId) =>
        TryCatch(async () =>
        {
            ValidateEmployeeId(employeeId);

            Employee maybeEmployee =
               await this.storageBroker.SelectEmployeeByIdAsync(employeeId);

            ValidateStorageEmployee(maybeEmployee, employeeId);

            return await this.storageBroker.DeleteEmployeeAsync(maybeEmployee);
        });
    }
}
