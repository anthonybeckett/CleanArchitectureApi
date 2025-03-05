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
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponse>> Handle(CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customerTitle = new Title(request.Dto.Title);
        
        var customerAddress = new Address(
            AddressLine1: request.Dto.AddressLine1,
            AddressLine2: request.Dto.AddressLine2,
            Town: request.Dto.Town,
            County: request.Dto.County,
            Postcode: request.Dto.Postcode,
            Country: request.Dto.Country
        );

        var customer = Customer.Create(customerTitle, customerAddress);

        await _unitOfWork.Repository<Customer>().CreateAsync(customer);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        var response = _mapper.Map<CustomerResponse>(customer);

        return Result<CustomerResponse>.Success(response, HttpStatusCode.Created);
    }
}