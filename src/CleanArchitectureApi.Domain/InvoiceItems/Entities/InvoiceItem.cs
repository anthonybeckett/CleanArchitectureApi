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

    internal InvoiceItem(
        Guid id,
        Balance sellPrice,
        Quantity quantity,
        Guid invoiceId
    ) : base(id)
    {
        SellPrice = sellPrice;
        Quantity = quantity;
        TotalPrice = new Balance(SellPrice.Value * Quantity.Value);
        InvoiceId = invoiceId;
    }

    public Balance SellPrice { get; private set; }

    public Quantity Quantity { get; private set; }

    public Balance TotalPrice { get; private set; }

    public Guid InvoiceId { get; private set; }

    public Invoice Invoice { get; private set; }
}