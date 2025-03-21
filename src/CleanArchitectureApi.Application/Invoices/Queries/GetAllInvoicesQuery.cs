using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Invoices.DTO;

namespace CleanArchitectureApi.Application.Invoices.Queries;

public record GetAllInvoicesQuery : IQuery<InvoiceResponseCollection>;