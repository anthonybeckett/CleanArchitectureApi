using System.Net;
using AutoMapper;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Customers.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Customers.ValueObjects;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Application.Customers.Commands;

internal sealed class CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : ICommandHandler<CreateCustomerCommand, CustomerResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CustomerResponse>> Handle(CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customerTitle = new Title(request.Dto.Title);

        var customerAddress = new Address(
            request.Dto.AddressLine1,
            request.Dto.AddressLine2,
            request.Dto.Town,
            request.Dto.County,
            request.Dto.Postcode,
            request.Dto.Country
        );

        var customer = Customer.Create(customerTitle, customerAddress);

        await _unitOfWork.Repository<Customer>().CreateAsync(customer, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var response = _mapper.Map<CustomerResponse>(customer);

        return Result<CustomerResponse>.Success(response, HttpStatusCode.Created);
    }
}