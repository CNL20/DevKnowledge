using MediatR;

namespace DevKnowledge.Application.Features.Topics.Commands.CreateTopic;

public record CreateTopicCommand(Guid DomainId, string Name, string? Description, string Slug) : IRequest<Guid>;
