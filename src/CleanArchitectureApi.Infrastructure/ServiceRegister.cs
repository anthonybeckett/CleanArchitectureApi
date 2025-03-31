using CleanArchitectureApi.Application.Abstractions.Cache;
using CleanArchitectureApi.Application.Abstractions.Emails;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Infrastructure.Outbox;
using CleanArchitectureApi.Infrastructure.Repositories;
using CleanArchitectureApi.Infrastructure.Services.Caching;
using CleanArchitectureApi.Infrastructure.Services.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CleanArchitectureApi.Infrastructure;

public static class ServiceRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbConnection(services, configuration);

        AddServicesToDiContainer(services, configuration);

        AddCaching(services, configuration);

        AddHealthChecks(services, configuration);

        AddBackgroundJobs(services, configuration);

        return services;
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        return services;
    }

    private static IServiceCollection AddServicesToDiContainer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(opt => opt.Configuration = configuration.GetConnectionString("Cache"));

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database") ?? throw new InvalidOperationException())
            .AddRedis(configuration.GetConnectionString("Cache") ?? throw new InvalidOperationException());

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartz();
        
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobsSetup>();

        return services;
    }
}