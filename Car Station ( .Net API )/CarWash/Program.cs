
using CarWash.Brokers.DateTimes;
using CarWash.Brokers.Loggings;
using CarWash.Brokers.Storages;
using CarWash.Brokers.UserManagement;
using CarWash.Models.Users;
using CarWash.Services.Foundations.Cars;
using CarWash.Services.Foundations.Customers;
using CarWash.Services.Foundations.Employees;
using CarWash.Services.Foundations.ServiceRequests;
using CarWash.Services.Foundations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CarWash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            AddNewtonSoftJson(builder.Services);
            builder.Services.AddLogging();
            builder.Services.AddDbContext<StorageBroker>();
            AddBroker(builder.Services);
            AddFoundationServices(builder.Services);
            

            builder.Services.AddIdentityCore<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<StorageBroker>()
                .AddDefaultTokenProviders();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //    options =>
            //{
            //    options.SwaggerDoc
            //    (
            //        name: "V1",
            //        info: new OpenApiInfo
            //        {
            //            Title = "CarWash",
            //            Version = "v1"
            //        });
            //});

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                //    (options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CarWash v1");
                //});
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            app.MapControllers();

            app.Run();
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IServiceRequestService, ServiceRequestService>();
            services.AddTransient<IServiceService, ServiceService>();
        }
        private static void AddBroker(IServiceCollection services)
        {
            services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddScoped<IDateTimeBroker, DateTimeBroker>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddScoped<IUserManagementBroker, UserManagementBroker>();
        }
        private static void AddNewtonSoftJson(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }
    }
}
