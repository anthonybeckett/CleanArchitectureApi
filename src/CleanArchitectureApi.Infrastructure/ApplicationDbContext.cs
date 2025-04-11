using System.Reflection;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Attributes;
using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Identity.Roles.Entities;
using CleanArchitectureApi.Domain.Identity.Users.Entities;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Products.Entities;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using CleanArchitectureApi.Infrastructure.Outbox;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CleanArchitectureApi.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher, IConfiguration configuration)
    : IdentityDbContext<AppUser, AppRole, Guid>
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddDomainEventsAsOutboxMessages();

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        ProcessAutoseedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = configuration.GetConnectionString("Database");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
            .Entries<IDomainEventRaiser>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                }
            )
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                DateTime.UtcNow,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)
            )).ToList();

        AddRange(outboxMessages);
    }

    private void ProcessAutoseedData(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder
            .Model
            .GetEntityTypes()
            .Select(x => x.ClrType)
            .Where(x => x.GetInterface(nameof(IAutoseedData)) != null)
            .SelectMany(e => e.GetProperties())
            .Where(e => e.GetCustomAttribute<AutoSeedDataAttribute>() != null)
            .GroupBy(e => e.DeclaringType)
            .ToList();

        foreach (var group in entityTypes)
        {
            var entityType = modelBuilder.Entity(group.Key);

            foreach (var property in group)
            {
                var value = property.GetValue(Activator.CreateInstance(property.DeclaringType));

                entityType.HasData(value ??
                                   throw new InternalServerException("AutoseedFailure.Error",
                                       ["PropertyInfo value null error"])
                );
            }
        }
    }
}