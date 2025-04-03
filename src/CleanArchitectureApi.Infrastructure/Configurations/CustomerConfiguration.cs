using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureApi.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(customer => customer.Address, address =>
        {
            address.Property(a => a.AddressLine1)
                .IsRequired()
                .HasMaxLength(40);

            address.Property(a => a.AddressLine2)
                .HasMaxLength(40);

            address.Property(a => a.Town)
                .IsRequired()
                .HasMaxLength(20);

            address.Property(a => a.County)
                .HasMaxLength(20);

            address.Property(a => a.Postcode)
                .IsRequired()
                .HasMaxLength(10);

            address.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(20);
        });

        builder.Property(customer => customer.Title)
            .HasConversion(
                title => title.Value,
                value => new Title(value)
            )
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(customer => customer.Balance)
            .HasConversion(
                balance => balance.Value,
                value => new Balance(value)
            )
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasMany(customer => customer.Invoices)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}