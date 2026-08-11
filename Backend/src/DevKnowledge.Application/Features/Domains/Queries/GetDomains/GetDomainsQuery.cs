using MediatR;

namespace DevKnowledge.Application.Features.Domains.Queries.GetDomains;

public record DomainDto(Guid Id, string Name, string? Description, string Slug);

public record GetDomainsQuery : IRequest<List<DomainDto>>;
