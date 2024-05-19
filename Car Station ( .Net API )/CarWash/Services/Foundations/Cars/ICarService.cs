using System;
using CarWash.Models.Cars;

namespace CarWash.Services.Foundations.Cars
{
    public interface ICarService
	{
        ValueTask<Car> AddCarAsync(Car car);
        IQueryable<Car> RetrieveAllCars();
        ValueTask<Car> RetrieveCarByIdAsync(Guid carId);
        ValueTask<Car> ModifyCarAsync(Car car);
        ValueTask<Car> RemoveCarByIdAsync(Guid carId);

    }
}

