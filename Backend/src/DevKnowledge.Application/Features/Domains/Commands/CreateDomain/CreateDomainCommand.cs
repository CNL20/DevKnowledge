using MediatR;

namespace DevKnowledge.Application.Features.Domains.Commands.CreateDomain;

public record CreateDomainCommand(string Name, string? Description, string Slug) : IRequest<Guid>;
