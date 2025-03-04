using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Products.Queries;

internal sealed class GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetAllProductsQuery, ProductResponseCollection>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProductResponseCollection>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork
            .Repository<Product>()
            .GetAll()
            .ProjectTo<ProductResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new ProductResponseCollection
        {
            Products = products.AsReadOnly()
        };

        return Result<ProductResponseCollection>.Success(response, HttpStatusCode.OK);
    }
}