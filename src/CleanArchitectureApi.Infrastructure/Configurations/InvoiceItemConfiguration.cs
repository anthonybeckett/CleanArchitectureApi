using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.InvoiceItems.ValueObjects;
using CleanArchitectureApi.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureApi.Infrastructure.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.Property(item => item.SellPrice)
            .HasConversion(
                sellPrice => sellPrice.Value,
                value => new Balance(value)
            )
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.TotalPrice)
            .HasConversion(
                totalPrice => totalPrice.Value,
                value => new Balance(value)
            )
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.Quantity)
            .HasConversion(
                quantity => quantity.Value,
                value => new Quantity(value)
            )
            .IsRequired();

        builder.Property(item => item.Description)
            .HasConversion(
                description => description.Value,
                value => new Title(value)
            )
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}