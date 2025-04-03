using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Invoices.ValueObjects;
using CleanArchitectureApi.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureApi.Infrastructure.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(invoice => invoice.PoNumber)
            .HasConversion(
                poNumber => poNumber.Value,
                value => new PoNumber(value)
            )
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(invoice => invoice.TotalBalance)
            .HasConversion(
                totalBalance => totalBalance.Value,
                value => new Balance(value)
            )
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasMany(invoice => invoice.PurchasedProducts)
            .WithOne(purchasedProduct => purchasedProduct.Invoice)
            .HasForeignKey(purchasedProduct => purchasedProduct.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.RowVersion).IsRowVersion();
    }
}