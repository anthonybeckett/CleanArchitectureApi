using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureApi.Application;

public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}