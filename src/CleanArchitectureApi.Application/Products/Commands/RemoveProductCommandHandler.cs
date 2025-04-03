using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;

namespace CleanArchitectureApi.Application.Products.Commands;

internal sealed class RemoveProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<RemoveProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
            return Result<NoContentDto>.Failure(HttpStatusCode.BadRequest, "Null.Error", "Product not found");

        _unitOfWork.Repository<Product>().Delete(product);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>.Success(HttpStatusCode.NoContent);
    }
}