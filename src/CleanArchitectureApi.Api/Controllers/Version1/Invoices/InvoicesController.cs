using CleanArchitectureApi.Application.Invoices.Commands;
using CleanArchitectureApi.Application.Invoices.DTO;
using CleanArchitectureApi.Application.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureApi.Api.Controllers.Version1.Invoices;

[ApiController]
[Route("/api/invoices")]
public class InvoicesController(ISender sender) : BaseController
{
    [HttpGet("{invoiceId}")]
    public async Task<IActionResult> GetInvoiceAsync(Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetInvoiceQuery(invoiceId), cancellationToken);

        return CreateResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoicesAsync(CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new GetAllInvoicesQuery(), cancellationToken);

        return CreateResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync(CreateInvoiceDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(new CreateInvoiceCommand(request), cancellationToken);

        return CreateResult(response);
    }
}