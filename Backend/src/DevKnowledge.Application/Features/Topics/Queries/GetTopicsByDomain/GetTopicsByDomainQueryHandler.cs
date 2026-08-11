using DevKnowledge.Application.Common.Exceptions;
using DevKnowledge.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevKnowledge.Application.Features.Topics.Queries.GetTopicsByDomain;

public class GetTopicsByDomainQueryHandler : IRequestHandler<GetTopicsByDomainQuery, List<TopicDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTopicsByDomainQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TopicDto>> Handle(GetTopicsByDomainQuery request, CancellationToken cancellationToken)
    {
        var domainExists = await _context.Domains
            .AnyAsync(d => d.Id == request.DomainId, cancellationToken);

        if (!domainExists)
        {
            throw new NotFoundException("Domain", request.DomainId);
        }

        return await _context.Topics
            .AsNoTracking()
            .Where(t => t.DomainId == request.DomainId)
            .Select(t => new TopicDto(t.Id, t.Name, t.Description, t.Slug, t.DomainId))
            .ToListAsync(cancellationToken);
    }
}
