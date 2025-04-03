using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Customers.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Customers.Queries;

internal sealed class GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetAllCustomersQuery, CustomerResponseCollection>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CustomerResponseCollection>> Handle(GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _unitOfWork.Repository<Customer>()
            .GetAll()
            .AsNoTracking()
            .ProjectTo<CustomerResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new CustomerResponseCollection
        {
            Customers = customers
        };

        return Result<CustomerResponseCollection>.Success(response, HttpStatusCode.OK);
    }
}