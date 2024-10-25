using System;
using Microsoft.EntityFrameworkCore;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;
using zIndraTechnicalChallenge.Infrastructure.Data.Context;
using zIndraTechnicalChallenge.Infrastructure.Data.Repositories;
using zIndraTechnicalChallenge.Infrastructure.Data.SeedWork;

namespace zIndraTechnicalChallenge.Service.Rest.MainContext.IoC
{
    public class Container
    {
        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterDataBase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
