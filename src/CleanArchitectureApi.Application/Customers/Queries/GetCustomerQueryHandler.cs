using System.Net;
using AutoMapper;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Customers.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;

namespace CleanArchitectureApi.Application.Customers.Queries;

internal sealed class GetCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetCustomerQuery, CustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CustomerResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
            .GetByIdAsync(request.CustomerId, cancellationToken);
        
        if (customer == null)
        {
            return Result<CustomerResponse>.Failure(HttpStatusCode.BadRequest, "Null.Error", "Customer not found");
        }
        
        var response = _mapper.Map<CustomerResponse>(customer);

        return Result<CustomerResponse>.Success(response, HttpStatusCode.OK);
    }
}