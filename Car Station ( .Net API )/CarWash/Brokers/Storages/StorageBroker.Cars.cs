
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CarWash.Models.Cars;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Car> Cars { get; set; }


        public async ValueTask<Car> InsertCar(Car car)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Car> CarEntyiyEntry = await broker.Cars.AddAsync(entity: car);
            await broker.SaveChangesAsync();
            return CarEntyiyEntry.Entity;
        }

        public IQueryable<Car> SelectAllCar() => Cars;


        public async ValueTask<Car> SelectAsyncCar(Guid carId)
        {
            var broker = new StorageBroker(configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await Cars.FindAsync(carId);
        }

        public async ValueTask<Car> UpdateAsyncCar(Car car)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Car> CarEntyiyEntry = broker.Cars.Update(entity: car);
            await broker.SaveChangesAsync();
            return CarEntyiyEntry.Entity;

        }

        public async ValueTask<Car> DeleteAsyncCar(Car car)
        {
            var broker = new StorageBroker(configuration);
            EntityEntry<Car> CarEntyiyEntry = broker.Cars.Remove(entity: car);
            await broker.SaveChangesAsync();
            return CarEntyiyEntry.Entity;

        }
    }
}
