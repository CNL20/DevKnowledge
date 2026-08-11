using DevKnowledge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevKnowledge.Application.Features.Domains.Queries.GetDomains;

public class GetDomainsQueryHandler : IRequestHandler<GetDomainsQuery, List<DomainDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDomainsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DomainDto>> Handle(GetDomainsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Domains
            .AsNoTracking()
            .Select(d => new DomainDto(d.Id, d.Name, d.Description, d.Slug))
            .ToListAsync(cancellationToken);
    }
}
