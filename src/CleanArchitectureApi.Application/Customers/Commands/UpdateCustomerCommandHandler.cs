using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Customers.ValueObjects;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Application.Customers.Commands;

internal sealed class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer == null)
            return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, "Null.Error", "Customer not found");

        var customerTitle = new Title(request.Dto.Title);

        var customerAddress = new Address(
            request.Dto.AddressLine1,
            request.Dto.AddressLine2,
            request.Dto.Town,
            request.Dto.County,
            request.Dto.Postcode,
            request.Dto.Country
        );

        customer.Update(customerTitle, customerAddress);

        _unitOfWork.Repository<Customer>().Update(customer);

        await _unitOfWork.CommitAsync(cancellationToken, true);

        return Result<NoContentDto>.Success(HttpStatusCode.NoContent);
    }
}