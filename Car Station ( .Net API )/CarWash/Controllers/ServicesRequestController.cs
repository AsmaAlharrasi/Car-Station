using CarWash.Models.Employees.Execptions;
using CarWash.Models.Employees;
using CarWash.Services.Foundations.ServiceRequests;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using CarWash.Models.ServicesRequests;
using CarWash.Models.ServicesRequests.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace CarWash.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    [Route("api/[controller]")]

    public class ServicesRequestController : RESTFulController
    {
        private readonly IServiceRequestService serviceRequestService;
        public ServicesRequestController(IServiceRequestService serviceRequestService)
        {
            this.serviceRequestService = serviceRequestService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<ServicesRequest>> PostServiceRequestAsync(ServicesRequest serviceRequest)
        {
            try
            {
                ServicesRequest registeredServiceRequest =
                    await this.serviceRequestService.AddServicesRequestAsync(serviceRequest);

                return Created(registeredServiceRequest);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
                when (serviceRequestValidationException.InnerException is AlreadyExistsServicesRequestException)
            {
                return Conflict(serviceRequestValidationException.InnerException);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
            {
                return BadRequest(serviceRequestValidationException.InnerException);
            }
            catch (ServicesRequestDependencyException serviceRequestDependencyException)
            {
                return InternalServerError(serviceRequestDependencyException);
            }
            catch (ServicesRequestServiceException serviceRequestServiceException)
            {
                return InternalServerError(serviceRequestServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<ServicesRequest>> GetAllServicesRequests()
        {
            try
            {
                IQueryable<ServicesRequest> storageServicesRequests =
                    this.serviceRequestService.RetriveAllServicesRequest();

                return Ok(storageServicesRequests);
            }
            catch (ServicesRequestDependencyException servicesRequestDependencyException)
            {
                return Problem(servicesRequestDependencyException.Message);
            }
            catch (ServicesRequestServiceException servicesRequestServiceException)
            {
                return Problem(servicesRequestServiceException.Message);
            }
        }


        [HttpGet("{serviceRequestId}")]
        public async ValueTask<ActionResult<ServicesRequest>> GetServiceRequestAsync(Guid serviceRequestId)
        {
            try
            {
                ServicesRequest storageServicesRequest =
                    await this.serviceRequestService.RetriveServicesRequestByIdAsync(serviceRequestId);

                return Ok(storageServicesRequest);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
                when (serviceRequestValidationException.InnerException is NotFoundServicesRequestException)
            {
                string innerMessage = GetInnerMessage(serviceRequestValidationException);

                return NotFound(innerMessage);
            }
            catch (ServicesRequestValidationException servicesRequestValidationException)
            {
                string innerMessage = GetInnerMessage(servicesRequestValidationException);

                return BadRequest(servicesRequestValidationException);
            }
            catch (ServicesRequestDependencyException serviceRequestDependencyException)
            {
                return Problem(serviceRequestDependencyException.Message);
            }
            catch (ServicesRequestServiceException serviceRequestServiceException)
            {
                return Problem(serviceRequestServiceException.Message);
            }
        }


        [HttpPut]
        public async ValueTask<ActionResult<ServicesRequest>> PutServiceRequestAsync(ServicesRequest servicesRequest)
        {
            try
            {
                ServicesRequest registeredServiceRequest =
                    await this.serviceRequestService.ModifyServicesRequestAsync(servicesRequest);

                return Ok(registeredServiceRequest);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
                when (serviceRequestValidationException.InnerException is NotFoundServicesRequestException)
            {
                string innerMessage = GetInnerMessage(serviceRequestValidationException);

                return NotFound(innerMessage);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
            {
                string innerMessage = GetInnerMessage(serviceRequestValidationException);

                return BadRequest(innerMessage);
            }
            catch (ServicesRequestDependencyException serviceRequestDependencyException)
                when (serviceRequestDependencyException.InnerException is LockedEmployeeException)
            {
                string innerMessage = GetInnerMessage(serviceRequestDependencyException);

                return Locked(innerMessage);
            }
            catch (ServicesRequestDependencyException serviceRequestDependencyException)
            {
                return Problem(serviceRequestDependencyException.Message);
            }
            catch (ServicesRequestServiceException serviceRequestServiceException)
            {
                return Problem(serviceRequestServiceException.Message);
            }
        }

        [HttpDelete("{serviceRequestId}")]
        public async ValueTask<ActionResult<ServicesRequest>> DeleteEmployeeAsync(Guid serviceRequestId)
        {
            try
            {
                ServicesRequest storageServiceRequest =
                    await this.serviceRequestService.RemoveServicesRequestAsync(serviceRequestId);

                return Ok(storageServiceRequest);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
                when (serviceRequestValidationException.InnerException is NotFoundServicesRequestException)
            {
                string innerMessage = GetInnerMessage(serviceRequestValidationException);

                return NotFound(innerMessage);
            }
            catch (ServicesRequestValidationException serviceRequestValidationException)
            {
                string innerMessage = GetInnerMessage(serviceRequestValidationException);

                return BadRequest(serviceRequestValidationException);
            }
            catch (ServicesRequestDependencyException serviceRequestDependencyException)
               when (serviceRequestDependencyException.InnerException is LockedEmployeeException)
            {
                string innerMessage = GetInnerMessage(serviceRequestDependencyException);

                return Locked(innerMessage);
            }
            catch (ServicesRequestDependencyException servicesRequestDependencyException)
            {
                return Problem(servicesRequestDependencyException.Message);
            }
            catch (ServicesRequestServiceException serviceRequestServiceException)
            {
                return Problem(serviceRequestServiceException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
           exception.InnerException.Message;
    }
}
