using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Customers.Commands;

internal sealed class RemoveCustomerCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetAll()
            .Include(x => x.Invoices)
            .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer == null)
            return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, "Null.Error", "Customer not found");

        if (customer.Invoices.Count > 0)
            return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, "Invalid.Error",
                "Customer cannot be removed when they have existing invoices.");

        _unitOfWork.Repository<Customer>().Delete(customer);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>.Success(HttpStatusCode.NoContent);
    }
}