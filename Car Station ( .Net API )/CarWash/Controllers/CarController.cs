using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWash.Models.Cars;
using CarWash.Models.Cars.Exceptions;
using CarWash.Services.Foundations.Cars;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;


namespace CarWash.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : RESTFulController
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Car>> PostCarAsync(Car car)
        {
            try
            {
                Car createCar = await this.carService.AddCarAsync(car);
                return Created(createCar);
            }
            catch (CarValidationException carValidationExeption)
            when (carValidationExeption.InnerException is AlreadyExistsCarException)
            {
                return Conflict(carValidationExeption.InnerException);
            }
            catch (CarValidationException carValidationExeption)
            {
                return BadRequest(carValidationExeption.InnerException);
            }
            catch (CarDependencyException carDependencyExeption)
            {
                return InternalServerError(carDependencyExeption);
            }
            catch (CarServiceException carServiceException)
            {
                return InternalServerError(carServiceException);
            }

        }

        [HttpGet]
        public ActionResult<IQueryable<Car>> GetAllCars()
        {
            try
            {
                IQueryable allCars = this.carService.RetrieveAllCars();
                return Ok(allCars);
            }
            catch (CarDependencyException carDependencyExeption)
            {
                return Problem(carDependencyExeption.Message);
            }
            catch (CarServiceException carServiceException)
            {
                return Problem(carServiceException.Message);
            }
        }

        [HttpGet("{carId}")]
        public async ValueTask<ActionResult<Car>> GetCarByIdAsync(Guid carId)
        {
            try
            {
                Car getCar = await this.carService.RetrieveCarByIdAsync(carId);
                return Ok(getCar);
            }
            catch (CarValidationException carValidationException)
                when (carValidationException.InnerException is NotFoundCarException)
            {
                string innerMessage = GetInnerMessage(carValidationException);
                return NotFound(innerMessage);
            }
            catch (CarValidationException carValidationException)
            {
                string innerMessage = GetInnerMessage(carValidationException);
                return BadRequest(innerMessage);
            }
            catch (CarDependencyException carDependencyExeption)
            {
                return Problem(carDependencyExeption.Message);
            }
            catch (CarServiceException carServiceException)
            {
                return Problem(carServiceException.Message);
            }
        }

        [HttpPut("{carId}")]
        public async ValueTask<ActionResult<Car>> PutCarAsync(Car car)
        {
            try
            {
                Car updateCar = await this.carService.ModifyCarAsync(car);
                return Ok(updateCar);
            }
            catch (CarValidationException carValidationException)
                when (carValidationException.InnerException is NotFoundCarException)
            {
                string innerMessage = GetInnerMessage(carValidationException);
                return NotFound(innerMessage);
            }
            catch (CarValidationException carValidationException)
            {
                string innerMessage = GetInnerMessage(carValidationException);
                return BadRequest(innerMessage);
            }
            catch (CarDependencyException carDependencyExeption)
                when (carDependencyExeption.InnerException is LockedCarException)
            {
                string innerMessage = GetInnerMessage(carDependencyExeption);
                return Locked(innerMessage);
            }
            catch (CarDependencyException carDependencyExeption)
            {
                return Problem(carDependencyExeption.Message);
            }
            catch (CarServiceException carServiceException)
            {
                return Problem(carServiceException.Message);
            }

        }

        [HttpDelete("{carId}")]
        public async ValueTask<ActionResult<Car>> DeleteCarAsync(Guid carId)
        {
            try
            {
                Car deleteCar = await this.carService.RemoveCarByIdAsync(carId);
                return Ok(carId);
            }
            catch (CarValidationException carValidationException)
                when (carValidationException.InnerException is NotFoundCarException)
            {
                string innerMessage = GetInnerMessage(carValidationException);
                return NotFound(innerMessage);
            }
            catch (CarValidationException carValidationException)
            {
                return BadRequest(carValidationException.Message);
            }
            catch (CarDependencyException carDependencyExeption)
               when (carDependencyExeption.InnerException is LockedCarException)
            {
                string innerMessage = GetInnerMessage(carDependencyExeption);
                return Locked(innerMessage);
            }
            catch (CarDependencyException carDependencyExeption)
            {
                return Problem(carDependencyExeption.Message);
            }
            catch (CarServiceException carServiceException)
            {
                return Problem(carServiceException.Message);
            }
        }

        private static string GetInnerMessage(Exception exception) =>
       exception.InnerException.Message;
    }

}


