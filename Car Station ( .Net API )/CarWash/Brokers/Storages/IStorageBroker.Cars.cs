using CarWash.Models.Cars;


namespace CarWash.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Car> InsertCar(Car car);
        IQueryable<Car> SelectAllCar();
        ValueTask<Car> SelectAsyncCar(Guid carId);
        ValueTask<Car> UpdateAsyncCar(Car car);
        ValueTask<Car> DeleteAsyncCar(Car car);
    }
}

