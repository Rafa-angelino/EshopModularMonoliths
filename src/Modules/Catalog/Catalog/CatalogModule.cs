using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            //add services to the container

            //api endpoint services

            //application use case services
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            //data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<CatalogDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IDataSeeder, CatalogDataSeeder>();
            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            //configure the HTTP request pipeline   

            //1 Use api endpoint services   
            //2 Use application use case services   
            //3 Use data - Infrastructure services

            app.UseMigration<CatalogDbContext>();
            return app;
        }
    }
}
