using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Application.Products.Commands;

internal sealed class UpdateProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(request.Dto.ProductId, cancellationToken);

        if (product == null)
        {
            return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, "Null.Error", "Product not found");
        }

        var productDto = request.Dto;
        Title description = new(request.Dto.Description ?? "");
        Balance unitPrice = new(request.Dto.UnitPrice);

        product.Update(description, unitPrice);
        
        _unitOfWork.Repository<Product>().Update(product);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>.Success(HttpStatusCode.NoContent);
    }
}