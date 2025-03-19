using System.Reflection;
using CleanArchitectureApi.Application.Abstractions.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureApi.Application;

public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        var applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(applicationAssembly);

            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));

            config.AddOpenBehavior(typeof(CachingBehaviour<,>));
        });

        return services;
    }
}