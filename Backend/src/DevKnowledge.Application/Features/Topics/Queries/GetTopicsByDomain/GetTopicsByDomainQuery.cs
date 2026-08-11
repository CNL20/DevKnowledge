using MediatR;

namespace DevKnowledge.Application.Features.Topics.Queries.GetTopicsByDomain;

public record TopicDto(Guid Id, string Name, string? Description, string Slug, Guid DomainId);

public record GetTopicsByDomainQuery(Guid DomainId) : IRequest<List<TopicDto>>;
