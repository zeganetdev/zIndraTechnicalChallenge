using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Globalization;
using zIndraTechnicalChallenge.Application.MainContext;
using zIndraTechnicalChallenge.Infrastructure.Data.Context;
using zIndraTechnicalChallenge.Service.Rest.MainContext.Filters;
using zIndraTechnicalChallenge.Service.Rest.MainContext.IoC;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace zIndraTechnicalChallenge.Service.Rest.MainContext
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var cultureInfo = new CultureInfo("es");
            CultureInfo.CurrentUICulture = cultureInfo;
            ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Container.RegisterComponents(services, Configuration);
            services.AddApplicationServices();

            services.AddControllers(opt =>
            {
                opt.Filters.Add(typeof(ExceptionFilter));
            })
            .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

            services.AddEndpointsApiExplorer();

            Container.RegisterDataBase(services, Configuration);

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(x =>
                {
                    x.AllowAnyOrigin();
                    x.AllowAnyMethod();
                    x.WithHeaders();
                });
            });

            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment())
            {

                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated();
                }
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            log.AddFile("Logs/ErrorLog-{Date}.txt");

            app.UseCors();

            app.UseEndpoints(app =>
            {
                app.MapControllers();
            });
        }

    }
}
