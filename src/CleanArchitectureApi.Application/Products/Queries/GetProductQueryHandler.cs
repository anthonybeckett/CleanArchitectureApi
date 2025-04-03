using System.Net;
using AutoMapper;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;

namespace CleanArchitectureApi.Application.Products.Queries;

internal sealed class GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IQueryHandler<GetProductQuery, ProductResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
            return Result<ProductResponse>.Failure(HttpStatusCode.BadRequest, "Null.Error", "Product not found");

        var response = _mapper.Map<ProductResponse>(product);

        return Result<ProductResponse>.Success(response, HttpStatusCode.OK);
    }
}