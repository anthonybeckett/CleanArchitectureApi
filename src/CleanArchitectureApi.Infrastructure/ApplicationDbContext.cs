using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Infrastructure;

public class ApplicationDbContext : DbContext
{
    protected ApplicationDbContext()
    {
        //
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Invoice> Invoices { get; set; }
    
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}