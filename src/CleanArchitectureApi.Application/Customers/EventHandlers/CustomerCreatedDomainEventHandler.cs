using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Emails;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Customers.Events;
using CleanArchitectureApi.Domain.Customers.Exceptions;
using MediatR;

namespace CleanArchitectureApi.Application.Customers.EventHandlers;

internal sealed class CustomerCreatedDomainEventHandler(IUnitOfWork unitOfWork, IEmailService emailService)
    : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(notification.CustomerId, cancellationToken);

        if (customer == null)
        {
            throw new CustomerNotFoundException();
        }

        await _emailService.SendAsync();
    }
}