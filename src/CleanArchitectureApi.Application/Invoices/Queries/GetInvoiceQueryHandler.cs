using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.InvoiceItems.DTO;
using CleanArchitectureApi.Application.Invoices.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Invoices.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Invoices.Queries;

internal sealed class GetInvoiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IQueryHandler<GetInvoiceQuery, InvoiceResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<InvoiceResponse>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>()
            .GetAll()
            .Include(x => x.PurchasedProducts)
            .ProjectTo<InvoiceResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.InvoiceId, cancellationToken);

        if (invoice is null)
        {
            return Result<InvoiceResponse>.Failure(HttpStatusCode.BadRequest, "Null.Error", "The invoice was not found");
        }

        return Result<InvoiceResponse>.Success(invoice, HttpStatusCode.OK);
    }
}