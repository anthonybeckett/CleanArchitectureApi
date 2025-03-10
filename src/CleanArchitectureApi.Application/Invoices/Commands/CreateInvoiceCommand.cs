using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Invoices.DTO;

namespace CleanArchitectureApi.Application.Invoices.Commands;

public record CreateInvoiceCommand(CreateInvoiceDto Dto) : ICommand<InvoiceResponse>;