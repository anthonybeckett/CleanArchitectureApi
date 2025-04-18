using CleanArchitectureApi.Application.Products.Commands;
using CleanArchitectureApi.Application.Products.DTO;
using CleanArchitectureApi.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureApi.Api.Controllers.Version1.Products;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class ProductsController(ISender sender) : BaseController
{
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductAsync(Guid productId,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetProductQuery(productId), cancellationToken);

        return CreateResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetAllProductsQuery(), cancellationToken);

        return CreateResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductsAsync(CreateProductDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new CreateProductCommand(request), cancellationToken);

        return CreateResult(response);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductAsync(
        Guid productId,
        UpdateProductDto request,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sender.Send(new UpdateProductCommand(productId, request), cancellationToken);

        return CreateResult(response);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RemoveProductCommand(productId), cancellationToken);

        return CreateResult(response);
    }
}