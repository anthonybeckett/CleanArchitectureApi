using CleanArchitectureApi.Application.Customers.Commands;
using CleanArchitectureApi.Application.Customers.DTO;
using CleanArchitectureApi.Application.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureApi.Api.Controllers.Version1.Customers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CustomersController(ISender sender) : BaseController
{
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerAsync(Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetCustomerQuery(customerId), cancellationToken);

        return CreateResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomersAsync(CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetAllCustomersQuery(), cancellationToken);

        return CreateResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomersAsync(CreateCustomerDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new CreateCustomerCommand(request), cancellationToken);

        return CreateResult(response);
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomerAsync(
        Guid customerId,
        UpdateCustomerDto request,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sender.Send(new UpdateCustomerCommand(customerId, request), cancellationToken);

        return CreateResult(response);
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new RemoveCustomerCommand(customerId), cancellationToken);

        return CreateResult(response);
    }
}