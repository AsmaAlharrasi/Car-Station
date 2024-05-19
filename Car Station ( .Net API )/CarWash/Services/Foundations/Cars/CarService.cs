using System;
using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Models.Cars;

namespace CarWash.Services.Foundations.Cars
{
    public partial class CarService :ICarService
	{
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public CarService(IStorageBroker storageBroker, IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker)
		{
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

       
        //add
        public ValueTask<Car> AddCarAsync(Car car) =>
            TryCatch(async () =>
            {
                return await storageBroker.InsertCar(car);
            });

        //update 
        public ValueTask<Car> ModifyCarAsync(Car car) =>
            TryCatch(async () =>
            {
                Car maybeCar = await this.storageBroker.SelectAsyncCar(car.Id);
                return await storageBroker.UpdateAsyncCar(car);

            });

        //delete
        public ValueTask<Car> RemoveCarByIdAsync(Guid carId) =>
            TryCatch(async () =>
            {
                Car maybeCar = await this.storageBroker.SelectAsyncCar(carId);
                return await this.storageBroker.DeleteAsyncCar(maybeCar);
            });

        //select all  
        public IQueryable<Car> RetrieveAllCars() =>
             TryCatch(() => this.storageBroker.SelectAllCar());

        //select by Id
        public ValueTask<Car> RetrieveCarByIdAsync(Guid carId) =>
            TryCatch(async () =>
            {
                Car maybeCar = await this.storageBroker.SelectAsyncCar(carId);
                return maybeCar;
            });

    }
}

