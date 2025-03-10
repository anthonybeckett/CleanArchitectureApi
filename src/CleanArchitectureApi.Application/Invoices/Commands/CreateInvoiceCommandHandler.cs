using System.Net;
using AutoMapper;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Invoices.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.InvoiceItems.ValueObjects;
using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Invoices.ValueObjects;
using CleanArchitectureApi.Domain.Products.Entities;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Application.Invoices.Commands;

internal sealed class CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper _mapper)
    : ICommandHandler<CreateInvoiceCommand, InvoiceResponse>
{
    public async Task<Result<InvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var poNumber = new PoNumber(request.Dto.PoNumber ?? String.Empty);

        var customerId = Guid.NewGuid();

        ICollection<InvoiceItem> purchasedProducts = [];

        var invoiceId = Guid.NewGuid();

        foreach (var purchasedProduct in request.Dto.PurchasedProducts)
        {
            var product = await unitOfWork
                              .Repository<Product>()
                              .GetByIdAsync(purchasedProduct.ProductId)
                          ?? throw new ArgumentNullException(
                              $"Product with id: {purchasedProduct.ProductId} not found");

            var invoiceItem = new InvoiceItem(
                Guid.NewGuid(),
                product.Description,
                new Balance(product.UnitPrice.Value),
                new Quantity(purchasedProduct.Quantity),
                invoiceId
            );

            purchasedProducts.Add(invoiceItem);
        }

        var invoice = Invoice.Create(invoiceId, poNumber, customerId, purchasedProducts, unitOfWork);

        await unitOfWork.Repository<Invoice>().CreateAsync(invoice, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);
        
        var response = _mapper.Map<InvoiceResponse>(invoice);

        return Result<InvoiceResponse>.Success(response, HttpStatusCode.Created);
    }
}