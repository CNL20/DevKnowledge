using DevKnowledge.Application.Common.Exceptions;
using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevKnowledge.Application.Features.Topics.Commands.CreateTopic;

public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTopicCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var domainExists = await _context.Domains
            .AnyAsync(d => d.Id == request.DomainId, cancellationToken);

        if (!domainExists)
        {
            throw new NotFoundException("Domain", request.DomainId);
        }

        var topicExists = await _context.Topics
            .AnyAsync(t => t.DomainId == request.DomainId && (t.Name == request.Name || t.Slug == request.Slug), cancellationToken);

        if (topicExists)
        {
            throw new ValidationException(new Dictionary<string, string[]>
            {
                { "Topic", new[] { "Topic with this Name or Slug already exists in the selected Domain." } }
            });
        }

        var topic = new Topic
        {
            DomainId = request.DomainId,
            Name = request.Name,
            Description = request.Description,
            Slug = request.Slug
        };

        _context.Topics.Add(topic);
        await _context.SaveChangesAsync(cancellationToken);

        return topic.Id;
    }
}
