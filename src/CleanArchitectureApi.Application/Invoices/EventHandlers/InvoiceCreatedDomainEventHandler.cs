using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Invoices.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Invoices.EventHandlers;

internal sealed class InvoiceCreatedDomainEventHandler(IUnitOfWork unitOfWork)
    : INotificationHandler<InvoiceCreatedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(InvoiceCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>()
            .GetAll()
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == notification.InvoiceId, cancellationToken);
        
        if (invoice is null)
        {
            return;
        }
        
        invoice.Customer.UpdateBalance(invoice.TotalBalance.Value);
        
        _unitOfWork.Repository<Invoice>().Update(invoice);
        
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}