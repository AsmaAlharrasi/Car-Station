using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWash.Models.ServicesModels;
using CarWash.Models.ServicesModels.Exceptions;
using CarWash.Services.Foundations.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarWash.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : RESTFulController
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService) =>
            this.serviceService = serviceService;

        [HttpPost]
        public async ValueTask<ActionResult<Service>> PostServicesAsync(Service service)
        {
            //try
            //{
                Service createService = await this.serviceService.AddServiceAsync(service);
                return Created(createService);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //    when (serviceValidationException.InnerException is AlreadyExistsServiceException)
            //{
            //    return Conflict(serviceValidationException.InnerException);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //{
            //    return BadRequest(serviceValidationException.InnerException);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //{
            //    return InternalServerError(serviceDependencyException);
            //}
            //catch (ServiceServiceException serviceServiceException)
            //{
            //    return InternalServerError(serviceServiceException);
            //}
        }

        [HttpGet]
        public ActionResult<IQueryable<Service>> GetAllServices()
        {
            //try
            //{
                IQueryable allServices =
                this.serviceService.RetrieveAllServices();
                return Ok(allServices);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //{
            //    return Problem(serviceDependencyException.Message);
            //}
            //catch (ServiceServiceException serviceServiceException)
            //{
            //    return Problem(serviceServiceException.Message);
            //}

        }

        [HttpGet("{serviceId}")]
        public async ValueTask<ActionResult<Service>> GetServiceByIdAsync(Guid serviceId)
        {
            //try
            //{
                Service storageService =
                    await this.serviceService.RetrieveServiceByIdAsync(serviceId);
                return Ok(storageService);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //    when (serviceValidationException.InnerException is NotFoundAssignmentException)
            //{
            //    string innerMessage = GetInnerMessage(serviceValidationException);

            //    return NotFound(innerMessage);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //{
            //    string innerMessage = GetInnerMessage(serviceValidationException);

            //    return BadRequest(innerMessage);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //{
            //    return Problem(serviceDependencyException.Message);
            //}
            //catch (ServiceServiceException serviceServiceException)
            //{
            //    return Problem(serviceServiceException.Message);
            //}
        }

        [HttpPut("{serviceId}")]
        public async ValueTask<ActionResult<Service>> PutServiceAsync(Service service)
        {
            //try
            //{
                Service registeredService =
                await this.serviceService.ModifyServiceAsync(service);
                return Ok(registeredService);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //    when (serviceValidationException.InnerException is NotFoundAssignmentException)
            //{
            //    string innerMessage = GetInnerMessage(serviceValidationException);

            //    return NotFound(innerMessage);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //{
            //    string innerMessage = GetInnerMessage(serviceValidationException);

            //    return BadRequest(innerMessage);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //    when (serviceDependencyException.InnerException is LockedAssignmentException)
            //{
            //    string innerMessage = GetInnerMessage(serviceDependencyException);

            //    return Locked(innerMessage);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //{
            //    return Problem(serviceDependencyException.Message);
            //}
            //catch (ServiceServiceException serviceServiceException)
            //{
            //    return Problem(serviceServiceException.Message);
            //}
        }

        [HttpDelete("{serviceId}")]
        public async ValueTask<ActionResult<Service>> DeleteServiceAsync(Guid serviceId)
        {
            //try
            //{
                Service storageService =
                    await this.serviceService.RemoveServiceByIdAsync(serviceId);
                return Ok(storageService);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //    when (serviceValidationException.InnerException is NotFoundAssignmentException)
            //{
            //    string innerMessage = GetInnerMessage(serviceValidationException);

            //    return NotFound(innerMessage);
            //}
            //catch (ServiceValidationException serviceValidationException)
            //{
            //    return BadRequest(serviceValidationException.Message);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //    when (serviceDependencyException.InnerException is LockedAssignmentException)
            //{
            //    string innerMessage = GetInnerMessage(serviceDependencyException);

            //    return Locked(innerMessage);
            //}
            //catch (ServiceDependencyException serviceDependencyException)
            //{
            //    return Problem(serviceDependencyException.Message);
            //}
            //catch (ServiceServiceException serviceServiceException)
            //{
            //    return Problem(serviceServiceException.Message);
            //}
        }
    }

    //private static string GetInnerMessage(Exception exception) =>
    //    exception.InnerException.Message;
}

