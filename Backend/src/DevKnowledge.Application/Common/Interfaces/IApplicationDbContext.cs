using Microsoft.EntityFrameworkCore;
using DevKnowledge.Domain.Entities;

namespace DevKnowledge.Application.Common.Interfaces;

// Abstraction cho DbContext để Application layer không phụ thuộc trực tiếp vào EF Core (Infrastructure).
public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Domain> Domains { get; }
    DbSet<Topic> Topics { get; }
    DbSet<Knowledge> Knowledges { get; }
    DbSet<Source> Sources { get; }
    DbSet<KnowledgeSource> KnowledgeSources { get; }
    DbSet<CodeExample> CodeExamples { get; }
    DbSet<TechTerm> TechTerms { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
