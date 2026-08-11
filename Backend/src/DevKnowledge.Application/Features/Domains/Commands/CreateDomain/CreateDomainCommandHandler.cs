using DevKnowledge.Application.Common.Exceptions;
using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevKnowledge.Application.Features.Domains.Commands.CreateDomain;

public class CreateDomainCommandHandler : IRequestHandler<CreateDomainCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateDomainCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDomainCommand request, CancellationToken cancellationToken)
    {
        // Check if domain with same name or slug already exists
        var exists = await _context.Domains
            .AnyAsync(d => d.Name == request.Name || d.Slug == request.Slug, cancellationToken);

        if (exists)
        {
            throw new ValidationException(new Dictionary<string, string[]>
            {
                { "Domain", new[] { "Domain with this Name or Slug already exists." } }
            });
        }

        var domain = new Domain.Entities.Domain
        {
            Name = request.Name,
            Description = request.Description,
            Slug = request.Slug
        };

        _context.Domains.Add(domain);
        await _context.SaveChangesAsync(cancellationToken);

        return domain.Id;
    }
}
