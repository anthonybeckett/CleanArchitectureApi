using CleanArchitectureApi.Application.Invoices.Commands;
using CleanArchitectureApi.Application.Invoices.DTO;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using Microsoft.Extensions.Options;

namespace CleanArchitectureApi.Application.Invoices.Validation;

public class CreateInvoiceValidation
{
    public static bool Validate(CreateInvoiceCommand request)
    {
        if (request.Dto.CustomerId == Guid.Empty)
        {
            throw new BadRequestException(["Customer ID is Required"]);
        }

        if (request.Dto.PurchasedProducts is null || request.Dto.PurchasedProducts.Count == 0)
        {
            throw new BadRequestException(["Cannot create an empty invoice"]);
        }

        if (request.Dto.PurchasedProducts.Any(product => product.ProductId == Guid.Empty))
        {
            throw new BadRequestException(["Missing product ids in product list."]);
        }

        if (request.Dto.PurchasedProducts.Any(x => x.Quantity <= 0))
        {
            throw new BadRequestException(["Product quantity must be greater than zero."]);
        }

        return true;
    }
}