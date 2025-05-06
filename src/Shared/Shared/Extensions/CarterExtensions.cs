using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class CarterExtensions
    {
        public static IServiceCollection AddCarterWithAssemblies
            (this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {
                foreach (var assembly in assemblies) //para cada assembly buscar a implementação de ICarterModule
                {
                    var catalogModules = assembly.GetTypes()
                        .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

                    config.WithModules(catalogModules);
                }
            });

            return services;    
        }
    }
}
