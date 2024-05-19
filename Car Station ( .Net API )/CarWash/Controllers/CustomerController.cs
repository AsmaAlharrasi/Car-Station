using CarWash.Models.Customers;
using CarWash.Models.Customers.Exceptions;
using CarWash.Models.ServicesModels.Exceptions;
using CarWash.Services.Foundations.Customers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using IQueryable = System.Linq.IQueryable;

namespace CarWash.Controllers
{
    [EnableCors("AllowAllOrigins")]

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : RESTFulController
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            try
            {
                Customer createCustomer = await customerService.AddCustomerAsync(customer);
                return Created(createCustomer);
            }
            catch (CustomerValidationException customerValidationException)
            when (customerValidationException.InnerException is AlreadyExistsCustomerException)
            {
                return Conflict(customerValidationException.InnerException);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerException)
            {
                return Problem(customerException.Message);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Customer>> GetAllCustomers()
        {
            try
            {
                IQueryable allCustomers =
                    this.customerService.RetrieveAllCustomers();
                return Ok(allCustomers);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerException)
            {
                return Problem(customerException.Message);
            }
        }

        [HttpGet("{customerId}")]
        public async ValueTask<ActionResult<Customer>> GetCustomerByIdAsync(Guid customerId)
        {
            try
            {
                Customer storageCustomer =
                    await this.customerService.RetrieveCustomerByIdAsync(customerId);
                return Ok(storageCustomer);
            }
            catch (CustomerValidationException customerValidationException)
            when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);
                return NotFound(innerMessage);
            }
            catch (CustomerValidationException customerValidationException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);
                return BadRequest(innerMessage);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerException)
            {
                return Problem(customerException.Message);
            }
        }

        [HttpPut("{customerId}")]
        public async ValueTask<ActionResult<Customer>> PutCustomerAsync(Customer customer)
        {
            try
            {
                Customer registeredCustomer =
                    await this.customerService.ModifyCustomerAsync(customer);
                return Ok(registeredCustomer);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);
                return NotFound(innerMessage);
            }
            catch (CustomerValidationException customerValidationException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);
                return BadRequest(innerMessage);
            }
            catch (CustomerDependencyException customerDependencyException)
                when (customerDependencyException.InnerException is LockedServiceException)
            {
                string innerMessage = GetInnerMessage(customerDependencyException);

                return Locked(innerMessage);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerException)
            {
                return Problem(customerException.Message);
            }
        }

        [HttpDelete("{customerId}")]
        public async ValueTask<ActionResult<Customer>> DeleteServiceAsync(Guid customerId)
        {
            try
            {
                Customer storageService =
                    await this.customerService.RemoveCustomerByIdAsync(customerId);
                return Ok(storageService);
            }
            catch (CustomerValidationException customerValidationException)
                when (customerValidationException.InnerException is NotFoundCustomerException)
            {
                string innerMessage = GetInnerMessage(customerValidationException);
                return NotFound(innerMessage);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.Message);
            }
            catch (CustomerDependencyException customerDependencyException)
                when (customerDependencyException.InnerException is LockedServiceException)
            {
                string innerMessage = GetInnerMessage(customerDependencyException);
                return Locked(innerMessage);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return Problem(customerDependencyException.Message);
            }
            catch (CustomerServiceException customerException)
            {
                return Problem(customerException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
            exception.InnerException.Message;
    }
}
