using Microsoft.EntityFrameworkCore;
using DevKnowledge.Domain.Entities;

namespace DevKnowledge.Application.Common.Interfaces;

// Abstraction cho DbContext để Application layer không phụ thuộc trực tiếp vào EF Core (Infrastructure).
public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Domain.Entities.Domain> Domains { get; }
    DbSet<Topic> Topics { get; }
    DbSet<Knowledge> Knowledges { get; }
    DbSet<Source> Sources { get; }
    DbSet<KnowledgeSource> KnowledgeSources { get; }
    DbSet<CodeExample> CodeExamples { get; }
    DbSet<TechTerm> TechTerms { get; }
    DbSet<LearningProgress> LearningProgresses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
