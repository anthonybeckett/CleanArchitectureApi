using System.Net;
using AutoMapper;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Application.Products.Commands;

internal sealed class CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : ICommandHandler<CreateProductCommand, ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productDto = request.Dto;
        var unitPrice = new Balance(productDto.UnitPrice);
        var description = new Title(productDto.Description ?? "");
        
        var product = Product.Create(description, unitPrice);

        await _unitOfWork.Repository<Product>()
            .CreateAsync(product, cancellationToken);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        var response = _mapper.Map<Product, ProductResponse>(product);

        return Result<ProductResponse>.Success(response, HttpStatusCode.Created);
    }
}