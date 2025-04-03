using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.InvoiceItems.ValueObjects;
using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Domain.InvoiceItems.Entities;

public class InvoiceItem : BaseEntity
{
    private InvoiceItem()
    {
        //
    }

    public InvoiceItem(
        Guid id,
        Title description,
        Balance sellPrice,
        Quantity quantity,
        Guid invoiceId
    ) : base(id)
    {
        Description = description;
        SellPrice = sellPrice;
        Quantity = quantity;
        TotalPrice = new Balance(SellPrice.Value * Quantity.Value);
        InvoiceId = invoiceId;
    }

    public Title Description { get; set; }

    public Balance SellPrice { get; }

    public Quantity Quantity { get; }

    public Balance TotalPrice { get; private set; }

    public Guid InvoiceId { get; private set; }

    public Invoice Invoice { get; private set; }
}