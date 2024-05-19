using System.Data;
using CarWash.Models.ServicesModels;
using CarWash.Models.Users;
using EFxceptions.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CarWash.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //Add Data 
        private async ValueTask<T> Insert<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Added;
            await  this.SaveChangesAsync();
            return @object;
        }
        // View all data 
        private IQueryable<T> SelectAll<T>() where T : class => this.Set<T>();

        //selete by Id 
        private async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
            await this.FindAsync<T>(@objectIds);

        //Updata 
        private async ValueTask<T> UpdateAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return @object;
        }

        //delete
        private async ValueTask<T> DeleteAsync<T>(T @object)
        {
            this.Entry(@object).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return @object;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetCarReference(modelBuilder);
            SetServicesRequestReference(modelBuilder);
            SetEmployeeReference(modelBuilder);
            SetServiceReference(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            string connectionString = this.configuration.GetConnectionString("Connection");
            optionsBuilder.UseSqlServer(connectionString);
        }

       
    }
}
