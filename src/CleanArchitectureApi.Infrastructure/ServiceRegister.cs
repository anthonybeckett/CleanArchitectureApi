using System.Numerics;
using System.Text;
using CleanArchitectureApi.Application.Abstractions.Cache;
using CleanArchitectureApi.Application.Abstractions.Emails;
using CleanArchitectureApi.Application.Abstractions.TokenProvider;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Identity.Jwt.ValueObjects;
using CleanArchitectureApi.Domain.Identity.Roles.Entities;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using CleanArchitectureApi.Infrastructure.Outbox;
using CleanArchitectureApi.Infrastructure.Repositories;
using CleanArchitectureApi.Infrastructure.Services.Caching;
using CleanArchitectureApi.Infrastructure.Services.Email;
using CleanArchitectureApi.Infrastructure.Services.TokenProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;

namespace CleanArchitectureApi.Infrastructure;

public static class ServiceRegister
{
    private static string? _connString;

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        FetchConnectionString();
        
        AddDbConnection(services, configuration);

        AddServicesToDiContainer(services, configuration);

        AddCaching(services, configuration);

        AddHealthChecks(services, configuration);

        AddBackgroundJobs(services, configuration);

        AddIdentity(services, configuration);

        return services;
    }

    private static void FetchConnectionString()
    {
        _connString = Environment.GetEnvironmentVariable("DB_CONNECTION");

        if (_connString == null)
        {
            throw new Exception("DB_CONNECTION environment variable not set");
        }
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(_connString);
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
            .AddSqlServer(_connString ?? throw new InvalidOperationException())
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

    private static IServiceCollection AddIdentity(IServiceCollection services, IConfiguration configuration)
    {
        // Possibly need a better way of handling this but if running locally, use the .env file config directly
        // else if using docker, use the configuration option
        var jwtSettings = new JwtSettings
        {
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") 
                       ?? configuration["Jwt:Audience"],
               
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") 
                     ?? configuration["Jwt:Issuer"],
             
            Secret = Environment.GetEnvironmentVariable("JWT_SECRET") 
                     ?? configuration["Jwt:Secret"],
             
            TokenValidityInMinutes = int.Parse(
                Environment.GetEnvironmentVariable("JWT_TOKEN_VALIDITY_MINUTES") 
                ?? configuration["Jwt:TokenValidityInMinutes"] 
                ?? "15"),
        
            RefreshTokenValidityInDays = int.Parse(
                Environment.GetEnvironmentVariable("JWT_REFRESH_TOKEN_VALIDITY_DAYS") 
                ?? configuration["Jwt:RefreshTokenValidityInDays"] 
                ?? "60")
        };
        
        services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateLifetime = false,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        services.Configure<TokenSettings>(configuration.GetSection("JWT"));

        services.AddTransient<ITokenService, TokenService>();
        
        return services;
    }
}