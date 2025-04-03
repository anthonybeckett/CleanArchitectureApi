using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.Invoices.Events;
using CleanArchitectureApi.Domain.Invoices.ValueObjects;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Domain.Invoices.Entities;

public sealed class Invoice : BaseEntity
{
    private Invoice()
    {
        //
    }

    private Invoice(
        Guid invoiceId,
        PoNumber poNumber,
        Guid customerId,
        ICollection<InvoiceItem> purchasedProducts,
        Balance totalBalance
    )
    {
        PoNumber = poNumber;
        CustomerId = customerId;
        PurchasedProducts = purchasedProducts;
        TotalBalance = totalBalance;
    }

    public PoNumber PoNumber { get; private set; }

    public Customer Customer { get; private set; }

    public Guid CustomerId { get; private set; }

    public ICollection<InvoiceItem> PurchasedProducts { get; private set; }

    public Balance TotalBalance { get; private set; }

    public static Invoice Create(
        Guid invoiceId,
        PoNumber poNumber,
        Guid customerId,
        ICollection<InvoiceItem> purchasedProductsItems,
        IUnitOfWork unitOfWork
    )
    {
        if (purchasedProductsItems is null || purchasedProductsItems.Count == 0)
            throw new BadRequestException(["Empty invoice cannot be created"]);

        var totalBalance = purchasedProductsItems.Sum(x => x.TotalPrice.Value);

        var invoice = new Invoice(
            invoiceId,
            poNumber,
            customerId,
            purchasedProductsItems,
            new Balance(totalBalance)
        );

        invoice.RaiseDomainEvent(new InvoiceCreatedDomainEvent(invoiceId));

        return invoice;
    }
}