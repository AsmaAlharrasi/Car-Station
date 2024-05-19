using CarWash.Models.Employees;
using CarWash.Models.Employees.Execptions;
using CarWash.Services.Foundations.Employees;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace CarWash.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : RESTFulController
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Employee>> PostEmployeeAsync(Employee employee)
        {
            try
            {
                Employee registeredEmployee =
                    await this.employeeService.AddEmployeeAsync(employee);

                return Created(registeredEmployee);
            }
            catch (EmployeeValidationException employeeValidationException)
                when (employeeValidationException.InnerException is AlreadyExistsEmployeeException)
            {
                return Conflict(employeeValidationException.InnerException);
            }
            catch (EmployeeValidationException employeeValidationException)
            {
                return BadRequest(employeeValidationException.InnerException);
            }
            catch (EmployeeDependencyException employeeDependencyException)
            {
                return InternalServerError(employeeDependencyException);
            }
            catch (EmployeeServiceException employeeServiceException)
            {
                return InternalServerError(employeeServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Employee>> GetAllEmployees()
        {
            try
            {
                IQueryable<Employee> storageEmployees =
                    this.employeeService.RetrieveAllEmployees();

                return Ok(storageEmployees);
            }
            catch (EmployeeDependencyException employeeDependencyException)
            {
                return Problem(employeeDependencyException.Message);
            }
            catch (EmployeeServiceException employeeServiceException)
            {
                return Problem(employeeServiceException.Message);
            }
        }

        [HttpGet("{employeeId}")]
        public async ValueTask<ActionResult<Employee>> GetEmployeeAsync(Guid employeeId)
        {
            try
            {
                Employee storageEmployee =
                    await this.employeeService.RetrieveEmployeeByIdAsync(employeeId);

                return Ok(storageEmployee);
            }
            catch (EmployeeValidationException employeeValidationException)
                when (employeeValidationException.InnerException is NotFoundEmployeeException)
            {
                string innerMessage = GetInnerMessage(employeeValidationException);

                return NotFound(innerMessage);
            }
            catch (EmployeeValidationException employeeValidationException)
            {
                string innerMessage = GetInnerMessage(employeeValidationException);

                return BadRequest(employeeValidationException);
            }
            catch (EmployeeDependencyException employeeDependencyException)
            {
                return Problem(employeeDependencyException.Message);
            }
            catch (EmployeeServiceException employeeServiceException)
            {
                return Problem(employeeServiceException.Message);
            }
        }

        [HttpPut("{employeeId}")]
        public async ValueTask<ActionResult<Employee>> PutEmployeeAsync(Employee employee)
        {
            try
            {
                Employee registeredEmployee =
                    await this.employeeService.ModifyEmployeeAsync(employee);

                return Ok(registeredEmployee);
            }
            catch (EmployeeValidationException employeeValidationException)
                when (employeeValidationException.InnerException is NotFoundEmployeeException)
            {
                string innerMessage = GetInnerMessage(employeeValidationException);

                return NotFound(innerMessage);
            }
            catch (EmployeeValidationException employeeValidationException)
            {
                string innerMessage = GetInnerMessage(employeeValidationException);

                return BadRequest(innerMessage);
            }
            catch (EmployeeDependencyException employeeDependencyException)
                when (employeeDependencyException.InnerException is LockedEmployeeException)
            {
                string innerMessage = GetInnerMessage(employeeDependencyException);

                return Locked(innerMessage);
            }
            catch (EmployeeDependencyException employeeDependencyException)
            {
                return Problem(employeeDependencyException.Message);
            }
            catch (EmployeeServiceException employeeServiceException)
            {
                return Problem(employeeServiceException.Message);
            }
        }

        [HttpDelete("{employeeId}")]
        public async ValueTask<ActionResult<Employee>> DeleteEmployeeAsync(Guid employeeId)
        {
            try
            {
                Employee storageEmployee =
                    await this.employeeService.RemoveEmployeeByIdAsync(employeeId);

                return Ok(storageEmployee);
            }
            catch (EmployeeValidationException employeeValidationException)
                when (employeeValidationException.InnerException is NotFoundEmployeeException)
            {
                string innerMessage = GetInnerMessage(employeeValidationException);

                return NotFound(innerMessage);
            }
            catch (EmployeeValidationException employeeValidationException)
            {
                string innerMessage = GetInnerMessage(employeeValidationException);

                return BadRequest(employeeValidationException);
            }
            catch (EmployeeDependencyException employeeDependencyException)
               when (employeeDependencyException.InnerException is LockedEmployeeException)
            {
                string innerMessage = GetInnerMessage(employeeDependencyException);

                return Locked(innerMessage);
            }
            catch (EmployeeDependencyException employeeDependencyException)
            {
                return Problem(employeeDependencyException.Message);
            }
            catch (EmployeeServiceException employeeServiceException)
            {
                return Problem(employeeServiceException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
            exception.InnerException.Message;
    }
}
