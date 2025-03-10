using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Invoices.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Invoices.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Application.Invoices.Queries;

internal sealed class GetAllInvoicesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IQueryHandler<GetAllInvoicesQuery, InvoiceResponseCollection>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<InvoiceResponseCollection>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Repository<Invoice>()
            .GetAll()
            .ProjectTo<InvoiceResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new InvoiceResponseCollection
        {
            Invoices = invoices.AsReadOnly()
        };

        return Result<InvoiceResponseCollection>.Success(response, HttpStatusCode.OK);
    }
}